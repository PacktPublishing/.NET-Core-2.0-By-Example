namespace LetsChat
{
    /// <summary>
    /// The class to hold the user information.
    /// </summary>
    public class UserInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserInformation"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="name">The name of user.</param>
        /// <param name="imageUrl">The url of user's profile picture.</param>
        public UserInformation(string connectionId, string name, string imageUrl)
        {
            this.ConnectionId = connectionId;
            this.Name = name;
            this.ImageUrl = imageUrl;
        }

        /// <summary>
        /// Gets the image path of the user
        /// </summary>
        public string ImageUrl { get; }

        /// <summary>
        /// Gets the connection identifier.
        /// </summary>
        public string ConnectionId { get; }

        /// <summary>
        /// Gets the name of user.
        /// </summary>
        public string Name { get; }
    }
}