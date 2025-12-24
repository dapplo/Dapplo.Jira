// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Information on the custom field id for the estimation information
/// </summary>
public class EstimationCustomFieldInfo
{
    /// <summary>
    ///     EstimationFieldInfo
    /// </summary>
    [JsonPropertyName("field")]
    public EstimationFieldInfo Field { get; set; }

    /// <summary>
    ///     Type of the estimation custom field
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }
}