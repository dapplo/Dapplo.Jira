// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira;

/// <summary>
///     This holds all the issue related extensions methods
/// </summary>
public static class IssueDomainExtensions
{
#pragma warning disable IDE0090 // Use 'new(...)'
    private static readonly LogSource Log = new LogSource();
#pragma warning restore IDE0090 // Use 'new(...)'

    /// <summary>
    ///     Add comment to the specified issue
    ///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1139
    /// </summary>
    /// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
    /// <param name="issueKey">key for the issue</param>
    /// <param name="body">the body of the comment</param>
    /// <param name="visibility">Visibility with optional visibility group or role</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Comment</returns>
    public static async Task<Comment> AddCommentAsync(this IIssueDomain jiraClient, string issueKey, string body, Visibility visibility = null,
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
            Visibility = visibility
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
    /// <param name="fields">IEnumerable of string A list of fields to return for the issue. Use it to retrieve a subset of fields.</param>
    /// <param name="expand">IEnumerable of string Use expand to include additional information about the issues in the response.</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Issue</returns>
    public static async Task<TIssue> GetAsync<TIssue, TFields>(this IIssueDomain jiraClient, string issueKey, IEnumerable<string> fields = null,
        IEnumerable<string> expand = null, CancellationToken cancellationToken = default)
        where TIssue : IssueWithFields<TFields>
        where TFields : IssueFields
    {
        if (issueKey == null)
        {
            throw new ArgumentNullException(nameof(issueKey));
        }

        Log.Debug().WriteLine("Retrieving issue information for {0}", issueKey);
        var issueUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey);
        if (fields != null)
        {
            issueUri = issueUri.ExtendQuery("fields", string.Join(",", fields));
        }

        // Add the configurable expand values, if the value is not null or empty
        expand ??= JiraConfig.ExpandGetIssue;
        if (expand != null)
        {
            issueUri = issueUri.ExtendQuery("expand", string.Join(",", expand));
        }

        jiraClient.Behaviour.MakeCurrent();

        var response = await issueUri.GetAsAsync<HttpResponse<TIssue, Error>>(cancellationToken).ConfigureAwait(false);

        return response.HandleErrors().WithClient(jiraClient) as TIssue;
    }

    /// <summary>
    ///     Get issue information
    ///     See: https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issues/#api-rest-api-3-issue-issueidorkey-get
    /// </summary>
    /// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
    /// <param name="issueKey">the issue key</param>
    /// <param name="fields">IEnumerable of string A list of fields to return for the issue. Use it to retrieve a subset of fields.</param>
    /// <param name="expand">IEnumerable of string to specified which fields to expand</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Issue</returns>
    public static Task<Issue> GetAsync(this IIssueDomain jiraClient, string issueKey, IEnumerable<string> fields = null, IEnumerable<string> expand = null,
        CancellationToken cancellationToken = default)
    {
        return jiraClient.GetAsync<Issue, IssueFields>(issueKey, fields, expand, cancellationToken);
    }

    /// <summary>
    ///     Search for issues, with a JQL (e.g. from a filter)
    ///     See: https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issue-search/#api-rest-api-3-search-jql-post
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
        return jiraClient.SearchAsync(jql.ToString(), new Page
        {
            MaxResults = maxResults,
            StartAt = startAt
        }, fields, expand, cancellationToken);
    }

    /// <summary>
    ///     Search for issues, with a JQL (e.g. from a filter)
    ///     See: https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issue-search/#api-rest-api-3-search-jql-post
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
    ///     See: https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issue-search/#api-rest-api-3-search-jql-post
    /// </summary>
    /// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
    /// <param name="search">Search information, with Jira Query Language, like SQL, for the search</param>
    /// <param name="page">Page with paging information, overwriting the page info in the search.</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>SearchIssuesResult</returns>
    public static Task<SearchIssuesResult<Issue, JqlIssueSearch>> SearchAsync(this IIssueDomain jiraClient, JqlIssueSearch search, Page page = null,
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
    ///     See: https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issue-search/#api-rest-api-3-search-jql-post
    /// </summary>
    /// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
    /// <param name="search">The search arguments</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>SearchIssuesResult</returns>
    public static async Task<SearchIssuesResult<Issue, JqlIssueSearch>> SearchAsync(this IIssueDomain jiraClient, JqlIssueSearch search,
        CancellationToken cancellationToken = default)
    {
        if (search == null)
        {
            throw new ArgumentNullException(nameof(search));
        }

        Log.Debug().WriteLine("Searching via JQL: {0}", search.Jql);

        jiraClient.Behaviour.MakeCurrent();
        var searchUri = jiraClient.JiraRestUri.AppendSegments("search", "jql");

        var response = await searchUri
            .PostAsync<HttpResponse<SearchIssuesResult<Issue, JqlIssueSearch>, Error>>(search, cancellationToken)
            .ConfigureAwait(false);
        var result = response.HandleErrors();
        // Store the original search parameter
        result.SearchParameter = search;
        // Make sure all issues are associated with the used jira client
        foreach (var issue in result)
        {
            _ = issue.WithClient(jiraClient);
        }

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
    public static async Task<IList<IssueType>> GetIssueTypesAsync(this IIssueDomain jiraClient, CancellationToken cancellationToken = default)
    {
        var issueTypesUri = jiraClient.JiraRestUri.AppendSegments("issuetype");
        jiraClient.Behaviour.MakeCurrent();
        var response = await issueTypesUri.GetAsAsync<HttpResponse<IList<IssueType>, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///     Get a list of all possible issue link types
    /// </summary>
    /// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>List with IssueLinkType elements</returns>
    public static async Task<IList<IssueLinkType>> GetIssueLinkTypesAsync(this IIssueDomain jiraClient, CancellationToken cancellationToken = default)
    {
        var issueLinkTypesUri = jiraClient.JiraRestUri.AppendSegments("issueLinkType");
        jiraClient.Behaviour.MakeCurrent();
        var response = await issueLinkTypesUri.GetAsAsync<HttpResponse<IssueLinkTypes, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors().Values;
    }

    /// <summary>
    ///     Get possible transitions for the specified issue
    ///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1289
    /// </summary>
    /// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
    /// <param name="issueKey">the issue key</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>List of Transition</returns>
    public static async Task<IList<Transition>> GetTransitionsAsync(this IIssueDomain jiraClient, string issueKey, CancellationToken cancellationToken = default)
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
    ///     Change the state of the issue (make a transition)
    /// </summary>
    /// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
    /// <param name="issueKey">string with the key for the issue to update</param>
    /// <param name="transition">Transition</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public static Task TransitionAsync(this IIssueDomain jiraClient, string issueKey, Transition transition, CancellationToken cancellationToken = default)
    {
        var issueEdit = new IssueEdit
        {
            Transition = transition
        };
        return TransitionAsync(jiraClient, issueKey, issueEdit, cancellationToken);
    }

    /// <summary>
    ///     Change the state of the issue (make a transition)
    /// </summary>
    /// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
    /// <param name="issueKey">string with the key for the issue to update</param>
    /// <param name="issueEdit">IssueEdit which describes what to edit</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public static async Task TransitionAsync(this IIssueDomain jiraClient, string issueKey, IssueEdit issueEdit, CancellationToken cancellationToken = default)
    {
        if (issueEdit?.Transition == null)
        {
            throw new ArgumentNullException(nameof(issueEdit));
        }

        Log.Debug().WriteLine("Transitioning issue {0} to {1}", issueKey, issueEdit.Transition.Name);
        jiraClient.Behaviour.MakeCurrent();
        var issueUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "transitions");
        var response = await issueUri.PostAsync<HttpResponseWithError<Error>>(issueEdit, cancellationToken).ConfigureAwait(false);
        // Expect HttpStatusCode.NoContent throw error if not
        response.HandleStatusCode(HttpStatusCode.NoContent);
    }


    /// <summary>
    ///     Create an issue link
    /// </summary>
    /// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
    /// <param name="issueLink">IssueLink</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public static async Task CreateIssueLinkAsync(this IIssueDomain jiraClient, IssueLink issueLink, CancellationToken cancellationToken = default)
    {
        var issueLinkUri = jiraClient.JiraRestUri.AppendSegments("issueLink");
        jiraClient.Behaviour.MakeCurrent();
        var response = await issueLinkUri.PostAsync<HttpResponse>(issueLink, cancellationToken).ConfigureAwait(false);
        response.HandleStatusCode(HttpStatusCode.Created);
    }

    /// <summary>
    ///     Get an issue link
    /// </summary>
    /// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
    /// <param name="linkId">string</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Issue </returns>
    public static async Task<IssueLink> GetIssueLinkAsync(this IIssueDomain jiraClient, string linkId, CancellationToken cancellationToken = default)
    {
        var issueLinkUri = jiraClient.JiraRestUri.AppendSegments("issueLink", linkId);
        jiraClient.Behaviour.MakeCurrent();
        var response = await issueLinkUri.GetAsAsync<HttpResponse<IssueLink, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///     Delete an issue link
    /// </summary>
    /// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
    /// <param name="issueLinkId">string with the ID of the issue link</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public static async Task DeleteIssueLinkAsync(this IIssueDomain jiraClient, string issueLinkId, CancellationToken cancellationToken = default)
    {
        var issueLinkUri = jiraClient.JiraRestUri.AppendSegments("issueLink", issueLinkId);
        jiraClient.Behaviour.MakeCurrent();
        var response = await issueLinkUri.DeleteAsync<HttpResponse>(cancellationToken).ConfigureAwait(false);
        response.HandleStatusCode(HttpStatusCode.NoContent);
    }

    /// <summary>
    ///     Create an issue
    /// </summary>
    /// <typeparam name="TFields">The type of the issue fields</typeparam>
    /// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
    /// <param name="issue">the issue to create</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Issue</returns>
    public static async Task<IssueWithFields<TFields>> CreateAsync<TFields>(this IIssueDomain jiraClient, IssueWithFields<TFields> issue,
        CancellationToken cancellationToken = default)
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
    ///     Edit an issue
    /// </summary>
    /// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
    /// <param name="issueKey">string with the key for the issue to update</param>
    /// <param name="issueEdit">IssueEdit which describes what to edit</param>
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
    public static async Task EditAsync(this IIssueDomain jiraClient, string issueKey, IssueEdit issueEdit, bool notifyUsers = true, bool overrideScreenSecurity = false,
        bool overrideEditableFlag = false, CancellationToken cancellationToken = default)
    {
        if (issueEdit == null)
        {
            throw new ArgumentNullException(nameof(issueEdit));
        }

        Log.Debug().WriteLine("Editing issue {0}", issueKey);
        jiraClient.Behaviour.MakeCurrent();
        var issueUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey).ExtendQuery(new Dictionary<string, bool>
        {
            {
                "notifyUsers", notifyUsers
            },
            {
                "overrideScreenSecurity", overrideScreenSecurity
            },
            {
                "overrideEditableFlag", overrideEditableFlag
            }
        });
        var response = await issueUri.PutAsync<HttpResponseWithError<Error>>(issueEdit, cancellationToken).ConfigureAwait(false);
        // Expect HttpStatusCode.NoContent throw error if not
        response.HandleStatusCode(HttpStatusCode.NoContent);
    }

    /// <summary>
    ///     Delete an issue
    /// </summary>
    /// <param name="jiraClient">IIssueDomain to bind the extension method to</param>
    /// <param name="issueKey">the key of the issue to delete</param>
    /// <param name="deleteSubtasks">
    ///     true or false (default) indicating that any sub-tasks should also be deleted.
    ///     If the issue has no sub-tasks this parameter is ignored. If the issue has subtasks and this parameter is missing or
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
    /// <param name="userPattern">optional string with a pattern to match the user to</param>
    /// <param name="startAt">optional int with the start, used for paging</param>
    /// <param name="maxResults">optional int with the maximum number of results, default is 50</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>IEnumerable with User</returns>
    public static Task<IEnumerable<User>> GetAssignableUsersAsync(this IIssueDomain jiraClient, string issueKey, string userPattern = null, int? startAt = null,
        int? maxResults = null, CancellationToken cancellationToken = default)
    {
        return jiraClient.User.GetAssignableUsersAsync(issueKey: issueKey, username: userPattern, startAt: startAt, maxResults: maxResults,
            cancellationToken: cancellationToken);
    }
}
