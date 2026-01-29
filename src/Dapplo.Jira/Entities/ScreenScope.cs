// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Screen scope information
/// </summary>
[JsonObject]
public class ScreenScope
{
    /// <summary>
    ///     Type of scope (PROJECT or TEMPLATE)
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    ///     Project associated with the screen
    /// </summary>
    [JsonProperty("project")]
    public ProjectDigest Project { get; set; }
}
