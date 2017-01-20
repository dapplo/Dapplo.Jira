//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Jira
// 
//  Dapplo.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;
using Dapplo.Log;

#endregion

namespace Dapplo.Jira.Internal
{
	/// <summary>
	///     Session API
	/// </summary>
	internal class SessionApi : ISessionApi
	{
		private static readonly LogSource Log = new LogSource();

		private readonly JiraApi _jiraApi;

		internal SessionApi(JiraApi jiraApi)
		{
			_jiraApi = jiraApi;
		}

		/// <inheritdoc />
		public async Task<LoginInfo> StartAsync(string username, string password, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (username == null)
			{
				throw new ArgumentNullException(nameof(username));
			}
			if (password == null)
			{
				throw new ArgumentNullException(nameof(password));
			}
			if (!_jiraApi.Behaviour.HttpSettings.UseCookies)
			{
				throw new ArgumentException("Cookies need to be enabled", nameof(IHttpSettings.UseCookies));
			}
			Log.Debug().WriteLine("Starting a session for {0}", username);

			var sessionUri = _jiraApi.JiraAuthUri.AppendSegments("session");

			_jiraApi.Behaviour.MakeCurrent();

			var content = new StringContent($"{{ \"username\": \"{username}\", \"password\": \"{password}\"}}");
			content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

			var response = await sessionUri.PostAsync<HttpResponse<SessionResponse, Error>>(content, cancellationToken);

			_jiraApi.HandleErrors(response);

			return response.Response.LoginInfo;
		}

		/// <inheritdoc />
		public async Task EndAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			// Find the cookie to expire
			var sessionCookies = _jiraApi.Behaviour.CookieContainer.GetCookies(_jiraApi.JiraBaseUri).Cast<Cookie>().ToList();

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
				var sessionUri = _jiraApi.JiraAuthUri.AppendSegments("session");

				_jiraApi.Behaviour.MakeCurrent();
				var response = await sessionUri.DeleteAsync<HttpResponseMessage>(cancellationToken);

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