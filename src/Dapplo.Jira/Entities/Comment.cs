// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Comment information
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/attachment
/// </summary>
public class Comment : BaseProperties<long>
{
    /// <summary>
    ///     Who created the comment
    /// </summary>
    [JsonPropertyName("author")]
    [ReadOnly(true)]
    public User Author { get; set; }

    /// <summary>
    ///     The text of the comment
    /// </summary>
    [JsonPropertyName("body")]
    public string Body { get; set; }

    /// <summary>
    ///     When was the comment created
    /// </summary>
    [JsonPropertyName("created")]
    [ReadOnly(true)]
    public DateTimeOffset? Created { get; set; }

    /// <summary>
    ///     Who updated the comment
    /// </summary>
    [JsonPropertyName("updateAuthor")]
    [ReadOnly(true)]
    public User UpdateAuthor { get; set; }

    /// <summary>
    ///     When was the comment updated
    /// </summary>
    [JsonPropertyName("updated")]
    [ReadOnly(true)]
    public DateTimeOffset? Updated { get; set; }

    /// <summary>
    ///     Is a comment visible
    /// </summary>
    [JsonPropertyName("visibility")]
    public Visibility Visibility { get; set; }
}