// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
/// Part of the changelog
/// </summary>
public class History : BaseProperties<long>
{
    /// <summary>
    ///     Who created the comment
    /// </summary>
    [JsonPropertyName("author")]
    [ReadOnly(true)]
    public User Author { get; set; }

    /// <summary>
    ///     When was the comment created
    /// </summary>
    [JsonPropertyName("created")]
    [ReadOnly(true)]
    public DateTimeOffset? Created { get; set; }

    /// <summary>
    ///     list of fields that have been changed during this operation
    /// </summary>
    /// <value>
    ///     The items.
    /// </value>
    [JsonPropertyName("items")]
    [ReadOnly(true)]
    public IList<HistoryItem> Items { get; set; }
}