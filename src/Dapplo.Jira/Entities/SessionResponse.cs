// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Response to the session login
/// </summary>
internal class SessionResponse
{
    [JsonPropertyName("loginInfo")] public LoginInfo LoginInfo { get; set; }

    [JsonPropertyName("session")] public JiraSession Session { get; set; }
}