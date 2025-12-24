// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Board configuration
/// </summary>
public class BoardConfiguration : Board
{
    /// <summary>
    ///     Filter for the Board
    /// </summary>
    [JsonPropertyName("filter")]
    public Filter Filter { get; set; }

    /// <summary>
    ///     Configuration for the columns of the Board
    /// </summary>
    [JsonPropertyName("columnConfig")]
    public ColumnConfig ColumnConfig { get; set; }

    /// <summary>
    ///     The custom field id for the ranking information
    /// </summary>
    [JsonPropertyName("ranking")]
    public RankingCustomFieldInfo Ranking { get; set; }

    /// <summary>
    ///     The custom field info for the estimation information
    /// </summary>
    [JsonPropertyName("estimation")]
    public EstimationCustomFieldInfo Estimation { get; set; }
}