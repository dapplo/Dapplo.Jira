// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Comment information
///     See
///     <a href="https://docs.atlassian.com/jira/REST/cloud/#api/2/configuration-getConfiguration">get configuration</a>
/// </summary>
public class Configuration
{
    /// <summary>
    ///     Is voting enabled?
    /// </summary>
    [JsonPropertyName("votingEnabled")]
    public bool IsVotingEnabled { get; set; }

    /// <summary>
    ///     Is watching enabled?
    /// </summary>
    [JsonPropertyName("watchingEnabled")]
    public bool IsWatchingEnabled { get; set; }

    /// <summary>
    ///     Are unassigned issues allowed?
    /// </summary>
    [JsonPropertyName("unassignedIssuesAllowed")]
    public bool AreUnassignedIssuesAllowed { get; set; }

    /// <summary>
    ///     Are sub-tasks enabled?
    /// </summary>
    [JsonPropertyName("subTasksEnabled")]
    public bool AreSubTasksEnabled { get; set; }

    /// <summary>
    ///     Is issue linking enabled?
    /// </summary>
    [JsonPropertyName("issueLinkingEnabled")]
    public bool IsIssueLinkingEnabled { get; set; }

    /// <summary>
    ///     Is time tracking enabled?
    /// </summary>
    [JsonPropertyName("timeTrackingEnabled")]
    public bool IsTimeTrackingEnabled { get; set; }

    /// <summary>
    ///     Are attachments enabled?
    /// </summary>
    [JsonPropertyName("attachmentsEnabled")]
    public bool AreAttachmentsEnabled { get; set; }

    /// <summary>
    ///     The configuration of the time tracking
    /// </summary>
    [JsonPropertyName("timeTrackingConfiguration")]
    public TimeTrackingConfiguration TimeTrackingConfiguration { get; set; }
}