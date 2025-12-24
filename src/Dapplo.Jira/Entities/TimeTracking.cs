// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Timetracking information
/// </summary>
public class TimeTracking
{
    /// <summary>
    ///     The originaly estimated time for this issue
    /// </summary>
    [JsonPropertyName("originalEstimate")]
    public string OriginalEstimate { get; set; }

    /// <summary>
    ///     The originaly estimated time for this issue
    /// </summary>
    [JsonPropertyName("originalEstimateSeconds")]
    public long? OriginalEstimateSeconds { get; set; }

    /// <summary>
    ///     The remaining estimated time for this issue
    /// </summary>
    [JsonPropertyName("remainingEstimate")]
    public string RemainingEstimate { get; set; }


    /// <summary>
    ///     The remaining estimated time, in seconds, for this issue
    /// </summary>
    [JsonPropertyName("remainingEstimateSeconds")]
    public long? RemainingEstimateSeconds { get; set; }

    /// <summary>
    ///     Time spent in form of "4w 4d 2h"
    /// </summary>
    [JsonPropertyName("timeSpent")]
    public string TimeSpent { get; set; }

    /// <summary>
    ///     Time spent in seconds
    /// </summary>
    [JsonPropertyName("timeSpentSeconds")]
    public long? TimeSpentSeconds { get; set; }
}