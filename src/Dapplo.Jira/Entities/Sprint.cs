// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using Newtonsoft.Json;
using Dapplo.Jira.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Sprint information
/// </summary>
[JsonObject]
public class Sprint : BaseProperties<long>
{
    /// <summary>
    ///     Name of the Sprint
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Goal of the Sprint
    /// </summary>
    [JsonProperty("goal")]
    public string Goal { get; set; }

    /// <summary>
    ///     From what board is this sprint?
    /// </summary>
    [JsonProperty("originBoardId")]
    public int? OriginBoardId { get; set; }

    /// <summary>
    ///     State of the Sprint
    /// </summary>
    [JsonProperty("state")]
    public string State { get; set; }

    /// <summary>
    ///     When was the sprint started
    /// </summary>
    [JsonProperty("startDate")]
    [ReadOnly(true)]
    [JsonConverter(typeof(CustomDateTimeOffsetConverter))]
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    ///     When was the sprint ended
    /// </summary>
    [JsonProperty("endDate")]
    [ReadOnly(true)]
    [JsonConverter(typeof(CustomDateTimeOffsetConverter))]
    public DateTimeOffset? EndDate { get; set; }

    /// <summary>
    ///     When was the sprint completed
    /// </summary>
    [JsonProperty("completeDate")]
    [ReadOnly(true)]
    [JsonConverter(typeof(CustomDateTimeOffsetConverter))]
    public DateTimeOffset? CompleteDate { get; set; }
}