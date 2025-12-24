// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;


namespace Dapplo.Jira.Entities;

/// <summary>
///     Board information
/// </summary>
public class Board : BaseProperties<long>
{
    /// <summary>
    ///     Name of the Board
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Board type
    /// </summary>
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BoardTypes Type { get; set; }

    /// <summary>
    ///     Filter for the board, used when creating
    /// </summary>
    [JsonPropertyName("filterId")]
    public long FilterId { get; set; }
}