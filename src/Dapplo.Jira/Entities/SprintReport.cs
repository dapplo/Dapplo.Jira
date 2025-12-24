// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Grasshopper Sprint Report
/// </summary>
public class SprintReport
{
    /// <summary>
    ///     The Sprint Report contents
    /// </summary>
    [JsonPropertyName("contents")]
    public SprintReportContents Contents { get; set; }

    /// <summary>
    ///     Sprint information
    /// </summary>
    [JsonPropertyName("sprint")]
    public SprintInReport Sprint { get; set; }

    /// <summary>
    ///     Indicates if the sprint supports pages
    /// </summary>
    [JsonPropertyName("supportsPages")]
    public bool SupportsPages { get; set; }
}