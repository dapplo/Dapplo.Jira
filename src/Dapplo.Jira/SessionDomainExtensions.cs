// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Dapplo.Jira;

/// <summary>
///     This holds all the user session extension methods
/// </summary>
public static class SessionDomainExtensions
{
#pragma warning disable IDE0090 // Use 'new(...)'
    private static readonly LogSource Log = new LogSource();
#pragma warning restore IDE0090 // Use 'new(...)'

    /// <summary>
    ///     Starts new session. No additional authorization required.
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
    [Obsolete("This method is deprecated, see https://developer.atlassian.com/cloud/jira/platform/deprecation-notice-basic-auth-and-cookie-based-auth/")]
    public static async Task<LoginInfo> StartAsync(this ISessionDomain jiraClient, string username, string password, CancellationToken cancellationToken = default)
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
            throw new InvalidOperationException($"Cookies need to be enabled, set {nameof(IHttpSettings.UseCookies)} to true");
        }

        Log.Debug().WriteLine("Starting a session for {0}", username);

        var sessionUri = jiraClient.JiraAuthUri.AppendSegments("session");

        jiraClient.Behaviour.MakeCurrent();

        var credentials = new { username, password };
        var json = JsonSerializer.Serialize(credentials, JiraJsonSerializerOptions.Default);
        var content = new StringContent(json);
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
            var response = await sessionUri.DeleteAsync<HttpResponse>(cancellationToken).ConfigureAwait(false);

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
