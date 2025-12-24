// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using Dapplo.Jira.Json;
using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Worklog information
/// </summary>
public class Worklog : BaseProperties<string>
{
    /// <summary>
    /// Default constructor for a work log
    /// </summary>
    public Worklog()
    {
    }

    /// <summary>
    /// Default constructor with a TimeSpan for the TimeSpentSeconds
    /// </summary>
    /// <param name="timeSpent">TimeSpan</param>
    public Worklog(TimeSpan timeSpent)
    {
        TimeSpentSeconds = (int)timeSpent.TotalSeconds;
    }

    /// <summary>
    ///     Author of this worklog
    /// </summary>
    [JsonPropertyName("author")]
    public User Author { get; set; }

    /// <summary>
    ///     Comment for this worklog
    /// </summary>
    [JsonPropertyName("comment")]
    public string Comment { get; set; }

    /// <summary>
    ///     When was the worklog created
    /// </summary>
    [JsonPropertyName("created")]
    [ReadOnly(true)]
    public DateTimeOffset? Created { get; set; }

    /// <summary>
    ///     When was the worklog started
    /// </summary>
    [JsonPropertyName("started")]
    [JsonConverter(typeof(JiraDateTimeOffsetConverter))]
    public DateTimeOffset? Started { get; set; }

    /// <summary>
    ///     Time spent in this worklog, this is a number and qualifier (h = hour, d = day etc)
    /// </summary>
    [JsonPropertyName("timeSpent")]
    public string TimeSpent { get; set; }

    /// <summary>
    ///     Time spent in this worklog, in seconds
    /// </summary>
    [JsonPropertyName("timeSpentSeconds")]
    public long? TimeSpentSeconds { get; set; }

    /// <summary>
    ///     Who updated this worklog, this cannot be updated
    /// </summary>
    [JsonPropertyName("updateAuthor")]
    [ReadOnly(true)]
    public User UpdateAuthor { get; set; }

    /// <summary>
    ///     When was the worklog updated, this cannot be updated
    /// </summary>
    [JsonPropertyName("updated")]
    [ReadOnly(true)]
    public DateTimeOffset? Updated { get; set; }

    /// <summary>
    ///     Visibility
    /// </summary>
    [JsonPropertyName("visibility")]
    public Visibility Visibility { get; set; }
}