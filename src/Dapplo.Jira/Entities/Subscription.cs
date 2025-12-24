// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Subscription information
/// </summary>
public class Subscription : ReadOnlyBaseId<long>
{
    /// <summary>
    ///     The user which subscribed
    /// </summary>
    [JsonPropertyName("user")]
    public User Subscriber { get; set; }
}