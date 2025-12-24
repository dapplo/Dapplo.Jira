// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     UpdatedWorklog information
/// </summary>
public class UpdatedWorklog
{
    /// <summary>
    ///     The ID of the updated worklog.
    /// </summary>
    [JsonPropertyName("worklogId")]
    public long Id { get; set; }

    /// <summary>
    ///     The datetime of the change, as a UNIX timestamp in milliseconds.
    /// </summary>
    [JsonPropertyName("updatedTime")]
    public long UpdatedTime { get; set; }

    /// <summary>
    ///     The updated worklog properties
    /// </summary>
    [JsonPropertyName("properties")]
    public IList<EntityProperty> Properties { get; set; }
}