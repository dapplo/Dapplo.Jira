// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Time tracking configuration
/// </summary>
public class TimeTrackingConfiguration
{
    /// <summary>
    ///     The number of working hours per day
    /// </summary>
    [JsonPropertyName("workingHoursPerDay")]
    public float WorkingHoursPerDay { get; set; } = 8;

    /// <summary>
    ///     The number of working days per week
    /// </summary>
    [JsonPropertyName("workingDaysPerWeek")]
    public float WorkingDaysPerWeek { get; set; } = 5;

    /// <summary>
    ///     The time format used
    /// </summary>
    [JsonPropertyName("timeFormat")]
    public string TimeFormat { get; set; } = "pretty";

    /// <summary>
    ///     The default unit
    /// </summary>
    [JsonPropertyName("defaultUnit")]
    public string DefaultUnit { get; set; } = "hour";
}