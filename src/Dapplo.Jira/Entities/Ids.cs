// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     To get the worklog information, a list of Ids needs to be supplied, this is the container for it
///     See <a href="https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issue-worklogs/#api-rest-api-3-worklog-list-post">Worklog list</a>
/// </summary>
public class IdContainer
{
    /// <summary>
    ///     The list of IDs which a service eeds
    /// </summary>
    [JsonPropertyName("ids")]
    public IEnumerable<long> Ids { get; set; }
}