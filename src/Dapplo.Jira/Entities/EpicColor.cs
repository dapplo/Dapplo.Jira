// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Epic color information
/// </summary>
public class EpicColor
{
    /// <summary>
    ///     Key for the color
    /// </summary>
    [JsonPropertyName("key")]
    public string Key { get; set; }
}