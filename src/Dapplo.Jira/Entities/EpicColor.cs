// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Epic color information
/// </summary>
[JsonObject]
public class EpicColor
{
    /// <summary>
    ///     Key for the color
    /// </summary>
    [JsonProperty("key")]
    public string Key { get; set; }
}