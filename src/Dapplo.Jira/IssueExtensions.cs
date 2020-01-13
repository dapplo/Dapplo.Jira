// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Domains;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Internal;
using Dapplo.Jira.Query;
using Dapplo.Log;

namespace Dapplo.Jira
{
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
		/// <returns>Comment</returns>
		public static async Task<Comment> AddCommentAsync(this IIssueDomain jiraClient, string issueKey, string body, string visibility = null,
			CancellationToken cancellationToken = default)
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			Log.Debug().WriteLine("Adding comment to {0}", issueKey);
			var comment = new Comment
			{
				Body = body,
				Visibility = visibility == null
					? null
					: new Visibility
					{
						Type = "role",
						Value = visibility
					}
			};
			jiraClient.Behaviour.MakeCurrent();
			var attachUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "comment");
			var response = await attachUri.PostAsync<HttpResponse<Comment, Error>>(comment, cancellationToken).ConfigureAwait(false);
			return response.HandleErrors(HttpStatusCode.Created);
		}

		/// <summary>
		///     Get issue information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e4539
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="issueKey">the issue key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Issue</returns>
		public static async Task<TIssue> GetAsync<TIssue, TFields>(this IIssueDomain jiraClient, string issueKey, CancellationToken cancellationToken = default)
			where TIssue : IssueWithFields<TFields>
			where TFields : IssueFields
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

			var response = await issueUri.GetAsAsync<HttpResponse<TIssue, Error>>(cancellationToken).ConfigureAwait(false);
			return response.HandleErrors();
		}

		/// <summary>
		///     Get issue information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e4539
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="issueKey">the issue key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Issue</returns>
		public static Task<Issue> GetAsync(this IIssueDomain jiraClient, string issueKey, CancellationToken cancellationToken = default)
		{
			return jiraClient.GetAsync<Issue, IssueFields>(issueKey, cancellationToken);
		}

		/// <summary>
		///     Get possible transitions for the specified issue
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1289
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="issueKey">the issue key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>List of Transition</returns>
		public static async Task<IList<Transition>> GetPossibleTransitionsAsync(this IIssueDomain jiraClient, string issueKey, CancellationToken cancellationToken = default)
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
			return response.HandleErrors().Items;
		}

		/// <summary>
		///     Search for issues, with a JQL (e.g. from a filter)
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2713
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="jql">Jira Query Language, like SQL, for the search. Use Where builder</param>
		/// <param name="startAt">Start of the results to return, used for paging. Default is 0</param>
		/// <param name="maxResults">Maximum number of results returned, default is 20</param>
		/// <param name="fields">Jira fields to include, if null the defaults from the JiraConfig.SearchFields are taken</param>
		/// <param name="expand">
		///     Additional fields to includes in the results, if null the defaults from the
		///     JiraConfig.ExpandSearch are taken
		/// </param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>SearchResults</returns>
		public static Task<SearchIssuesResult<Issue, JqlIssueSearch>> SearchAsync(this IIssueDomain jiraClient, IFinalClause jql, int maxResults = 20, int startAt = 0,
			IEnumerable<string> fields = null, IEnumerable<string> expand = null, CancellationToken cancellationToken = default)
		{
			return jiraClient.SearchAsync(jql.ToString(), new Page {MaxResults = maxResults, StartAt = startAt}, fields, expand, cancellationToken);
		}

		/// <summary>
		///     Search for issues, with a JQL (e.g. from a filter)
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2713
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="jql">Jira Query Language, like SQL, for the search</param>
		/// <param name="page">Page with paging information, default this starts at the beginning with a maxResults of 20</param>
		/// <param name="fields">Jira fields to include, if null the defaults from the JiraConfig.SearchFields are taken</param>
		/// <param name="expand">
		///     Additional fields to includes in the results, if null the defaults from the
		///     JiraConfig.ExpandSearch are taken
		/// </param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>
		///     SearchIssuesResult
		/// </returns>
		public static Task<SearchIssuesResult<Issue, JqlIssueSearch>> SearchAsync(this IIssueDomain jiraClient, string jql, Page page = null, IEnumerable<string> fields = null,
			IEnumerable<string> expand = null, CancellationToken cancellationToken = default)
		{
			if (jql == null)
			{
				throw new ArgumentNullException(nameof(jql));
			}

			var search = new JqlIssueSearch
			{
				Jql = jql,
				ValidateQuery = true,
				MaxResults = page?.MaxResults ?? 20,
				StartAt = page?.StartAt ?? 0,
				Fields = fields ?? new List<string>(JiraConfig.SearchFields),
				Expand = expand ?? (JiraConfig.ExpandSearch != null ? new List<string>(JiraConfig.ExpandSearch) : null)
			};
			return jiraClient.SearchAsync(search, cancellationToken);
		}

		/// <summary>
		///     Search for issues, with a JQL (e.g. from a filter)
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2713
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="search">Search information, with Jira Query Language, like SQL, for the search</param>
		/// <param name="page">Page with paging information, overwriting the page info in the search.</param>
		/// <param name="fields">Jira fields to include, if null the defaults from the JiraConfig.SearchFields are taken</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>SearchIssuesResult</returns>
		public static Task<SearchIssuesResult<Issue, JqlIssueSearch>> SearchAsync(this IIssueDomain jiraClient, JqlIssueSearch search, Page page = null, IEnumerable<string> fields = null,
			CancellationToken cancellationToken = default)
		{
			if (search == null)
			{
				throw new ArgumentNullException(nameof(search));
			}

			if (page != null)
			{
				search.MaxResults = page.MaxResults ?? 20;
				search.StartAt = page.StartAt ?? 0;
			}
			return jiraClient.SearchAsync(search, cancellationToken);
		}

		/// <summary>
		///     Search for issues, with a JQL (e.g. from a filter)
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2713
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="search">The search arguments</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>SearchIssuesResult</returns>
		public static async Task<SearchIssuesResult<Issue, JqlIssueSearch>> SearchAsync(this IIssueDomain jiraClient, JqlIssueSearch search, CancellationToken cancellationToken = default)
		{
			if (search == null)
			{
				throw new ArgumentNullException(nameof(search));
			}

			Log.Debug().WriteLine("Searching via JQL: {0}", search.Jql);

			jiraClient.Behaviour.MakeCurrent();
			var searchUri = jiraClient.JiraRestUri.AppendSegments("search");

			var response = await searchUri
				.PostAsync<HttpResponse<SearchIssuesResult<Issue, JqlIssueSearch>, Error>>(search, cancellationToken)
				.ConfigureAwait(false);
			var result = response.HandleErrors();
			// Store the original search parameter
			result.SearchParameter = search;
			return result;
		}

		/// <summary>
		///     Update comment
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1139
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="issueKey">jira key to which the comment belongs</param>
		/// <param name="comment">Comment to update</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Comment</returns>
		public static async Task<Comment> UpdateCommentAsync(this IIssueDomain jiraClient, string issueKey, Comment comment, CancellationToken cancellationToken = default)
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}

			Log.Debug().WriteLine("Updating comment {0} for issue {1}", comment.Id, issueKey);

			jiraClient.Behaviour.MakeCurrent();

			var attachUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "comment", comment.Id);
			var response = await attachUri.PutAsync<HttpResponse<Comment, Error>>(comment, cancellationToken).ConfigureAwait(false);
			return response.HandleErrors();
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
			return response.HandleErrors();
		}

		/// <summary>
		///     Create an issue
		/// </summary>
		/// <typeparam name="TFields">The type of the issue fields</typeparam>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="issue">the issue to create</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Issue</returns>
		public static async Task<IssueWithFields<TFields>> CreateAsync<TFields>(this IIssueDomain jiraClient, IssueWithFields<TFields> issue, CancellationToken cancellationToken = default)
			where TFields : IssueFields
		{
			if (issue == null)
			{
				throw new ArgumentNullException(nameof(issue));
			}
			Log.Debug().WriteLine("Creating issue {0}", issue);
			jiraClient.Behaviour.MakeCurrent();
			var issueUri = jiraClient.JiraRestUri.AppendSegments("issue");
			var response = await issueUri.PostAsync<HttpResponse<IssueWithFields<TFields>, Error>>(issue, cancellationToken).ConfigureAwait(false);
			return response.HandleErrors(HttpStatusCode.Created);
		}

		/// <summary>
		///     Update an issue
		/// </summary>
		/// <typeparam name="TFields">The type of the issue</typeparam>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="issue">the issue to update</param>
		/// <param name="notifyUsers">
		///     send the email with notification that the issue was updated to users that watch it. Admin or
		///     project admin permissions are required to disable the notification. default = true
		/// </param>
		/// <param name="overrideScreenSecurity">
		///     allows to update fields that are not on the screen. Only connect add-on users with
		///     admin scope permission are allowed to use this flag. default = false
		/// </param>
		/// <param name="overrideEditableFlag">
		///     Updates the issue even if the issue is not editable due to being in a status with
		///     jira.issue.editable set to false or missing. Only connect add-on users with admin scope permission are allowed to
		///     use this flag. default = false
		/// </param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>TIssue</returns>
		public static async Task UpdateAsync<TFields>(this IIssueDomain jiraClient, IssueWithFields<TFields> issue, bool notifyUsers = true, bool overrideScreenSecurity = false, bool overrideEditableFlag = false, CancellationToken cancellationToken = default)
			where TFields : IssueFields
		{
			if (issue == null)
			{
				throw new ArgumentNullException(nameof(issue));
			}
			Log.Debug().WriteLine("Updating issue {0}", issue.Key);
			jiraClient.Behaviour.MakeCurrent();
			var issueToUpdate = new IssueWithFields<TFields>
			{
				Fields = issue.Fields
			};
			var issueUri = jiraClient.JiraRestUri.AppendSegments("issue", issue.Key).ExtendQuery(new Dictionary<string, bool>
			{
				{"notifyUsers", notifyUsers},
				{"overrideScreenSecurity", overrideScreenSecurity},
				{"overrideEditableFlag", overrideEditableFlag}
			});
			var response = await issueUri.PutAsync<HttpResponseWithError<Error>>(issueToUpdate, cancellationToken).ConfigureAwait(false);
			// Expect HttpStatusCode.NoContent throw error if not
			response.HandleStatusCode(HttpStatusCode.NoContent);
		}

		/// <summary>
		///     Delete an issue
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="issueKey">the key of the issue to delete</param>
		/// <param name="deleteSubtasks">
		///     true or false (default) indicating that any subtasks should also be deleted.
		///     If the issue has no subtasks this parameter is ignored. If the issue has subtasks and this parameter is missing or
		///     false, then the issue will not be deleted and an error will be returned
		/// </param>
		/// <param name="cancellationToken">CancellationToken</param>
		public static async Task DeleteAsync(this IIssueDomain jiraClient, string issueKey, bool deleteSubtasks = false,
			CancellationToken cancellationToken = default)
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
			response.HandleStatusCode(HttpStatusCode.NoContent);
		}

		/// <summary>
		///     Assign an issue to a user
		/// </summary>
		/// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
		/// <param name="issueKey">Key for the issue to assign</param>
		/// <param name="user">User to assign to, use User.Nobody to remove the assignee or User.Default to automaticly assign</param>
		/// <param name="cancellationToken">CancellationToken</param>
		public static async Task AssignAsync(this IIssueDomain jiraClient, string issueKey, User user, CancellationToken cancellationToken = default)
		{
			if (user == null)
			{
				user = User.Nobody;
			}

			jiraClient.Behaviour.MakeCurrent();
			var issueUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "assignee");
			var response = await issueUri.PutAsync<HttpResponse>(user, cancellationToken).ConfigureAwait(false);
			response.HandleStatusCode(HttpStatusCode.NoContent, HttpStatusCode.OK);
		}

		/// <summary>
		///     Retrieve the users who this issue can be assigned to
		/// </summary>
		/// <param name="jiraClient">IProjectDomain</param>
		/// <param name="issueKey">string with the key of the issue</param>
		/// <param name="userpattern">optional string with a pattern to match the user to</param>
		/// <param name="startAt">optional int with the start, used for paging</param>
		/// <param name="maxResults">optional int with the maximum number of results, default is 50</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>IEnumerable with User</returns>
		public static Task<IEnumerable<User>> GetAssignableUsersAsync(this IIssueDomain jiraClient, string issueKey, string userpattern = null, int? startAt = null, int? maxResults = null, CancellationToken cancellationToken = default)
		{
			return jiraClient.User.GetAssignableUsersAsync(issueKey: issueKey, username: userpattern, startAt: startAt, maxResults: maxResults, cancellationToken: cancellationToken);
		}
	}
}