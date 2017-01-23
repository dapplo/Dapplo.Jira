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
	///     The methods of the work (log) domain
	/// </summary>
	public interface IWorkApi
	{
		/// <summary>
		///     Log work for the specified issue
		/// </summary>
		/// <param name="issueKey">key for the issue</param>
		/// <param name="worklog">Worklog with the work which needs to be logged</param>
		/// <param name="adjustEstimate">allows you to provide specific instructions to update the remaining time estimate of the issue.</param>
		/// <param name="adjustValue">e.g. "2d".
		///     When "new" is selected for adjustEstimate the new value for the remaining estimate field.
		///     When "manual" is selected for adjustEstimate the amount to reduce the remaining estimate by.
		/// </param>
		/// <param name="cancellationToken">CancellationToken</param>
		Task LogWorkAsync(string issueKey, Worklog worklog, AdjustEstimate adjustEstimate = AdjustEstimate.Auto, string adjustValue = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Update work log for the specified issue
		/// </summary>
		/// <param name="issueKey">key for the issue</param>
		/// <param name="worklog">Worklog with the work which needs to be updated</param>
		/// <param name="adjustEstimate">allows you to provide specific instructions to update the remaining time estimate of the issue.</param>
		/// <param name="adjustValue">e.g. "2d".
		///     When "new" is selected for adjustEstimate the new value for the remaining estimate field.
		///     When "manual" is selected for adjustEstimate the amount to reduce the remaining estimate by.
		/// </param>
		/// <param name="cancellationToken">CancellationToken</param>
		Task UpdateLoggedWorkAsync(string issueKey, Worklog worklog, AdjustEstimate adjustEstimate = AdjustEstimate.Auto, string adjustValue = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Get worklogs information
		/// </summary>
		/// <param name="issueKey">the issue key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Worklogs</returns>
		Task<Worklogs> GetAsync(string issueKey, CancellationToken cancellationToken = default(CancellationToken));
	}
}