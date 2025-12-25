// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Security level information
/// </summary>
public class SecurityLevel : BaseProperties<long>
{
    /// <summary>
    ///     Description for the security level
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Name of the security level
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
}