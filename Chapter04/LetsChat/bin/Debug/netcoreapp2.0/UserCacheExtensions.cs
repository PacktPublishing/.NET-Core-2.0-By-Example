// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserCacheExtensions.cs" company="GTI">
//   Copyright © 2016 Microsoft.
// </copyright>
// <summary>
//   The class for User Cache Helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace GTI.Repository.Cache.Audit
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using GTI.Common.Constants;
    using Microsoft.Services.Caching;
    using Microsoft.Services.Caching.Contracts;

    /// <summary>
    /// The Engagement Cache Helper.
    /// </summary>
    public static class UserCacheExtensions
    {
        /// <summary>
        /// Clears cached engagement by Id. 
        /// </summary>
        /// <param name="cacheProvider">The cache provider.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="engagementId">The engagement Id.</param>
        /// <returns> The task.</returns>
        public static async Task ClearUserRolePermissionByEngagementIdCacheAsync(this ICacheProvider cacheProvider, string userName, string engagementId)
        {
            IEnumerable<string> keys = cacheProvider.GetKeys(nameof(UserCacheRepository), nameof(UserCacheRepository.GetUserRolePermissionByEngagementId), new List<dynamic>() { userName, engagementId });
            if (keys.Any())
            {
                await cacheProvider.BulkRemoveAsync(keys).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Clears cached engagement by Id.
        /// </summary>
        /// <param name="cacheProvider">The cache provider.</param>
        /// <param name="engagementId">The engagement Id.</param>
        /// <returns>
        /// The task.
        /// </returns>
        public static async Task ClearEngagementRolePermissionByEngagementIdCacheAsync(this ICacheProvider cacheProvider, string engagementId)
        {
            IEnumerable<string> keys = cacheProvider.GetKeys(nameof(UserCacheRepository), nameof(UserCacheRepository.GetSecurableRolePermissionByEngagementId), new List<dynamic>() { engagementId });
            if (keys.Any())
            {
                await cacheProvider.BulkRemoveAsync(keys).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Clears All the users from cache.
        /// </summary>
        /// <param name="cacheProvider">The cache provider.</param>
        /// <returns>The task.</returns>
        public static async Task ClearAllUsersCacheAsync(this ICacheProvider cacheProvider)
        {
            IEnumerable<string> keys = cacheProvider.GetKeys(nameof(UserCacheRepository), nameof(UserCacheRepository.GetAll));
            if (keys.Any())
            {
                await cacheProvider.BulkRemoveAsync(keys).ConfigureAwait(false);
            }
        }
    }
}
