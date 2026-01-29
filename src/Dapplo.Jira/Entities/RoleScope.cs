// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Scope information for a project role
/// </summary>
[JsonObject]
public class RoleScope
{
    /// <summary>
    ///     Type of the scope (e.g., PROJECT)
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    ///     Project associated with this scope
    /// </summary>
    [JsonProperty("project")]
    public ProjectDigest Project { get; set; }
}
