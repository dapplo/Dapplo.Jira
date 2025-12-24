// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Sprint information within a Sprint Report
/// </summary>
public class SprintInReport : Sprint
{
    /// <summary>
    ///     Sequence of this sprint
    /// </summary>
    [JsonPropertyName("sequence")]
    public long Sequence { get; set; }

    /// <summary>
    ///     Indicates the amount of pages attached to the sprint
    /// </summary>
    [JsonPropertyName("linkedPagesCount")]
    public int LinkedPagesCount { get; set; }

    /// <summary>
    ///     Indicated if the Sprint can be updated
    /// </summary>
    [JsonPropertyName("canUpdateSprint")]
    public bool CanUpdateSprint { get; set; }

    /// <summary>
    ///     Links to pages attached to the sprint
    /// </summary>
    [JsonPropertyName("remoteLinks")]
    public IEnumerable<RemoteLinks> RemoteLinks { get; set; }

    /// <summary>
    ///     Days remaining before the end of the sprint
    /// </summary>
    [JsonPropertyName("daysRemaining")]
    public int DaysRemaining { get; set; }
}