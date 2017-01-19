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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;
using Dapplo.Log;

#endregion

namespace Dapplo.Jira.Internal
{
	/// <summary>
	///     This holds all the user related methods
	/// </summary>
	internal class UserApi : IUserApi
	{
		private static readonly LogSource Log = new LogSource();
		private readonly JiraApi _jiraApi;

		internal UserApi(JiraApi jiraApi)
		{
			_jiraApi = jiraApi;
		}

		/// <inheritdoc />
		public async Task<User> GetAsync(string username, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (username == null)
			{
				throw new ArgumentNullException(nameof(username));
			}
			Log.Debug().WriteLine("Retrieving user {0}", username);

			var userUri = _jiraApi.JiraRestUri.AppendSegments("user").ExtendQuery("username", username);
			_jiraApi.Behaviour.MakeCurrent();

			var response = await userUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
			return _jiraApi.HandleErrors(response);
		}

		/// <inheritdoc />
		public async Task<IList<User>> SearchAsync(string query, bool includeActive = true, bool includeInactive = false, int startAt = 0,
			int maxResults = 20, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (query == null)
			{
				throw new ArgumentNullException(nameof(query));
			}
			Log.Debug().WriteLine("Search user via {0}", query);

			_jiraApi.Behaviour.MakeCurrent();
			var searchUri = _jiraApi.JiraRestUri.AppendSegments("user", "search").ExtendQuery(new Dictionary<string, object>
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
			return _jiraApi.HandleErrors(response);
		}

		/// <inheritdoc />
		public async Task<User> WhoAmIAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			Log.Debug().WriteLine("Retieving who I am");

			var myselfUri = _jiraApi.JiraRestUri.AppendSegments("myself");
			_jiraApi.Behaviour.MakeCurrent();
			var response = await myselfUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
			return _jiraApi.HandleErrors(response);
		}
	}
}