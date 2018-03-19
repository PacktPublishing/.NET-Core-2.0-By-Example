using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;


namespace LetsChat
{
    /// <summary>
    /// The Chat hub class.
    /// </summary>
    [Authorize]
    public class ChatHub : Hub
    {
        /// <summary>
        /// The user tracker to keep track of online users.
        /// </summary>
        private IUserTracker userTracker;

        /// <summary>
        ///  Initializes a new instance of the <see cref="ChatHub"/> class.
        /// </summary>
        /// <param name="userTracker">The user tracker.</param>
        public ChatHub(IUserTracker userTracker)            
        {
            this.userTracker = userTracker;
        }

        /// <summary>
        /// Gets all the connected user list.
        /// </summary>
        /// <returns>The collection of online users.</returns>
        public async Task<IEnumerable<UserInformation>> GetOnlineUsersAsync()
        {
            return await userTracker.GetAllOnlineUsersAsync();
        }

        /// <summary>
        /// Fires on client connected.
        /// </summary>
        /// <returns>The task.</returns>
        public override async Task OnConnectedAsync()
        {
            var user = Helper.GetUserInformationFromContext(Context);
            await this.userTracker.AddUserAsync(Context.Connection, user);
            await Clients.All.InvokeAsync("UsersJoined", new UserInformation[] { user });
            //// On connection, refresh online list.
            await Clients.All.InvokeAsync("SetUsersOnline", await GetOnlineUsersAsync());
           
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Fires when client disconnects.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>The task.</returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = Helper.GetUserInformationFromContext(Context);
            await Clients.All.InvokeAsync("UsersLeft", new UserInformation[] { user });
            await this.userTracker.RemoveUserAsync(Context.Connection);
            //// On disconnection, refresh online list.
            await Clients.All.InvokeAsync("SetUsersOnline", await GetOnlineUsersAsync());
            await base.OnDisconnectedAsync(exception);
        }
       
        /// <summary>
        /// Sends the message to all the connected clients.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <returns>A task.</returns>
        public async Task Send(string message)
        {
            UserInformation user = Helper.GetUserInformationFromContext(Context);
            await Clients.All.InvokeAsync("Send", user.Name, message, user.ImageUrl);
        }
    }
}
