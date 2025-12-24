// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Represents a property of an entity
/// </summary>
public class EntityProperty
{
    /// <summary>
    ///     The property key
    /// </summary>
    [JsonPropertyName("key")]
    public string Key { get; set; }

    /// <summary>
    ///     The property value
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; }
}