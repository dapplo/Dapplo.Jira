// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Base id, used in pretty much every entity
/// </summary>
public class BaseId<TId>
{
    /// <summary>
    ///     Id of this entity
    /// </summary>
    [JsonPropertyName("id")]
    public TId Id { get; set; }
}