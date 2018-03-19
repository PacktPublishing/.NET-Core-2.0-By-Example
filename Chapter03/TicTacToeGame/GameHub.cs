using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Concurrent;

namespace TicTacToeGame
{
    /// <summary>
    /// The Game Hub class derived from Hub
    /// </summary>
    public class GameHub : Hub
    {
        /// <summary>
        ///  To keep the list of all the connected players registered with the game hub.
        /// </summary>
        private static readonly ConcurrentBag<Player> players = new ConcurrentBag<Player>();

        /// <summary>
        ///  The list of games going on.
        /// </summary>
        private static readonly ConcurrentBag<Game> games = new ConcurrentBag<Game>();

        /// <summary>
        ///  To simulate the coin toss. Like heads and tails, 0 belongs to one player and 1 to opponent.
        /// </summary>
        private static readonly Random toss = new Random();

        /// <summary>
        ///  Fires when a player is connected to hub.
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        /// <summary>
        ///  Fires when a player disconnects from the hub. 
        ///  In our case this would happen when the player looses connection with the hub, refreshes or closes the browser.
        /// </summary>
        /// <param name="exception">The exception at the time of disconnection.</param>
        /// <returns>The task.</returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {            
            //// Lets find a game if any of which player 1 / player 2 is disconnected.
            var game = games?.FirstOrDefault(j => j.Player1.ConnectionId == Context.ConnectionId || j.Player2.ConnectionId == Context.ConnectionId);
            if (game == null)
            {
                //// We are here so it means we have no game whose players were disconnected. 
                //// But there may be a scenario that a player is not playing but disconnected and is still in our list.
                //// So, lets remove that player from our list.
                var playerWithoutGame = players.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
                if (playerWithoutGame != null)
                {
                    //// Remove this player from our player list.
                    Remove<Player>(players, playerWithoutGame);
                }

                return null;
            }

            //// We have a game in which the player got disconnected, so lets remove that game from our list.
            if (game != null)
            {
                Remove<Game>(games, game);
            }

            //// Though we have removed the game from our list, we still need to notify the opponent that he has a walkover.
            //// If the current connection Id matches the player 1 connection Id, its him who disconnected else its player 2
            var player = game.Player1.ConnectionId == Context.ConnectionId ? game.Player1 : game.Player2;

            if (player == null)
            {
                return null;
            }

            //// Remove this player as he is disconnected and was in the game.
            Remove<Player>(players, player);

            //// Check if there was an opponent of the player. If yes, tell him, he won/ got a walk over.
            if (player.Opponent != null)
            {
                return OnOpponentDisconnected(player.Opponent.ConnectionId, player.Name);
            }
            
            return base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Fires on opponent disconnected.
        /// </summary>
        /// <param name="connectionId">The connection id of the player.</param>
        /// <param name="playerName">The player name that disconnected.</param>
        /// <returns></returns>
        public Task OnOpponentDisconnected(string connectionId, string playerName)
        {
            //// Notify this connection id that the given player name has disconnected.
            return Clients.Client(connectionId).InvokeAsync(Constants.OpponentDisconnected, playerName);
        }                

        /// <summary>
        /// Fires on completion of registration.
        /// </summary>
        /// <param name="connectionId">The connectionId of the player which registered</param>
        public void OnRegisterationComplete(string connectionId)
        {
            //// Notify this connection id that the registration is complete.
            this.Clients.Client(connectionId).InvokeAsync(Constants.RegistrationComplete);
        }

        /// <summary>
        /// Registers the player with name and image.
        /// </summary>
        /// <param name="nameAndImageData">The name and image data sent by the player.</param>
        public void RegisterPlayer(string nameAndImageData)
        {
            var splitData = nameAndImageData?.Split(new char[] { '#' }, StringSplitOptions.None);
            string name = splitData[0];
            string image = splitData[1];
            var player = players?.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (player == null)
            {
                player = new Player { ConnectionId = Context.ConnectionId, Name = name, IsPlaying = false, IsSearchingOpponent = false, RegisterTime = DateTime.UtcNow, Image = image };
                if (!players.Any(j => j.Name == name))
                {
                    players.Add(player);
                }
            }
            else
            {
                player.IsPlaying = false;
                player.IsSearchingOpponent = false;
            }

            this.OnRegisterationComplete(Context.ConnectionId);
        }

        /// <summary>
        /// Invoked by the player to make a move on the board.
        /// </summary>
        /// <param name="position">The position to place the player</param>
        public void MakeAMove(int position)
        {
            //// Lets find a game from our list of games where one of the player has the same connection Id as the current connection has.
            var game = games?.FirstOrDefault(x => x.Player1.ConnectionId == Context.ConnectionId || x.Player2.ConnectionId == Context.ConnectionId);

            if (game == null || game.IsOver)
            {
                //// No such game exist!
                return;
            }

            //// Designate 0 for player 1
            int symbol = 0;
                       
            if (game.Player2.ConnectionId == Context.ConnectionId)
            {
                //// Designate 1 for player 2.
                symbol = 1;
            }

            var player = symbol == 0 ? game.Player1 : game.Player2;
                        
            if (player.WaitingForMove)
            {
                return;
            }
            
            //// Update both the players that 
            Clients.Client(game.Player1.ConnectionId).InvokeAsync(Constants.MoveMade, new MoveInformation { OpponentName = player.Name, ImagePosition = position, Image = player.Image });
            Clients.Client(game.Player2.ConnectionId).InvokeAsync(Constants.MoveMade, new MoveInformation { OpponentName = player.Name, ImagePosition = position, Image = player.Image });

            //// Place the symbol and look for a winner after every move.
            if (game.Play(symbol, position))
            {
                Remove<Game>(games, game);
                Clients.Client(game.Player1.ConnectionId).InvokeAsync(Constants.GameOver, $"The winner is {player.Name}");
                Clients.Client(game.Player2.ConnectionId).InvokeAsync(Constants.GameOver, $"The winner is {player.Name}");
                player.IsPlaying = false;
                player.Opponent.IsPlaying = false;
                this.Clients.Client(player.ConnectionId).InvokeAsync(Constants.RegistrationComplete);
                this.Clients.Client(player.Opponent.ConnectionId).InvokeAsync(Constants.RegistrationComplete);
            }

            //// If no one won and its a tame draw, update the players that the game is over and let them look for new game to play.
            if (game.IsOver && game.IsDraw)
            {
                Remove<Game>(games, game);
                Clients.Client(game.Player1.ConnectionId).InvokeAsync(Constants.GameOver, "Its a tame draw!!!");
                Clients.Client(game.Player2.ConnectionId).InvokeAsync(Constants.GameOver, "Its a tame draw!!!");
                player.IsPlaying = false;
                player.Opponent.IsPlaying = false;
                this.Clients.Client(player.ConnectionId).InvokeAsync(Constants.RegistrationComplete);
                this.Clients.Client(player.Opponent.ConnectionId).InvokeAsync(Constants.RegistrationComplete);
            }

            if (!game.IsOver)
            {
                player.WaitingForMove = !player.WaitingForMove;
                player.Opponent.WaitingForMove = !player.Opponent.WaitingForMove;

                Clients.Client(player.Opponent.ConnectionId).InvokeAsync(Constants.WaitingForOpponent, player.Opponent.Name);
                Clients.Client(player.ConnectionId).InvokeAsync(Constants.WaitingForOpponent, player.Opponent.Name);
            }
        }

        /// <summary>
        /// Finds the opponent for the player and sets the Seraching for Opponent property of player to true. 
        /// We will use the connection id from context to identify the current player.
        /// Once we have 2 players looking for opponents, we can pair them as opponent and simulate coin toss to start the game.
        /// </summary>
        public void FindOpponent()
        {
            //// First fetch the player from our players collection having current connection id
            var player = players.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (player == null)
            {
                //// Since player would be registered before making this call,
                //// we should not reach here. If we are here, something somewhere in the flow above is broken.
                return;
            }

            //// Set that player is seraching for opponent.
            player.IsSearchingOpponent = true;

            //// We will follow a queue, so find a player who registered earlier as opponent. 
            //// This would only be the case if more than 2 players are looking for opponent.
            var opponent = players.Where(x => x.ConnectionId != Context.ConnectionId && x.IsSearchingOpponent && !x.IsPlaying).OrderBy(x =>x.RegisterTime).FirstOrDefault();
            if (opponent == null)
            {
                //// Could not find any opponent, invoke opponentNotFound method in the client.
                Clients.Client(Context.ConnectionId).InvokeAsync(Constants.OpponentNotFound);
                return;
            }

            //// Set both players as playing.
            player.IsPlaying = true;
            player.IsSearchingOpponent = false; //// Make him unsearchable for opponent search

            opponent.IsPlaying = true;
            opponent.IsSearchingOpponent = false;

            //// Set each other as opponents.
            player.Opponent = opponent;
            opponent.Opponent = player;

            //// Notify both players that they can play the game by invoking opponentFound method for both the players.
            //// Also pass the opponent name and opoonet image, so that they can visualize it.
            //// Here we are directly using connection id, but group is a good candidate and use here.
            Clients.Client(Context.ConnectionId).InvokeAsync(Constants.OpponentFound, opponent.Name, opponent.Image);
            Clients.Client(opponent.ConnectionId).InvokeAsync(Constants.OpponentFound, player.Name, player.Image);

            //// Coin Toss
            if (toss.Next(0, 1) == 0)
            {
                //// got 0 - First player will make the first move and player 2 would wait.
                player.WaitingForMove = false;
                opponent.WaitingForMove = true;

                //// Tell player that its his turn
                Clients.Client(player.ConnectionId).InvokeAsync(Constants.WaitingForMove, opponent.Name);
                //// Tell opponent player to wait for the move
                Clients.Client(opponent.ConnectionId).InvokeAsync(Constants.WaitingForOpponent, player.Name);
            }
            else
            {
                //// got 1 - First player will wait and player 2 would make the first move.
                player.WaitingForMove = true;
                opponent.WaitingForMove = false;

                //// Tell opponent player that its his turn
                Clients.Client(opponent.ConnectionId).InvokeAsync(Constants.WaitingForMove, player.Name);
                //// Tell player to wait for the opponent's move
                Clients.Client(player.ConnectionId).InvokeAsync(Constants.WaitingForOpponent, opponent.Name);
            }

            //// Create a new game with these 2 player and add it to games collection.
            games.Add(new Game { Player1 = player, Player2 = opponent });
        }

        private void Remove<T>(ConcurrentBag<T> players, T playerWithoutGame)
        {
            players = new ConcurrentBag<T>(players?.Except(new[] { playerWithoutGame }));
        }
    }
}