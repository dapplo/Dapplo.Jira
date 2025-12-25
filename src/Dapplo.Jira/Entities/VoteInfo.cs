// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Vote information
/// </summary>
public class VoteInfo
{
    /// <summary>
    ///     Does the issue have votes?
    /// </summary>
    [JsonPropertyName("hasVoted")]
    public bool HasVoted { get; set; }

    /// <summary>
    ///     Link to itself
    /// </summary>
    [JsonPropertyName("self")]
    public Uri Self { get; set; }

    /// <summary>
    ///     Who are the voters
    /// </summary>
    [JsonPropertyName("voters")]
    public IList<User> Voters { get; set; }

    /// <summary>
    ///     How many votes does it have
    /// </summary>
    [JsonPropertyName("votes")]
    public long? Votes { get; set; }
}