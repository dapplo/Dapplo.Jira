// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Status information
/// </summary>
public class Status : BaseProperties<string>
{
    /// <summary>
    ///     Category for this status
    /// </summary>
    [JsonPropertyName("statusCategory")]
    public StatusCategory Category { get; set; }

    /// <summary>
    ///     Description for this status
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Url to the icon for this status
    /// </summary>
    [JsonPropertyName("iconUrl")]
    public Uri IconUri { get; set; }

    /// <summary>
    ///     Name of the status
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
}