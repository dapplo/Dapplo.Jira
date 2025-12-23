// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Search result information, see <a href="https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issue-search/#api-rest-api-3-search-jql-post">here</a>
/// </summary>
[JsonObject]
public class SearchIssuesResult<TIssue, TSearch> : PageableResult, IEnumerable<TIssue> where TIssue : IssueBase
{
    /// <summary>
    /// The original search value, used to continue searches
    /// </summary>
    [JsonIgnore]
    public TSearch SearchParameter { get; set; }

    /// <summary>
    ///     Expand values
    /// </summary>
    [JsonProperty("expand")]
    public string Expand { get; set; }

    /// <summary>
    ///     List of issues
    /// </summary>
    [JsonProperty("issues")]
    public IList<TIssue> Issues { get; set; }

    /// <summary>
    ///     Contains a dictionary with the the display name of each field.
    /// </summary>
    [JsonProperty("names")]
    public IDictionary<string, string> FieldDisplayNames { get; set; }

    /// <summary>
    ///     Number of items in the result
    /// </summary>
    [JsonIgnore]
    public int Count => Issues?.Count ?? 0;

    /// <summary>
    /// Retrieve the next page, this is based upon the number of items that was returned
    /// </summary>
    [JsonIgnore]
    public Page NextPage => new Page
    {
        StartAt = StartAt + (Issues?.Count ?? 0),
        MaxResults = MaxResults
    };

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Generic IEnumerator implementation
    /// </summary>
    /// <returns>IEnumerator with TIssue</returns>
    public IEnumerator<TIssue> GetEnumerator()
    {
        return Issues.GetEnumerator();
    }
}