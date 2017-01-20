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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;

#endregion

namespace Dapplo.Jira
{
	/// <summary>
	///     The methods of the user domain
	/// </summary>
	public interface IUserApi
	{
		/// <summary>
		///     Get user information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e5339
		/// </summary>
		/// <param name="username"></param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>User</returns>
		Task<User> GetAsync(string username, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Returns a list of users that match the search string.
		///     This resource cannot be accessed anonymously.
		///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/user-findUsers
		/// </summary>
		/// <param name="query">A query string used to search username, name or e-mail address</param>
		/// <param name="startAt"></param>
		/// <param name="maxResults">Maximum number of results returned, default is 20</param>
		/// <param name="includeActive">If true, then active users are included in the results (default true)</param>
		/// <param name="includeInactive">If true, then inactive users are included in the results (default false)</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>SearchResult</returns>
		Task<IList<User>> SearchAsync(string query, bool includeActive = true, bool includeInactive = false, int startAt = 0,
			int maxResults = 20, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Get currrent user information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e4253
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>User</returns>
		Task<User> GetMyselfAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}