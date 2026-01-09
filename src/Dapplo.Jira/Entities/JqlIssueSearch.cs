// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Search request information, see <a href="https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issue-search/#api-rest-api-3-search-jql-post">here</a>
/// </summary>
[JsonObject]
public class JqlIssueSearch : Page
{
    /// <summary>
    ///     Expand values
    /// </summary>
    /// <value>
    ///     The expands.
    /// </value>
    [JsonProperty("expand")]
    public string Expand { get; set; } = JiraConfig.ExpandSearch != null ? string.Join(",", JiraConfig.ExpandSearch):null;

    /// <summary>
    /// A list of up to 5 issue properties to include in the results. This parameter accepts a comma-separated list.
    /// </summary>
    [JsonProperty("properties")]
    public IEnumerable<string> Properties { get; set; }

    /// <summary>
    ///     Fields for this query
    /// </summary>
    [JsonProperty("fields")]
    public IEnumerable<string> Fields { get; set; } = JiraConfig.SearchFields;

    /// <summary>
    ///     The JQL for this search
    /// </summary>
    [JsonProperty("jql")]
    public string Jql { get; set; }

    /// <summary>
    ///     Reference fields by their key (rather than ID). The default is false.
    /// </summary>
    [JsonProperty("fieldsByKeys", NullValueHandling = NullValueHandling.Include)]
    public bool FieldsByKeys { get; set; } = false;
    
}
