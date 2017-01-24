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
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;
using Dapplo.Log;
using Dapplo.HttpExtensions.Extensions;

#endregion

namespace Dapplo.Jira
{
	/// <summary>
	///     Marker interface for the work domain
	/// </summary>
	public interface IWorkDomain : IJiraDomain
	{
	}

	/// <summary>
	///     This holds all the work log related extension methods
	/// </summary>
	public static class WorkExtensions
	{
		private static readonly LogSource Log = new LogSource();

		/// <summary>
		///     Clone a worklog to tran
		/// </summary>
		public static Worklog CloneForTransport(this Worklog worklog)
		{
			var worklogCopy = new Worklog
			{
				Comment = worklog.Comment,
				TimeSpentSeconds = worklog.TimeSpentSeconds,
				Visibility = worklog.Visibility
			};
			return worklogCopy;
		}
		/// <summary>
		///     Get worklogs information
		/// </summary>
		/// <param name="jiraClient">IWorkDomain to bind the extension method to</param>
		/// <param name="issueKey">the issue key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Worklogs</returns>
		public static async Task<Worklogs> GetAsync(this IWorkDomain jiraClient, string issueKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			Log.Debug().WriteLine("Retrieving worklogs information for {0}", issueKey);
			var worklogUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "worklog");
			jiraClient.Behaviour.MakeCurrent();

			var response = await worklogUri.GetAsAsync<HttpResponse<Worklogs, Error>>(cancellationToken).ConfigureAwait(false);
			jiraClient.HandleErrors(response);
			return response.Response;
		}

		/// <summary>
		///     Log work for the specified issue
		/// </summary>
		/// <param name="jiraClient">IWorkDomain to bind the extension method to</param>
		/// <param name="issueKey">key for the issue</param>
		/// <param name="worklog">Worklog with the work which needs to be logged</param>
		/// <param name="adjustEstimate">allows you to provide specific instructions to update the remaining time estimate of the issue.</param>
		/// <param name="adjustValue">e.g. "2d".
		///     When "new" is selected for adjustEstimate the new value for the remaining estimate field.
		///     When "manual" is selected for adjustEstimate the amount to reduce the remaining estimate by.
		/// </param>
		/// <param name="cancellationToken">CancellationToken</param>
		public static async Task<Worklog> CreateAsync(this IWorkDomain jiraClient, string issueKey, Worklog worklog, AdjustEstimate adjustEstimate = AdjustEstimate.Auto, string adjustValue = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			if (worklog == null)
			{
				throw new ArgumentNullException(nameof(worklog));
			}

			var worklogUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "worklog");
			if (adjustEstimate != AdjustEstimate.Auto)
			{
				worklogUri = worklogUri.ExtendQuery("adjustEstimate", adjustEstimate.EnumValueOf());
				switch (adjustEstimate)
				{
					case AdjustEstimate.Manual:
						worklogUri = worklogUri.ExtendQuery("reduceBy", adjustValue);
						break;
					case AdjustEstimate.New:
						worklogUri = worklogUri.ExtendQuery("newEstimate", adjustValue);
						break;
				}
			}

			jiraClient.Behaviour.MakeCurrent();

			var response = await worklogUri.PostAsync<HttpResponse<Worklog>>(worklog.CloneForTransport(), cancellationToken).ConfigureAwait(false);
			if (response.StatusCode != HttpStatusCode.Created)
			{
				throw new Exception(response.StatusCode.ToString());
			}
			return response.Response;
		}

		/// <summary>
		///     Update work log for the specified issue
		/// </summary>
		/// <param name="jiraClient">IWorkDomain to bind the extension method to</param>
		/// <param name="issueKey">key for the issue</param>
		/// <param name="worklog">Worklog with the work which needs to be updated</param>
		/// <param name="adjustEstimate">allows you to provide specific instructions to update the remaining time estimate of the issue.</param>
		/// <param name="adjustValue">e.g. "2d".
		///     When "new" is selected for adjustEstimate the new value for the remaining estimate field.
		///     When "manual" is selected for adjustEstimate the amount to reduce the remaining estimate by.
		/// </param>
		/// <param name="cancellationToken">CancellationToken</param>
		public static async Task UpdateAsync(this IWorkDomain jiraClient, string issueKey, Worklog worklog, AdjustEstimate adjustEstimate = AdjustEstimate.Auto, string adjustValue = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			if (worklog == null)
			{
				throw new ArgumentNullException(nameof(worklog));
			}

			var worklogUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "worklog", worklog.Id);
			if (adjustEstimate != AdjustEstimate.Auto)
			{
				worklogUri = worklogUri.ExtendQuery("adjustEstimate", adjustEstimate.EnumValueOf());
				switch (adjustEstimate)
				{
					case AdjustEstimate.Manual:
						worklogUri = worklogUri.ExtendQuery("reduceBy", adjustValue);
						break;
					case AdjustEstimate.New:
						worklogUri = worklogUri.ExtendQuery("newEstimate", adjustValue);
						break;
				}
			}

			jiraClient.Behaviour.MakeCurrent();

			var response = await worklogUri.PutAsync<HttpResponse<Error>>(worklog.CloneForTransport(), cancellationToken).ConfigureAwait(false);
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new Exception(response.StatusCode.ToString());
			}
		}

		/// <summary>
		/// Delete the spefified Worklog
		/// </summary>
		/// <param name="jiraClient">IWorkDomain to bind the extension method to</param>
		/// <param name="issueKey">Key of the issue to delete to worklog for</param>
		/// <param name="worklog">Worklog to delete</param>
		/// <param name="adjustEstimate">allows you to provide specific instructions to update the remaining time estimate of the issue.</param>
		/// <param name="adjustValue">e.g. "2d".
		///     When "new" is selected for adjustEstimate the new value for the remaining estimate field.
		///     When "manual" is selected for adjustEstimate the amount to reduce the remaining estimate by.
		/// </param>
		/// <param name="cancellationToken">CancellationToken</param>
		public static async Task DeleteAsync(this IWorkDomain jiraClient, string issueKey, Worklog worklog, AdjustEstimate adjustEstimate = AdjustEstimate.Auto, string adjustValue = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			if (worklog == null)
			{
				throw new ArgumentNullException(nameof(worklog));
			}
			var worklogUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "worklog", worklog.Id);
			if (adjustEstimate != AdjustEstimate.Auto)
			{
				worklogUri = worklogUri.ExtendQuery("adjustEstimate", adjustEstimate.EnumValueOf());
				switch (adjustEstimate)
				{
					case AdjustEstimate.Manual:
						worklogUri = worklogUri.ExtendQuery("reduceBy", adjustValue);
						break;
					case AdjustEstimate.New:
						worklogUri = worklogUri.ExtendQuery("newEstimate", adjustValue);
						break;
				}
			}

			jiraClient.Behaviour.MakeCurrent();

			var response = await worklogUri.DeleteAsync<HttpResponse>(cancellationToken).ConfigureAwait(false);
			if (response.StatusCode != HttpStatusCode.NoContent)
			{
				throw new Exception(response.StatusCode.ToString());
			}
		}
	}

}