namespace TicTacToeGame
{
    /// <summary>
    ///  While playing the game, players would make moves. This class contains the information of those moves.
    /// </summary>
    internal class MoveInformation
    {
        /// <summary>
        ///  Gets or sets the opponent name. The name of opponent who has to make the next move.
        /// </summary>
        public string OpponentName { get; set; }

        /// <summary>
        /// Gets or sets the player who made the move.
        /// </summary>
        public string MoveMadeBy { get; set; }

        /// <summary>
        ///  Gets or sets the image position. The position in the game board (0-8) where the player placed his image.
        /// </summary>
        public int ImagePosition { get; set; }

        /// <summary>
        /// Gets or sets the image. The image of the player that he placed in the board (0-8)
        /// </summary>
        public string Image { get; set; }
    }
}