#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016 Dapplo
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
// along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.be 

#endregion

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;
using Dapplo.Log;

namespace Dapplo.Jira.Internal
{
	/// <summary>
	/// This holds all the issue related methods
	/// </summary>
	internal class IssueApi : IIssueApi
	{
		private static readonly LogSource Log = new LogSource();
		private readonly JiraApi _jiraApi;

		internal IssueApi(JiraApi jiraApi)
		{
			_jiraApi = jiraApi;
		}

		/// <inheritdoc />
		public async Task AddCommentAsync(string issueKey, string body, string visibility = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			Log.Debug().WriteLine("Adding comment to {0}", issueKey);
			var comment = new Comment
			{
				Body = body,
				Visibility = visibility == null ? null : new Visibility
				{
					Type = "role",
					Value = visibility
				}
			};
			_jiraApi.Behaviour.MakeCurrent();
			var attachUri = _jiraApi.JiraRestUri.AppendSegments("issue", issueKey, "comment");
			await attachUri.PostAsync(comment, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<Issue> GetAsync(string issueKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			Log.Debug().WriteLine("Retrieving issue information for {0}", issueKey);
			var issueUri = _jiraApi.JiraRestUri.AppendSegments("issue", issueKey);
			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetIssue?.Length > 0)
			{
				issueUri = issueUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetIssue));
			}
			_jiraApi.Behaviour.MakeCurrent();

			var response = await issueUri.GetAsAsync<HttpResponse<Issue, Error>>(cancellationToken).ConfigureAwait(false);
			_jiraApi.HandleErrors(response);
			return response.Response;
		}

		/// <inheritdoc />
		public async Task<IList<Transition>> GetPossibleTransitionsAsync(string issueKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			Log.Debug().WriteLine("Retrieving transition information for {0}", issueKey);
			var transitionsUri = _jiraApi.JiraRestUri.AppendSegments("issue", issueKey, "transitions");
			if (JiraConfig.ExpandGetTransitions?.Length > 0)
			{
				transitionsUri = transitionsUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetTransitions));
			}
			_jiraApi.Behaviour.MakeCurrent();
			var response = await transitionsUri.GetAsAsync<HttpResponse<Transitions, Error>>(cancellationToken).ConfigureAwait(false);
			_jiraApi.HandleErrors(response);
			return response.Response.Items;
		}

		/// <inheritdoc />
		public async Task<SearchResult> SearchAsync(string jql, int maxResults = 20, IEnumerable<string> fields = null,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (jql == null)
			{
				throw new ArgumentNullException(nameof(jql));
			}

			Log.Debug().WriteLine("Searching via JQL: {0}", jql);

			_jiraApi.Behaviour.MakeCurrent();
			var search = new Search
			{
				Jql = jql,
				ValidateQuery = true,
				MaxResults = maxResults,
				Fields = fields ?? new List<string>(JiraConfig.SearchFields)
			};
			var searchUri = _jiraApi.JiraRestUri.AppendSegments("search");

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandSearch?.Length > 0)
			{
				searchUri = searchUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandSearch));
			}

			var response = await searchUri.PostAsync<HttpResponse<SearchResult, Error>>(search, cancellationToken).ConfigureAwait(false);
			_jiraApi.HandleErrors(response);
			return response.Response;
		}

	}
}
