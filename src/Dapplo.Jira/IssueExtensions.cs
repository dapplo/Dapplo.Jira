// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;
using Newtonsoft.Json;

namespace Dapplo.Jira
{
    /// <summary>
    ///     This holds all the issue related extensions methods
    /// </summary>
    public static class IssueExtensions
    {
        /// <summary>
        /// Transition the issue
        /// </summary>
        /// <param name="issue">Issue</param>
        /// <param name="transition">string with the transition to use</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Task</returns>
        public static async Task TransitionAsync(this IssueBase issue, string transition, CancellationToken cancellationToken = default)
        {
            var jiraClient = issue.AssociatedJiraClient;
            var transitions = await jiraClient.Issue.GetTransitionsAsync(issue.Key, cancellationToken);
            var newTransition =
                transitions.First(t => string.Equals(t.Name, transition, StringComparison.OrdinalIgnoreCase));
            await issue.AssociatedJiraClient.Issue.TransitionAsync(issue.Key, newTransition, cancellationToken);
        }

        /// <summary>
        /// Transition the issue
        /// </summary>
        /// <param name="issue">Issue</param>
        /// <param name="transition">string with the transition to use</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Task</returns>
        public static Task TransitionAsync(this IssueBase issue, Transition transition, CancellationToken cancellationToken = default)
        {
            return issue.AssociatedJiraClient.Issue.TransitionAsync(issue.Key, transition, cancellationToken);
        }

        /// <summary>
        /// Add a comment to the issue
        /// </summary>
        /// <param name="issue">Issue to comment</param>
        /// <param name="comment">string with the comment to add</param>
        /// <param name="visibility">Visibility optional</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Task</returns>
        public static Task AddCommentAsync(this IssueBase issue, string comment, Visibility visibility = null, CancellationToken cancellationToken = default)
        {
            return issue.AssociatedJiraClient.Issue.AddCommentAsync(issue.Key, comment, visibility, cancellationToken);
        }

        /// <summary>
        /// Assign the issue to someone
        /// </summary>
        /// <param name="issue">Issue to comment</param>
        /// <param name="newUser">User to assign to</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Task</returns>
        public static Task AssignAsync(this IssueBase issue, User newUser, CancellationToken cancellationToken = default)
        {
            return issue.AssociatedJiraClient.Issue.AssignAsync(issue.Key, newUser, cancellationToken);
        }

        /// <summary>
        /// Retrieve a custom field from an issue
        /// </summary>
        /// <typeparam name="TCustomField">type for the custom field</typeparam>
        /// <param name="issue">Issue  to retrieve custom field value from</param>
        /// <param name="customFieldName">string with the name of the custom field</param>
        /// <returns>TCustomField</returns>
        public static TCustomField GetCustomField<TCustomField>(this Issue issue, string customFieldName)
        {
            var customFields = issue.Fields.CustomFields;
            if (customFields == null || !customFields.ContainsKey(customFieldName))
            {
                return default;
            }
            return JsonConvert.DeserializeObject<TCustomField>(issue.Fields.CustomFields[customFieldName].ToString());
        }

        /// <summary>
        /// Retrieve a custom field from an issue
        /// </summary>
        /// <param name="issue">Issue  to retrieve custom field value from</param>
        /// <param name="customFieldName">string with the name of the custom field</param>
        /// <returns>string</returns>
        public static string GetCustomField(this Issue issue, string customFieldName)
        {
            var customFields = issue.Fields.CustomFields;
            if (customFields == null || !customFields.ContainsKey(customFieldName))
            {
                return default;
            }
            return issue.Fields.CustomFields[customFieldName].ToString();
        }

        /// <summary>
        /// Retrieve a custom field from an agile issue
        /// </summary>
        /// <typeparam name="TCustomField">type for the custom field</typeparam>
        /// <param name="issue">AgileIssue to retrieve custom field value from</param>
        /// <param name="customFieldName">string with the name of the custom field</param>
        /// <returns>TCustomField</returns>
        public static TCustomField GetCustomField<TCustomField>(this AgileIssue issue, string customFieldName)
        {
            var customFields = issue.Fields.CustomFields;
            if (customFields == null || !customFields.ContainsKey(customFieldName))
            {
                return default;
            }
            return JsonConvert.DeserializeObject<TCustomField>(issue.Fields.CustomFields[customFieldName].ToString());
        }

        /// <summary>
        /// Retrieve a custom field from an agile issue
        /// </summary>
        /// <param name="issue">AgileIssue to retrieve custom field value from</param>
        /// <param name="customFieldName">string with the name of the custom field</param>
        /// <returns>string</returns>
        public static string GetCustomField(this AgileIssue issue, string customFieldName)
        {
            var customFields = issue.Fields.CustomFields;
            if (customFields == null || !customFields.ContainsKey(customFieldName))
            {
                return default;
            }
            return issue.Fields.CustomFields[customFieldName].ToString();
        }

        /// <summary>
        /// Add a custom field to an IssueEdit
        /// </summary>
        /// <typeparam name="TCustomField">type for the custom field</typeparam>
        /// <param name="issueToEdit">IssueEdit to add a custom field with value to</param>
        /// <param name="customFieldName">string with the name of the custom field</param>
        /// <param name="customFieldValue">TCustomField with the value</param>
        /// <returns>IssueEdit for a fluent usage</returns>
        public static IssueEdit AddCustomField<TCustomField>(this IssueEdit issueToEdit, string customFieldName, TCustomField customFieldValue)
        {
            // Make sure that IssueFields is available
            issueToEdit.Fields ??= new IssueFields();
            issueToEdit.Fields.CustomFields.Add(customFieldName, customFieldValue);
            return issueToEdit;
        }

        /// <summary>
        /// Add a custom field to an Issue
        /// </summary>
        /// <typeparam name="TCustomField">type for the custom field</typeparam>
        /// <param name="issueToEdit">IssueEdit to add a custom field with value to</param>
        /// <param name="customFieldName">string with the name of the custom field</param>
        /// <param name="customFieldValue">TCustomField with the value</param>
        /// <returns>Issue for a fluent usage</returns>
        public static Issue AddCustomField<TCustomField>(this Issue issueToEdit, string customFieldName, TCustomField customFieldValue)
        {
            // Make sure that IssueFields is available
            issueToEdit.Fields ??= new IssueFields();
            issueToEdit.Fields.CustomFields.Add(customFieldName, customFieldValue);
            return issueToEdit;
        }
    }
}
