namespace TicTacToeGame
{
    internal class Game
    {
        /// <summary>
        ///  Gets or sets the value indicating whether the game is over.
        /// </summary>
        public bool IsOver { get; private set; }

        /// <summary>
        /// Gets or sets the value indicating whether the game is draw.
        /// </summary>
        public bool IsDraw { get; private set; }

        /// <summary>
        /// Gets or sets Player 1 of the game
        /// </summary>
        public Player Player1 { get; set; }
        
        /// <summary>
        /// Gets or sets Player 2 of the game
        /// </summary>
        public Player Player2 { get; set; }

        /// <summary>
        /// For internal housekeeping, To keep track of value in each of the box in the grid.
        /// </summary>
        private readonly int[] field = new int[9];

        /// <summary>
        /// The number of moves left. We start the game with 9 moves remaining in a 3x3 grid.
        /// </summary>
        private int movesLeft = 9;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            //// Initialize the game
            for (var i = 0; i < field.Length; i++)
            {
                field[i] = -1;
            }
        }

        /// <summary>
        /// Place the player number at a given position for a player
        /// </summary>
        /// <param name="player">The player number would be 0 or 1</param>
        /// <param name="position">The position where player number would be placed, should be between 0 and 8, both inclusive</param>
        /// <returns>Boolean true if game is over and we have a winner.</returns>
        public bool Play(int player, int position)
        {
            if (this.IsOver)
            {
                return false;
            }

            //// Place the player number at the given position
            this.PlacePlayerNumber(player, position);

            //// Check if we have a winner. If this returns true, 
            //// game would be over and would have a winner, else game would continue.
            return this.CheckWinner();
        }

        /// <summary>
        /// Checks for the winner by inspecting different combination of winning combinations
        /// Notice that each position is initialized with -1, meaning no player has placed his number there.
        /// </summary>
        /// <returns>Boolean true if we have a winner.</returns>
        private bool CheckWinner()
        {
            //// Given the board below:
            ////  0|1|2
            ////  3|4|5
            ////  6|7|8
            //// There can be a winner if one of the following positions are occupied by same player, i.e, values of these cells is not -1 and is either 0 or 1
            
            //// 2,5,8 || 1,4,7 || 0,3,6 || 0,1,2 || 3,4,5 || 6,7,8 || 0,4,8 || 2,4,6

            //// This for loop checks for 2,5,8 || 1,4,7 || 0,3,6 || 0,1,2 || 3,4,5 || 6,7,8 combinations.
            for (int i = 0; i < 3; i++)
            {
                if (((field[i * 3] != -1 && field[(i * 3)] == field[(i * 3) + 1] && field[(i * 3)] == field[(i * 3) + 2]) ||
                     (field[i] != -1 && field[i] == field[i + 3] && field[i] == field[i + 6])))
                {
                    this.IsOver = true; //// Game is over
                    return true; //// We have a winner
                }
            }

            //// Manually check for these combinations 0,4,8 || 2,4,6
            if ((field[0] != -1 && field[0] == field[4] && field[0] == field[8]) || (field[2] != -1 && field[2] == field[4] && field[2] == field[6]))
            {
                this.IsOver = true; //// Game is over
                return true;   //// We have a winner
            }

            return false; //// Game can go on, we still don't have a winner.
        }

        /// <summary>
        /// Places the player number at the given position for the player if the position is marked as -1, i.e., not taken
        /// </summary>
        /// <param name="player">The player number, i.e, 0 or 1</param>
        /// <param name="position">The position to place the player number, should be between 0 and 8, both inclusive.</param>
        private void PlacePlayerNumber(int player, int position)
        {
            this.movesLeft -= 1;

            if (this.movesLeft <= 0)
            {
                //// We are out of moves, so game is over and is draw
                this.IsOver = true;
                this.IsDraw = true;               
            }

            if (position < field.Length && field[position] == -1)
            {
                field[position] = player;
            }
        }
    }
}