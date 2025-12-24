// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Transitions information
/// </summary>
public class Transitions
{
    /// <summary>
    ///     The actual list of transitions
    /// </summary>
    [JsonPropertyName("transitions")]
    public IList<Transition> Items { get; set; }
}