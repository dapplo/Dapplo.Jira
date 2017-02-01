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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;

#endregion

namespace Dapplo.Jira
{
	/// <summary>
	///     The marker interface for the agile domain
	/// </summary>
	public interface IAgileDomain : IJiraDomain
	{
	}

	/// <summary>
	///     This holds all the issue related extensions methods
	/// </summary>
	public static class AgileExtensions
	{
		/// <summary>
		///     Add comment to the specified issue
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1139
		/// </summary>
		/// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
		/// <param name="issueKey">key for the issue</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>AgileIssue</returns>
		public static async Task<AgileIssue> GetIssueAsync(this IAgileDomain jiraClient, string issueKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			jiraClient.Behaviour.MakeCurrent();
			var agileIssueUri = jiraClient.JiraAgileRestUri.AppendSegments("issue", issueKey);
			if (JiraConfig.ExpandGetIssue?.Length > 0)
			{
				agileIssueUri = agileIssueUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetIssue.Concat(new []{"customfield_10007"})));
			}
			var response = await agileIssueUri.GetAsAsync<HttpResponse<AgileIssue, Error>>(cancellationToken).ConfigureAwait(false);
			return response.HandleErrors();
		}

		/// <summary>
		///     Get all sprints
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1139
		/// </summary>
		/// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
		/// <param name="boardId">Id of the board to get the sprints for</param>
		/// <param name="stateFilter">Filters results to sprints in specified states. Valid values: future, active, closed. You can define multiple states separated by commas, e.g. state=active,closed</param>
		/// <param name="page">optional Pageable</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Results with Sprint objects</returns>
		public static async Task<Results<Sprint>> GetSprintsAsync(this IAgileDomain jiraClient, long boardId, string stateFilter = null, Pageable page = null,CancellationToken cancellationToken = default(CancellationToken))
		{
			jiraClient.Behaviour.MakeCurrent();
			var sprintsUri = jiraClient.JiraAgileRestUri.AppendSegments("board", boardId, "sprint");
			if (page != null)
			{
				sprintsUri = sprintsUri.ExtendQuery(new Dictionary<string, object>
				{
					{
						"startAt", page.StartAt
					},
					{
						"maxResults", page.MaxResults
					}
				});
			}
			if (stateFilter != null)
			{
				sprintsUri = sprintsUri.ExtendQuery("state", stateFilter);
			}
			var response = await sprintsUri.GetAsAsync<HttpResponse<Results<Sprint>, Error>>(cancellationToken).ConfigureAwait(false);
			return response.HandleErrors();
		}

		/// <summary>
		///     Get all boards
		///     See <a href="https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/board-getAllBoards">get all boards</a>
		/// </summary>
		/// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
		/// <param name="type">Filters results to boards of the specified type. Valid values: scrum, kanban.</param>
		/// <param name="name">Filters results to boards that match or partially match the specified name.</param>
		/// <param name="projectKeyOrId">Filters results to boards that are relevant to a project. Relevance means that the jql filter defined in board contains a reference to a project.</param>
		/// <param name="page">optional Pageable</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Results with Board objects</returns>
		public static async Task<Results<Board>> GetBoardsAsync(this IAgileDomain jiraClient, string type = null, string name = null, string projectKeyOrId = null, Pageable page = null,CancellationToken cancellationToken = default(CancellationToken))
		{
			jiraClient.Behaviour.MakeCurrent();
			var boards = jiraClient.JiraAgileRestUri.AppendSegments("board");
			if (page != null)
			{
				boards = boards.ExtendQuery(new Dictionary<string, object>
				{
					{
						"startAt", page.StartAt
					},
					{
						"maxResults", page.MaxResults
					}
				});
			}
			if (type != null)
			{
				boards = boards.ExtendQuery("type", type);
			}
			if (name != null)
			{
				boards = boards.ExtendQuery("name", name);
			}
			if (projectKeyOrId != null)
			{
				boards = boards.ExtendQuery("projectKeyOrId", projectKeyOrId);
			}
			var response = await boards.GetAsAsync<HttpResponse<Results<Board>, Error>>(cancellationToken).ConfigureAwait(false);
			return response.HandleErrors();
		}
	}
}