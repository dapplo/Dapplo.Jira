// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Field information
/// </summary>
public class Field : BaseProperties<string>
{
    /// <summary>
    ///     Name of the field
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Is this field a custom field?
    /// </summary>
    [JsonPropertyName("custom")]
    public bool IsCustom { get; set; }

    /// <summary>
    ///     Is this field orderable?
    /// </summary>
    [JsonPropertyName("orderable")]
    public bool IsOrderable { get; set; }

    /// <summary>
    ///     Is this field navigable?
    /// </summary>
    [JsonPropertyName("navigable")]
    public bool IsNavigable { get; set; }

    /// <summary>
    ///     Is this field searchable?
    /// </summary>
    [JsonPropertyName("searchable")]
    public bool IsSearchable { get; set; }

    /// <summary>
    ///     Aliases in where clauses
    /// </summary>
    [JsonPropertyName("clauseNames")]
    public IList<string> ClauseNames { get; set; }

    /// <summary>
    ///     Schema for the field
    /// </summary>
    [JsonPropertyName("schema")]
    public Schema Schema { get; set; }
}