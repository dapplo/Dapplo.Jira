// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     StatusCategory information
/// </summary>
public class StatusCategory : BaseProperties<long>
{
    /// <summary>
    ///     Name of the color
    /// </summary>
    [JsonPropertyName("colorName")]
    public string ColorName { get; set; }

    /// <summary>
    ///     Key for the status category
    /// </summary>
    [JsonPropertyName("key")]
    public string Key { get; set; }

    /// <summary>
    ///     Name of the status category
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
}