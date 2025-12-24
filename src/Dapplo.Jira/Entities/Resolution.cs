// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Resolution information
/// </summary>
public class Resolution : BaseProperties<string>
{
    /// <summary>
    ///     Description of the resolution
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Name of the resolution
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
}