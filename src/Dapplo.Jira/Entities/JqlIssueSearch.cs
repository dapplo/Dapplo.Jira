// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Search request information, see <a href="https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issue-search/#api-rest-api-3-search-jql-post">here</a>
/// </summary>
public class JqlIssueSearch : Page
{
    /// <summary>
    ///     Expand values
    /// </summary>
    /// <value>
    ///     The expands.
    /// </value>
    [JsonPropertyName("expand")]
    public IEnumerable<string> Expand { get; set; } = JiraConfig.ExpandSearch;

    /// <summary>
    ///     Fields for this query
    /// </summary>
    [JsonPropertyName("fields")]
    public IEnumerable<string> Fields { get; set; } = JiraConfig.SearchFields;

    /// <summary>
    ///     The JQL for this search
    /// </summary>
    [JsonPropertyName("jql")]
    public string Jql { get; set; }

    /// <summary>
    ///     Does the query (JQL) need to be validated?
    /// </summary>
    [JsonPropertyName("validateQuery")]
    public bool ValidateQuery { get; set; } = true;
}