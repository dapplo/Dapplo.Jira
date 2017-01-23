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
using Dapplo.HttpExtensions.Extensions;
using Dapplo.Jira.Entities;
using Dapplo.Log;

#endregion

namespace Dapplo.Jira.Internal
{
	/// <summary>
	///     This holds all the work related methods
	/// </summary>
	internal class WorkApi : IWorkApi
	{
		private static readonly LogSource Log = new LogSource();
		private readonly JiraApi _jiraApi;

		internal WorkApi(JiraApi jiraApi)
		{
			_jiraApi = jiraApi;
		}

		/// <inheritdoc />
		public async Task<Worklogs> GetAsync(string issueKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			Log.Debug().WriteLine("Retrieving worklogs information for {0}", issueKey);
			var worklogUri = _jiraApi.JiraRestUri.AppendSegments("issue", issueKey, "worklog");
			_jiraApi.Behaviour.MakeCurrent();

			var response = await worklogUri.GetAsAsync<HttpResponse<Worklogs, Error>>(cancellationToken).ConfigureAwait(false);
			_jiraApi.HandleErrors(response);
			return response.Response;
		}

		/// <inheritdoc />
		public async Task<Worklog> CreateAsync(string issueKey, Worklog worklog, AdjustEstimate adjustEstimate = AdjustEstimate.Auto, string adjustValue = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			if (worklog == null)
			{
				throw new ArgumentNullException(nameof(worklog));
			}
			var worklogCopy = new Worklog
			{
				Comment = worklog.Comment,
				TimeSpentSeconds = worklog.TimeSpentSeconds,
				Visibility = worklog.Visibility
			};
			var worklogUri = _jiraApi.JiraRestUri.AppendSegments("issue", issueKey, "worklog");
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

			_jiraApi.Behaviour.MakeCurrent();

			var response = await worklogUri.PostAsync<HttpResponse<Worklog>>(worklogCopy, cancellationToken).ConfigureAwait(false);
			if (response.StatusCode != HttpStatusCode.Created)
			{
				throw new Exception(response.StatusCode.ToString());
			}
			return response.Response;
		}

		/// <inheritdoc />
		public async Task UpdateAsync(string issueKey, Worklog worklog, AdjustEstimate adjustEstimate = AdjustEstimate.Auto, string adjustValue = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			if (worklog == null)
			{
				throw new ArgumentNullException(nameof(worklog));
			}
			var worklogCopy = new Worklog
			{
				Comment = worklog.Comment,
				TimeSpentSeconds = worklog.TimeSpentSeconds,
				Visibility = worklog.Visibility
			};
			var worklogUri = _jiraApi.JiraRestUri.AppendSegments("issue", issueKey, "worklog", worklog.Id);
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

			_jiraApi.Behaviour.MakeCurrent();

			var response = await worklogUri.PutAsync<HttpResponse<Error>>(worklogCopy, cancellationToken).ConfigureAwait(false);
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new Exception(response.StatusCode.ToString());
			}
		}

		/// <inheritdoc />
		public async Task DeleteAsync(string issueKey, Worklog worklog, AdjustEstimate adjustEstimate = AdjustEstimate.Auto, string adjustValue = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			if (worklog == null)
			{
				throw new ArgumentNullException(nameof(worklog));
			}
			var worklogUri = _jiraApi.JiraRestUri.AppendSegments("issue", issueKey, "worklog", worklog.Id);
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

			_jiraApi.Behaviour.MakeCurrent();

			var response = await worklogUri.DeleteAsync<HttpResponse>(cancellationToken).ConfigureAwait(false);
			if (response.StatusCode != HttpStatusCode.NoContent)
			{
				throw new Exception(response.StatusCode.ToString());
			}
		}
	}
}