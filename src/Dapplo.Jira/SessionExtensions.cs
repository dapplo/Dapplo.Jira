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
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    ///     This holds all the user session extension methods
    /// </summary>
    public static class SessionExtensions
    {
        private static readonly LogSource Log = new LogSource();

        /// <summary>
        ///     Starts new session. No additional authorization requered.
        /// </summary>
        /// <remarks>
        ///     Please be aware that although cookie-based authentication has many benefits, such as performance (not having to
        ///     make multiple authentication calls), the session cookie can expire..
        /// </remarks>
        /// <param name="jiraClient">ISessionDomain to bind the extension method to</param>
        /// <param name="username">User username</param>
        /// <param name="password">User password</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>LoginInfo</returns>
        public static async Task<LoginInfo> StartAsync(this ISessionDomain jiraClient, string username, string password,
            CancellationToken cancellationToken = default)
        {
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (!jiraClient.Behaviour.HttpSettings.UseCookies)
            {
                throw new ArgumentException("Cookies need to be enabled", nameof(IHttpSettings.UseCookies));
            }
            Log.Debug().WriteLine("Starting a session for {0}", username);

            var sessionUri = jiraClient.JiraAuthUri.AppendSegments("session");

            jiraClient.Behaviour.MakeCurrent();

            var content = new StringContent($"{{ \"username\": \"{username}\", \"password\": \"{password}\"}}");
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var response = await sessionUri.PostAsync<HttpResponse<SessionResponse, Error>>(content, cancellationToken);
            return response.HandleErrors().LoginInfo;
        }

        /// <summary>
        ///     Ends session. No additional authorization required.
        /// </summary>
        /// <param name="jiraClient">ISessionDomain to bind the extension method to</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task EndAsync(this ISessionDomain jiraClient, CancellationToken cancellationToken = default)
        {
            // Find the cookie to expire
            var sessionCookies = jiraClient.Behaviour.CookieContainer.GetCookies(jiraClient.JiraBaseUri).Cast<Cookie>().ToList();

            Log.Debug().WriteLine("Ending session");

            // check if a cookie was found, if not skip the end session
            if (sessionCookies.Any())
            {
                if (Log.IsDebugEnabled())
                {
                    Log.Debug().WriteLine("Found {0} cookies to invalidate", sessionCookies.Count);
                    foreach (var sessionCookie in sessionCookies)
                    {
                        Log.Debug().WriteLine("Found cookie {0} for domain {1} which expires on {2}", sessionCookie.Name, sessionCookie.Domain, sessionCookie.Expires);
                    }
                }
                var sessionUri = jiraClient.JiraAuthUri.AppendSegments("session");

                jiraClient.Behaviour.MakeCurrent();
                var response = await sessionUri.DeleteAsync<HttpResponseMessage>(cancellationToken).ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    Log.Warn().WriteLine("Failed to close jira session. Status code: {0} ", response.StatusCode);
                }
                // Expire the cookie, no mather what the return code was.
                foreach (var sessionCookie in sessionCookies)
                {
                    sessionCookie.Expired = true;
                }
            }
        }
    }
}