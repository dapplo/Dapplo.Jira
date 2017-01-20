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
using Dapplo.Jira.Query;

#endregion

namespace Dapplo.Jira
{
	/// <summary>
	///     The methods of the issue domain
	/// </summary>
	public interface IIssueApi
	{
		/// <summary>
		///     Add comment to the specified issue
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1139
		/// </summary>
		/// <param name="issueKey">key for the issue</param>
		/// <param name="body">the body of the comment</param>
		/// <param name="visibility">optional visibility role</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Attachment</returns>
		Task AddCommentAsync(string issueKey, string body, string visibility = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Get issue information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e4539
		/// </summary>
		/// <param name="issueKey">the issue key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Issue</returns>
		Task<Issue> GetAsync(string issueKey, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Get possible transitions for the specified issue
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1289
		/// </summary>
		/// <param name="issueKey">the issue key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Issue</returns>
		Task<IList<Transition>> GetPossibleTransitionsAsync(string issueKey, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Search for issues, with a JQL (e.g. from a filter)
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2713
		/// </summary>
		/// <param name="jql">Jira Query Language, like SQL, for the search</param>
		/// <param name="maxResults">Maximum number of results returned, default is 20</param>
		/// <param name="fields">Jira fields to include, if null the defaults from the JiraConfig.SearchFields are taken</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>SearchResult</returns>
		Task<SearchResult> SearchAsync(string jql, int maxResults = 20, IEnumerable<string> fields = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Search for issues, with a JQL (e.g. from a filter)
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2713
		/// </summary>
		/// <param name="jql">Jira Query Language, like SQL, for the search. Use Where builder</param>
		/// <param name="maxResults">Maximum number of results returned, default is 20</param>
		/// <param name="fields">Jira fields to include, if null the defaults from the JiraConfig.SearchFields are taken</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>SearchResult</returns>
		Task<SearchResult> SearchAsync(IFinalClause jql, int maxResults = 20, IEnumerable<string> fields = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Update comment
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1139
		/// </summary>
		/// <param name="issueKey">jira key to which the comment belongs</param>
		/// <param name="comment">Comment to update</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Attachment</returns>
		Task UpdateCommentAsync(string issueKey, Comment comment, CancellationToken cancellationToken = default(CancellationToken));


		/// <summary>
		///     Get a list of all possible issue types
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>List with IssueType elements</returns>
		Task<IList<IssueType>> GetIssueTypesAsync(CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Create an issue
		/// </summary>
		/// <param name="issue">the issue to create</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Issue</returns>
		Task<Issue> CreateAsync(Issue issue, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Delete an issue
		/// </summary>
		/// <param name="issueKey">the key of the issue to delete</param>
		/// <param name="deleteSubtasks">true or false (default) indicating that any subtasks should also be deleted.
		/// If the issue has no subtasks this parameter is ignored. If the issue has subtasks and this parameter is missing or false, then the issue will not be deleted and an error will be returned</param>
		/// <param name="cancellationToken">CancellationToken</param>
		Task DeleteAsync(string issueKey, bool deleteSubtasks = false, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// 
		/// </summary>
		/// <param name="issueKey">Key for the issue to assign</param>
		/// <param name="user">User to assign to, use User.Nobody to remove the assignee or User.Default to automaticly assign</param>
		/// <param name="cancellationToken"></param>
		Task AssignAsync(string issueKey, User user, CancellationToken cancellationToken = default(CancellationToken));
	}
}