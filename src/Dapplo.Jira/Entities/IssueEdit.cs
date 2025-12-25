// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Container for certain update fields
/// </summary>
public class IssueEdit
{
    /// <summary>
    /// The new transition
    /// </summary>
    [JsonPropertyName("transition")]
    public Transition Transition { get; set; }

    /// <summary>
    ///     Container for issue updates
    /// </summary>
    [JsonPropertyName("update")]
    public Dictionary<string, IIssueUpdateOperation> Update { get; } =
        new Dictionary<string, IIssueUpdateOperation>();

    /// <summary>
    /// The fields to edit
    /// </summary>
    [JsonPropertyName("fields")]
    public IssueFields Fields { get; set; }
}