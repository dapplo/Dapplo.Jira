#region Dapplo 2017 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Jira
// 
// Dapplo.Jira is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Jira is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Domains;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Internal;
using Dapplo.Log;

#endregion

namespace Dapplo.Jira
{
    /// <summary>
    ///     This holds all the user related extension methods
    /// </summary>
    public static class UserExtensions
    {
        private static readonly LogSource Log = new LogSource();

        /// <summary>
        ///     Get user information
        ///     See: https://docs.atlassian.com/jira/REST/latest/#d2e5339
        /// </summary>
        /// <param name="jiraClient">IWorkDomain to bind the extension method to</param>
        /// <param name="username"></param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>User</returns>
        public static async Task<User> GetAsync(this IUserDomain jiraClient, string username, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }
            Log.Debug().WriteLine("Retrieving user {0}", username);

            var userUri = jiraClient.JiraRestUri.AppendSegments("user").ExtendQuery("username", username);
            jiraClient.Behaviour.MakeCurrent();

            var response = await userUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Returns a list of users that match the search string.
        ///     This resource cannot be accessed anonymously.
        ///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/user-findUsers
        /// </summary>
        /// <param name="jiraClient">IWorkDomain to bind the extension method to</param>
        /// <param name="query">A query string used to search username, name or e-mail address</param>
        /// <param name="startAt"></param>
        /// <param name="maxResults">Maximum number of results returned, default is 20</param>
        /// <param name="includeActive">If true, then active users are included in the results (default true)</param>
        /// <param name="includeInactive">If true, then inactive users are included in the results (default false)</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>SearchResult</returns>
        public static async Task<IList<User>> SearchAsync(this IUserDomain jiraClient, string query, bool includeActive = true, bool includeInactive = false, int startAt = 0,
            int maxResults = 20, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            Log.Debug().WriteLine("Search user {0}", query);

            jiraClient.Behaviour.MakeCurrent();
            var searchUri = jiraClient.JiraRestUri.AppendSegments("user", "search").ExtendQuery(new Dictionary<string, object>
            {
                {
                    "username", query
                },
                {
                    "includeActive", includeActive
                },
                {
                    "includeInactive", includeInactive
                },
                {
                    "startAt", startAt
                },
                {
                    "maxResults", maxResults
                }
            });

            var response = await searchUri.GetAsAsync<HttpResponse<IList<User>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get currrent user information
        ///     See: https://docs.atlassian.com/jira/REST/latest/#d2e4253
        /// </summary>
        /// <param name="jiraClient">IWorkDomain to bind the extension method to</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>User</returns>
        public static async Task<User> GetMyselfAsync(this IUserDomain jiraClient, CancellationToken cancellationToken = default(CancellationToken))
        {
            Log.Debug().WriteLine("Retieving who I am");

            var myselfUri = jiraClient.JiraRestUri.AppendSegments("myself");
            jiraClient.Behaviour.MakeCurrent();
            var response = await myselfUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }
    }
}