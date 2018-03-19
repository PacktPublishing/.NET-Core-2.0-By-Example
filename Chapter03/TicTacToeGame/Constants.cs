using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    /// <summary>
    /// The class for keeping all the constant string. Here we are storing all the client side methods that are invoked from SignalR hub.
    /// </summary>
    public class Constants
    {
        /// <summary>
        ///  Stores the registrationComplete method of the client.
        /// </summary>
        public const string RegistrationComplete = "registrationComplete";

        /// <summary>
        ///  Stores the waitingForOpponent method of the client.
        /// </summary>
        public const string WaitingForOpponent = "waitingForOpponent";

        /// <summary>
        ///  Stores the opponentFound method of the client.
        /// </summary>
        public const string OpponentFound = "opponentFound";

        /// <summary>
        ///  Stores the opponentNotFound method of the client.
        /// </summary>
        public const string OpponentNotFound = "opponentNotFound";

        /// <summary>
        ///  Stores the opponentDisconnected method of the client.
        /// </summary>
        public const string OpponentDisconnected = "opponentDisconnected";

        /// <summary>
        ///  Stores the waitingForMove method of the client.
        /// </summary>
        public const string WaitingForMove = "waitingForMove";

        /// <summary>
        ///  Stores the moveMade method of the client.
        /// </summary>
        public const string MoveMade = "moveMade";

        /// <summary>
        ///  Stores the gameOver method of the client.
        /// </summary>
        public const string GameOver = "gameOver";
    }
}
