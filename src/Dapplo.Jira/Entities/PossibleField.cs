// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Possible field information
/// </summary>
public class PossibleField
{
    /// <summary>
    ///     Allowed values
    /// </summary>
    [JsonPropertyName("allowedValues")]
    public IList<AllowedValue> AllowedValues { get; set; }

    /// <summary>
    ///     TODO: Describe
    /// </summary>
    [JsonPropertyName("autoCompleteUrl")]
    public Uri AutoCompleteUrl { get; set; }

    /// <summary>
    ///     Name of the field
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Possible operations
    /// </summary>
    [JsonPropertyName("operations")]
    public IList<string> Operations { get; set; }

    /// <summary>
    ///     The summary of the time spend on this issue
    /// </summary>
    [JsonPropertyName("required")]
    public bool Required { get; set; }
}