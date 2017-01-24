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
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Query;
using Dapplo.Log;

#endregion

namespace Dapplo.Jira
{
	/// <summary>
	///     The marker interface for the issue domain
	/// </summary>
	public interface IIssueDomain : IJiraDomain
	{
	}

	/// <summary>
	///     This holds all the issue related extensions methods
	/// </summary>
	public static class IssueExtensions
	{
		private static readonly LogSource Log = new LogSource();

		/// <summary>
		///     Add comment to the specified issue
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1139
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="issueKey">key for the issue</param>
		/// <param name="body">the body of the comment</param>
		/// <param name="visibility">optional visibility role</param>
		/// <param name="cancellationToken">CancellationToken</param>
		public static async Task AddCommentAsync(this IIssueDomain jiraClient, string issueKey, string body, string visibility = null, CancellationToken cancellationToken = default(CancellationToken))
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
			jiraClient.Behaviour.MakeCurrent();
			var attachUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "comment");
			await attachUri.PostAsync(comment, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Get issue information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e4539
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="issueKey">the issue key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Issue</returns>
		public static async Task<Issue> GetAsync(this IIssueDomain jiraClient, string issueKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			Log.Debug().WriteLine("Retrieving issue information for {0}", issueKey);
			var issueUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey);
			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetIssue?.Length > 0)
			{
				issueUri = issueUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetIssue));
			}
			jiraClient.Behaviour.MakeCurrent();

			var response = await issueUri.GetAsAsync<HttpResponse<Issue, Error>>(cancellationToken).ConfigureAwait(false);
			jiraClient.HandleErrors(response);
			return response.Response;
		}

		/// <summary>
		///     Get possible transitions for the specified issue
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1289
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="issueKey">the issue key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>List of Transition</returns>
		public static async Task<IList<Transition>> GetPossibleTransitionsAsync(this IIssueDomain jiraClient, string issueKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			Log.Debug().WriteLine("Retrieving transition information for {0}", issueKey);
			var transitionsUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "transitions");
			if (JiraConfig.ExpandGetTransitions?.Length > 0)
			{
				transitionsUri = transitionsUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetTransitions));
			}
			jiraClient.Behaviour.MakeCurrent();
			var response = await transitionsUri.GetAsAsync<HttpResponse<Transitions, Error>>(cancellationToken).ConfigureAwait(false);
			jiraClient.HandleErrors(response);
			return response.Response.Items;
		}

		/// <summary>
		///     Search for issues, with a JQL (e.g. from a filter)
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2713
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="jql">Jira Query Language, like SQL, for the search. Use Where builder</param>
		/// <param name="maxResults">Maximum number of results returned, default is 20</param>
		/// <param name="fields">Jira fields to include, if null the defaults from the JiraConfig.SearchFields are taken</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>SearchResult</returns>
		public static async Task<SearchResult> SearchAsync(this IIssueDomain jiraClient, IFinalClause jql, int maxResults = 20, IEnumerable<string> fields = null,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			return await jiraClient.SearchAsync(jql.ToString(), maxResults, fields, cancellationToken);
		}

		/// <summary>
		///     Search for issues, with a JQL (e.g. from a filter)
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2713
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="jql">Jira Query Language, like SQL, for the search</param>
		/// <param name="maxResults">Maximum number of results returned, default is 20</param>
		/// <param name="fields">Jira fields to include, if null the defaults from the JiraConfig.SearchFields are taken</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>SearchResult</returns>
		public static async Task<SearchResult> SearchAsync(this IIssueDomain jiraClient, string jql, int maxResults = 20, IEnumerable<string> fields = null,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (jql == null)
			{
				throw new ArgumentNullException(nameof(jql));
			}

			Log.Debug().WriteLine("Searching via JQL: {0}", jql);

			jiraClient.Behaviour.MakeCurrent();
			var search = new Search
			{
				Jql = jql,
				ValidateQuery = true,
				MaxResults = maxResults,
				Fields = fields ?? new List<string>(JiraConfig.SearchFields)
			};
			var searchUri = jiraClient.JiraRestUri.AppendSegments("search");

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandSearch?.Length > 0)
			{
				searchUri = searchUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandSearch));
			}

			var response = await searchUri.PostAsync<HttpResponse<SearchResult, Error>>(search, cancellationToken).ConfigureAwait(false);
			jiraClient.HandleErrors(response);
			return response.Response;
		}

		/// <summary>
		///     Update comment
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1139
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="issueKey">jira key to which the comment belongs</param>
		/// <param name="comment">Comment to update</param>
		/// <param name="cancellationToken">CancellationToken</param>
		public static async Task UpdateCommentAsync(this IIssueDomain jiraClient, string issueKey, Comment comment, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}

			Log.Debug().WriteLine("Updating comment {0} for issue {1}", comment.Id, issueKey);

			jiraClient.Behaviour.MakeCurrent();

			var attachUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "comment", comment.Id);
			await attachUri.PutAsync(comment, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Get a list of all possible issue types
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>List with IssueType elements</returns>
		public static async Task<IList<IssueType>> GetIssueTypesAsync(this IIssueDomain jiraClient, CancellationToken cancellationToken = new CancellationToken())
		{
			var issueTypesUri = jiraClient.JiraRestUri.AppendSegments("issuetype");
			jiraClient.Behaviour.MakeCurrent();
			var response = await issueTypesUri.GetAsAsync<HttpResponse<IList<IssueType>, Error>>(cancellationToken).ConfigureAwait(false);
			jiraClient.HandleErrors(response);
			return response.Response;
		}

		/// <summary>
		///     Create an issue
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="issue">the issue to create</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Issue</returns>
		public static async Task<Issue> CreateAsync(this IIssueDomain jiraClient, Issue issue, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issue == null)
			{
				throw new ArgumentNullException(nameof(issue));
			}
			Log.Debug().WriteLine("Creating issue {0}", issue);
			jiraClient.Behaviour.MakeCurrent();
			var issueUri = jiraClient.JiraRestUri.AppendSegments("issue");
			var response = await issueUri.PostAsync<HttpResponse<Issue, Error>>(issue, cancellationToken).ConfigureAwait(false);
			jiraClient.HandleErrors(response);
			return response.Response;
		}

		/// <summary>
		///     Delete an issue
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="issueKey">the key of the issue to delete</param>
		/// <param name="deleteSubtasks">true or false (default) indicating that any subtasks should also be deleted.
		/// If the issue has no subtasks this parameter is ignored. If the issue has subtasks and this parameter is missing or false, then the issue will not be deleted and an error will be returned</param>
		/// <param name="cancellationToken">CancellationToken</param>
		public static async Task DeleteAsync(this IIssueDomain jiraClient, string issueKey, bool deleteSubtasks = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			jiraClient.Behaviour.MakeCurrent();
			var issueUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey);
			if (deleteSubtasks)
			{
				issueUri = issueUri.ExtendQuery("deleteSubtasks", true);
			}
			var response = await issueUri.DeleteAsync<HttpResponse>(cancellationToken).ConfigureAwait(false);
			if (response.StatusCode != HttpStatusCode.NoContent)
			{
				var message = response.StatusCode.ToString();
				Log.Warn().WriteLine("Http status code: {0}. Response from server: {1}", response.StatusCode, message);
				throw new Exception($"Status: {response.StatusCode} Message: {message}");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="issueKey">Key for the issue to assign</param>
		/// <param name="user">User to assign to, use User.Nobody to remove the assignee or User.Default to automaticly assign</param>
		/// <param name="cancellationToken"></param>
		public static async Task AssignAsync(this IIssueDomain jiraClient, string issueKey, User user, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (user == null)
			{
				user = User.Nobody;
			}

			jiraClient.Behaviour.MakeCurrent();
			var issueUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "assignee");
			var response = await issueUri.PutAsync<HttpResponse>(user, cancellationToken).ConfigureAwait(false);
			if (response.StatusCode != HttpStatusCode.NoContent)
			{
				var message = response.StatusCode.ToString();
				Log.Warn().WriteLine("Http status code: {0}. Response from server: {1}", response.StatusCode, message);
				throw new Exception($"Status: {response.StatusCode} Message: {message}");
			}
		}
	}
}