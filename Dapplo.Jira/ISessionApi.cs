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

using System.Threading;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;

#endregion

namespace Dapplo.Jira
{
	/// <summary>
	///     The methods of the session domain
	/// </summary>
	public interface ISessionApi
	{
		/// <summary>
		///     Starts new session. No additional authorization requered.
		/// </summary>
		/// <remarks>
		///     Please be aware that although cookie-based authentication has many benefits, such as performance (not having to
		///     make multiple authentication calls), the session cookie can expire..
		/// </remarks>
		/// <param name="username">User username</param>
		/// <param name="password">User password</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>LoginInfo</returns>
		Task<LoginInfo> StartAsync(string username, string password, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Ends session. No additional authorization required.
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		Task EndAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}