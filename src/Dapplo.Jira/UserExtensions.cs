// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Domains;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Internal;
using Dapplo.Log;

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
        /// <param name="jiraClient">IUserDomain to bind the extension method to</param>
        /// <param name="username">string</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>User</returns>
        public static async Task<User> GetByUsernameAsync(this IUserDomain jiraClient, string username, CancellationToken cancellationToken = default)
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
        ///     Get user information
        ///     See: https://docs.atlassian.com/jira/REST/latest/#d2e5339
        /// </summary>
        /// <param name="jiraClient">IUserDomain to bind the extension method to</param>
        /// <param name="accountId">accountId</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>User</returns>
        public static async Task<User> GetByAccountIdAsync(this IUserDomain jiraClient, string accountId, CancellationToken cancellationToken = default)
        {
            if (accountId == null)
            {
                throw new ArgumentNullException(nameof(accountId));
            }
            Log.Debug().WriteLine("Retrieving user for accountId {0}", accountId);

            var userUri = jiraClient.JiraRestUri.AppendSegments("user").ExtendQuery("accountId", accountId);
            jiraClient.Behaviour.MakeCurrent();

            var response = await userUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get user information
        ///     See: https://docs.atlassian.com/jira/REST/latest/#d2e5339
        /// </summary>
        /// <param name="jiraClient">IUserDomain to bind the extension method to</param>
        /// <param name="userIdentifier">IUserIdentifier</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>User</returns>
        public static Task<User> GetAsync(this IUserDomain jiraClient, IUserIdentifier userIdentifier, CancellationToken cancellationToken = default)
        {
            if (userIdentifier?.AccountId != null)
            {
                return GetByAccountIdAsync(jiraClient, userIdentifier.AccountId, cancellationToken);
            }
            if (userIdentifier?.Name != null)
            {
                return GetByUsernameAsync(jiraClient, userIdentifier.Name, cancellationToken);
            }
            throw new ArgumentNullException(nameof(userIdentifier), "no users identifier specified");
        }

        /// <summary>
        ///     Returns a list of users that match the search string.
        ///     This resource cannot be accessed anonymously.
        ///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/user-findUsers
        /// </summary>
        /// <param name="jiraClient">IUserDomain to bind the extension method to</param>
        /// <param name="query">A query string used to search name or e-mail address</param>
        /// <param name="startAt">int with the offset used for paged searches</param>
        /// <param name="maxResults">Maximum number of results returned, default is 20</param>
        /// <param name="includeActive">If true, then active users are included in the results (default true)</param>
        /// <param name="includeInactive">If true, then inactive users are included in the results (default false)</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>SearchResults</returns>
        public static async Task<IList<User>> SearchAsync(this IUserDomain jiraClient, string query, bool includeActive = true, bool includeInactive = false, int startAt = 0, int maxResults = 20, CancellationToken cancellationToken = default)
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
                    "query", query
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
        ///     Returns a list of users via a structured query.
        ///     This resource cannot be accessed anonymously.
        ///     See: https://developer.atlassian.com/cloud/jira/platform/rest/v3/#api-rest-api-3-user-search-query-get
        /// </summary>
        /// <param name="jiraClient">IUserDomain to bind the extension method to</param>
        /// <param name="query">A query string used to search username, name or e-mail address</param>
        /// <param name="startAt">int with the offset used for paged searches</param>
        /// <param name="maxResults">Maximum number of results returned, default is 20</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>SearchResults</returns>
        public static async Task<IList<User>> SearchByQueryAsync(this IUserDomain jiraClient, string query, int startAt = 0, int maxResults = 20, CancellationToken cancellationToken = default)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            Log.Debug().WriteLine("Search user by query {0}", query);

            jiraClient.Behaviour.MakeCurrent();
            var searchUri = jiraClient.JiraRestUri.AppendSegments("user", "search", "query").ExtendQuery(new Dictionary<string, object>
            {
                {
                    "query", query
                },
                {
                    "startAt", startAt
                },
                {
                    "maxResults", maxResults
                }
            });

            var response = await searchUri.GetAsAsync<HttpResponse<SearchResult<User, string>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors().Values;
        }

        /// <summary>
        ///     Get current user information
        ///     See: https://docs.atlassian.com/jira/REST/latest/#d2e4253
        /// </summary>
        /// <param name="jiraClient">IUserDomain to bind the extension method to</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>User</returns>
        public static async Task<User> GetMyselfAsync(this IUserDomain jiraClient, CancellationToken cancellationToken = default)
        {
            Log.Debug().WriteLine("Retrieving who I am");

            var myselfUri = jiraClient.JiraRestUri.AppendSegments("myself");
            jiraClient.Behaviour.MakeCurrent();
            var response = await myselfUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        /// Retrieve the assignable users for the specified project or issue
        /// See <a href="https://docs.atlassian.com/jira/REST/cloud/#api/2/user-findAssignableUsers">here</a>
        /// </summary>
        /// <param name="jiraClient">IUserDomain</param>
        /// <param name="username">optional string with a pattern</param>
        /// <param name="projectKey">optional string with the key of the project</param>
        /// <param name="issueKey">optional string with the key of the issue</param>
        /// <param name="startAt">optional int where to start returning the results</param>
        /// <param name="maxResults">optional int with the max of the result</param>
        /// <param name="actionDescriptorId">optional int, not documented what this does</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>IEnumerable with User</returns>
        public static async Task<IEnumerable<User>> GetAssignableUsersAsync(this IUserDomain jiraClient, string username = null, string projectKey = null, string issueKey = null, int? startAt = null, int? maxResults = null, int? actionDescriptorId = null, CancellationToken cancellationToken = default)
        {
            var usersSearchUri = jiraClient.JiraRestUri
                .AppendSegments("user", "assignable", "search");

            if (projectKey != null)
            {
                usersSearchUri = usersSearchUri.ExtendQuery(new Dictionary<string, object>
                {
                    {"project", projectKey}
                });
            }

            if (issueKey != null)
            {
                usersSearchUri = usersSearchUri.ExtendQuery(new Dictionary<string, object>
                {
                    {"issueKey", issueKey}
                });
            }

            if (username != null)
            {
                usersSearchUri = usersSearchUri.ExtendQuery(new Dictionary<string, object>
                {
                    {"username", username}
                });
            }

            if (startAt.HasValue)
            {
                usersSearchUri = usersSearchUri.ExtendQuery(new Dictionary<string, object>
                {
                    {"startAt", startAt.Value}
                });
            }

            if (maxResults.HasValue)
            {
                usersSearchUri = usersSearchUri.ExtendQuery(new Dictionary<string, object>
                {
                    {"maxResults", maxResults.Value}
                });
            }

            if (actionDescriptorId.HasValue)
            {
                usersSearchUri = usersSearchUri.ExtendQuery(new Dictionary<string, object>
                {
                    {"actionDescriptorId", actionDescriptorId.Value}
                });
            }

            jiraClient.Behaviour.MakeCurrent();
            var response = await usersSearchUri.GetAsAsync<HttpResponse<IEnumerable<User>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }
    }
}