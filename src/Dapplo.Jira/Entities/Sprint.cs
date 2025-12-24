// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Text.Json.Serialization;
using Dapplo.Jira.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Sprint information
/// </summary>
public class Sprint : BaseProperties<long>
{
    /// <summary>
    ///     Name of the Sprint
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Goal of the Sprint
    /// </summary>
    [JsonPropertyName("goal")]
    public string Goal { get; set; }

    /// <summary>
    ///     From what board is this sprint?
    /// </summary>
    [JsonPropertyName("originBoardId")]
    public int? OriginBoardId { get; set; }

    /// <summary>
    ///     State of the Sprint
    /// </summary>
    [JsonPropertyName("state")]
    public string State { get; set; }

    /// <summary>
    ///     When was the sprint started
    /// </summary>
    [JsonPropertyName("startDate")]
    [ReadOnly(true)]
    [JsonConverter(typeof(JiraDateTimeOffsetConverter))]
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    ///     When was the sprint ended
    /// </summary>
    [JsonPropertyName("endDate")]
    [ReadOnly(true)]
    [JsonConverter(typeof(JiraDateTimeOffsetConverter))]
    public DateTimeOffset? EndDate { get; set; }

    /// <summary>
    ///     When was the sprint completed
    /// </summary>
    [JsonPropertyName("completeDate")]
    [ReadOnly(true)]
    [JsonConverter(typeof(JiraDateTimeOffsetConverter))]
    public DateTimeOffset? CompleteDate { get; set; }
}