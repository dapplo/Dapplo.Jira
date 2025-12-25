// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Priority information
/// </summary>
public class Priority : BaseProperties<int>
{
    /// <summary>
    ///     Description of the priority
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Url to the icon for this priority
    /// </summary>
    [JsonPropertyName("iconUrl")]
    public Uri IconUrl { get; set; }

    /// <summary>
    ///     Name of the priority
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Status color
    /// </summary>
    [JsonPropertyName("statusColor")]
    public string StatusColor { get; set; }
}