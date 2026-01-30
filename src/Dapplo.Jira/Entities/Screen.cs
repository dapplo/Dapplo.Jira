// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Screen information
/// </summary>
[JsonObject]
public class Screen : BaseProperties<long>
{
    /// <summary>
    ///     Name of the screen
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Description of the screen
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Scope of the screen
    /// </summary>
    [JsonProperty("scope")]
    public ScreenScope Scope { get; set; }
}
