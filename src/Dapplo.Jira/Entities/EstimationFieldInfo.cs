// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Information on the custom field id for the estimation information
/// </summary>
public class EstimationFieldInfo
{
    /// <summary>
    ///     Field name of the estimation custom field
    /// </summary>
    [JsonPropertyName("fieldId")]
    public string FieldId { get; set; }

    /// <summary>
    ///     Display name for the estimation
    /// </summary>
    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }
}