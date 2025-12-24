// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Base fields, used in pretty much every entity
/// </summary>
public class BaseProperties<TId> : ReadOnlyBaseId<TId>
{
    /// <summary>
    ///     Link to itself
    /// </summary>
    [JsonPropertyName("self")]
    [ReadOnly(true)]
    public Uri Self { get; set; }
}