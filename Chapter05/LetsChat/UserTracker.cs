using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace LetsChat
{
    /// <summary>
    /// The User Tracker class for tracking users that are connected to chat.
    /// </summary>
    public class UserTracker : IUserTracker
    {
        /// <summary>
        /// The private storage for keeping the track of online users connected to chat hub.
        /// We are going to register the User Tracker as singleton, so no need to make it as static as it would be resued once the class is initialized.
        /// </summary>
        private readonly ConcurrentDictionary<HubConnectionContext, UserInformation> onlineUserStore = new ConcurrentDictionary<HubConnectionContext, UserInformation>();

        /// <summary>
        /// Add user to User Tracker data store. This would be called when a user joins the chat hub.
        /// </summary>
        /// <param name="connection">The hub connection context.</param>
        /// <param name="userInfo">The user information</param>
        /// <returns>The task.</returns>
        public async Task AddUserAsync(HubConnectionContext connection, UserInformation userInfo)
        {
            //// Add the connection and user to the local storage.
            onlineUserStore.TryAdd(connection, userInfo);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Gets all the online users (connected to chat hub)
        /// </summary>
        /// <returns>A collection of online user information</returns>
        public async Task<IEnumerable<UserInformation>> GetAllOnlineUsersAsync() => await Task.FromResult(onlineUserStore.Values.AsEnumerable());

        /// <summary>
        /// Removes user from User Tracker data store. This would be called when a user leaves the chat hub.
        /// </summary>
        /// <param name="connection">The hub connection context.</param>
        /// <returns>The task.</returns>
        public async Task RemoveUserAsync(HubConnectionContext connection)
        {
            //// Remove the connection and user from the local storage.
            if (onlineUserStore.TryRemove(connection, out var userInfo))
            {
                await Task.CompletedTask;
            }
        }
    }
}
