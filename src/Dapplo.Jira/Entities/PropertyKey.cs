// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Represents a property key
/// </summary>
[JsonObject]
public class PropertyKey
{
    /// <summary>
    ///     The key of the property
    /// </summary>
    [JsonProperty("key")]
    public string Key { get; set; }

    /// <summary>
    ///     The self URL
    /// </summary>
    [JsonProperty("self")]
    public string Self { get; set; }
}
