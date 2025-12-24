// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Jira login info
/// </summary>
public class LoginInfo
{
    /// <summary>
    ///     Failed login count
    /// </summary>
    [JsonPropertyName("failedLoginCount")]
    public int? FailedLoginCount { get; set; }

    /// <summary>
    ///     Last failed login time
    /// </summary>
    [JsonPropertyName("lastFailedLoginTime")]
    public string LastFailedLoginTime { get; set; }

    /// <summary>
    ///     Login count
    /// </summary>
    [JsonPropertyName("loginCount")]
    public int? LoginCount { get; set; }

    /// <summary>
    ///     Previous login time
    /// </summary>
    [JsonPropertyName("previousLoginTime")]
    public string PreviousLoginTime { get; set; }
}