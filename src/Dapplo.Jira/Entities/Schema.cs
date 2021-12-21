// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Schema information of a field
/// </summary>
[JsonObject]
public class Schema
{
    /// <summary>
    ///     System for this schema
    /// </summary>
    [JsonProperty("system")]
    public string System { get; set; }

    /// <summary>
    ///     Type for this schema
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }
}