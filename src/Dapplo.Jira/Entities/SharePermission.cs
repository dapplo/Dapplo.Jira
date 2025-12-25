// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Share Permission information
/// </summary>
public class SharePermission
{
    /// <summary>
    ///     Id for the share permission
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    ///     Project for the permissions
    /// </summary>
    [JsonPropertyName("project")]
    public ProjectDigest Project { get; set; }

    /// <summary>
    ///     Share type can be project
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }
}