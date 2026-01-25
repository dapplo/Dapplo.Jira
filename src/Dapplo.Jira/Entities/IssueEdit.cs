// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Container for certain update fields
/// </summary>
[JsonObject]
public class IssueEdit
{
    /// <summary>
    /// The new transition
    /// </summary>
    [JsonProperty("transition")]
    public Transition Transition { get; set; }

    /// <summary>
    ///     Container for issue updates
    /// </summary>
    [JsonProperty("update")]
    public Dictionary<string, IIssueUpdateOperation> Update { get; } =
        new Dictionary<string, IIssueUpdateOperation>();

    /// <summary>
    /// The fields to edit
    /// </summary>
    [JsonProperty("fields")]
    public BaseIssueFields Fields { get; set; }
}
