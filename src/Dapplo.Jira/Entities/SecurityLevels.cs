// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Security levels
/// </summary>
public class SecurityLevels
{
    /// <summary>
    ///     The actual list of security levels
    /// </summary>
    [JsonPropertyName("levels")]
    public IList<SecurityLevel> Levels { get; set; }
}