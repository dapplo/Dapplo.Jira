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
	///     The methods of the filter domain
	/// </summary>
	public interface IFilterApi
	{
		/// <summary>
		///     Get filter favorites
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1388
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>List of filter</returns>
		Task<IList<Filter>> GetFavoritesAsync(CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Create a filter
		/// </summary>
		/// <param name="filter">Filter to create</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Filter</returns>
		Task<Filter> CreateAsync(Filter filter, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Update a filter
		/// </summary>
		/// <param name="filter">Filter to update</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Filter</returns>
		Task<Filter> UpdateAsync(Filter filter, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Get filter
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1388
		/// </summary>
		/// <param name="id">filter id</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Filter</returns>
		Task<Filter> GetAsync(long id, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Delete filter
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1388
		/// </summary>
		/// <param name="filter">Filter to delete</param>
		/// <param name="cancellationToken">CancellationToken</param>
		Task DeleteAsync(Filter filter, CancellationToken cancellationToken = default(CancellationToken));
	}
}