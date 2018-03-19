using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetsChat
{
    /// <summary>
    /// Contract for user tracking.
    /// </summary>
    public interface IUserTracker
    {
        /// <summary>
        /// Gets all the online users (connected to chat hub)
        /// </summary>
        /// <returns>A collection of online user information</returns>
        Task<IEnumerable<UserInformation>> GetAllOnlineUsersAsync();
       
        /// <summary>
        /// Add user to User Tracker data store. This would be called when a user joins the chat hub.
        /// </summary>
        /// <param name="connection">The hub connection context.</param>
        /// <param name="userInfo">The user information</param>
        /// <returns>The task.</returns>
        Task AddUserAsync(HubConnectionContext connection, UserInformation userInfo);

        /// <summary>
        /// Removes user from User Tracker data store. This would be called when a user leaves the chat hub.
        /// </summary>
        /// <param name="connection">The hub connection context.</param>
        /// <returns>The task.</returns>
        Task RemoveUserAsync(HubConnectionContext connection);
    }
}