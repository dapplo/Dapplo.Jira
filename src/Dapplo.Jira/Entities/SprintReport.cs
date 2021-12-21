// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Grasshopper Sprint Report
/// </summary>
[JsonObject]
public class SprintReport
{
    /// <summary>
    ///     The Sprint Report contents
    /// </summary>
    [JsonProperty("contents")]
    public SprintReportContents Contents { get; set; }

    /// <summary>
    ///     Sprint information
    /// </summary>
    [JsonProperty("sprint")]
    public SprintInReport Sprint { get; set; }

    /// <summary>
    ///     Indicates if the sprint supports pages
    /// </summary>
    [JsonProperty("supportsPages")]
    public bool SupportsPages { get; set; }
}