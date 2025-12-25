// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Base id, used in pretty much every entity
///     This is used where the ID is only de-serialized, and not sent when serializing
/// </summary>
public class ReadOnlyBaseId<TId>
{
    /// <summary>
    ///     Id of this entity
    /// </summary>
    [JsonPropertyName("id")]
    [ReadOnly(true)]
    public TId Id { get; set; }
}