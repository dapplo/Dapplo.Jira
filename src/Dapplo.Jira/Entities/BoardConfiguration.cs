// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Board configuration
/// </summary>
[JsonObject]
public class BoardConfiguration : Board
{
    /// <summary>
    ///     Filter for the Board
    /// </summary>
    [JsonProperty("filter")]
    public Filter Filter { get; set; }

    /// <summary>
    ///     Configuration for the columns of the Board
    /// </summary>
    [JsonProperty("columnConfig")]
    public ColumnConfig ColumnConfig { get; set; }

    /// <summary>
    ///     The custom field id for the ranking information
    /// </summary>
    [JsonProperty("ranking")]
    public RankingCustomFieldInfo Ranking { get; set; }

    /// <summary>
    ///     The custom field info for the estimation information
    /// </summary>
    [JsonProperty("estimation")]
    public EstimationCustomFieldInfo Estimation { get; set; }
}