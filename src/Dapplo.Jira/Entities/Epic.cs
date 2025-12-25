// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Epic information
/// </summary>
public class Epic : BaseProperties<long>
{
    /// <summary>
    ///     Name of the Epic
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Epic summary
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    /// <summary>
    ///     Is the epic done?
    /// </summary>
    [JsonPropertyName("done")]
    public bool IsDone { get; set; }

    /// <summary>
    ///     Color of the Epic
    /// </summary>
    [JsonPropertyName("color")]
    public EpicColor Color { get; set; }
}