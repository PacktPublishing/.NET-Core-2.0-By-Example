using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LetsChat
{
    /// <summary>
    /// The Helper class.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Gets the user information from the Hub caller context.
        /// </summary>
        /// <param name="context">The Hub Caller Context.</param>
        /// <returns>The user Information.</returns>
        public static UserInformation GetUserInformationFromContext(HubCallerContext context)
        {
            Claim nameIdentifierClaim = context.User.Claims.FirstOrDefault(j => j.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            var userId = nameIdentifierClaim.Value;
            var imageUrl = $"https://graph.facebook.com/{userId}/picture?type=square";
            return new UserInformation(context.ConnectionId, context.User.Identity.Name, imageUrl);
        }
    }
}
