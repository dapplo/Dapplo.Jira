// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Report Issue information
/// </summary>
public class ReportIssue : IssueBase
{
    /// <summary>
    ///     Is this issue hidden?
    /// </summary>
    [JsonPropertyName("hidden")]
    public bool Hidden { get; set; }

    /// <summary>
    ///     Name of the type
    /// </summary>
    [JsonPropertyName("typeName")]
    public string TypeName { get; set; }

    /// <summary>
    ///     Type ID for the issue
    /// </summary>
    [JsonPropertyName("typeId")]
    public string TypeId { get; set; }

    /// <summary>
    ///     Issue summary
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    /// <summary>
    ///     Url to the type of issue
    /// </summary>
    [JsonPropertyName("typeUrl")]
    public Uri TypeUrl { get; set; }

    /// <summary>
    ///     Url to the priority
    /// </summary>
    [JsonPropertyName("priorityUrl")]
    public Uri PriorityUrl { get; set; }

    /// <summary>
    ///     Name of the priority
    /// </summary>
    [JsonPropertyName("priorityName")]
    public string PriorityName { get; set; }

    /// <summary>
    ///     Is the issue done?
    /// </summary>
    [JsonPropertyName("done")]
    public bool Done { get; set; }

    /// <summary>
    ///     Who is this issue assigned to?
    /// </summary>
    [JsonPropertyName("assignee")]
    public string Assignee { get; set; }

    /// <summary>
    ///     Key of the person this issue is assigned to
    /// </summary>
    [JsonPropertyName("assigneeKey")]
    public string AssigneeKey { get; set; }

    /// <summary>
    ///     Uri to the avatar of the current user
    /// </summary>
    [JsonPropertyName("avatarUrl")]
    public Uri AvatarUrl { get; set; }

    /// <summary>
    ///     Does user have a custom avatar?
    /// </summary>
    [JsonPropertyName("hasCustomUserAvatar")]
    public bool HasCustomUserAvatar { get; set; }

    /// <summary>
    ///     Is this issue flagged?
    /// </summary>
    [JsonPropertyName("flagged")]
    public bool Flagged { get; set; }

    /// <summary>
    ///     The epic this issue belongs to
    /// </summary>
    [JsonPropertyName("epic")]
    public string Epic { get; set; }

    /// <summary>
    ///     The epic field
    /// </summary>
    [JsonPropertyName("epicField")]
    public EpicField EpicField { get; set; }

    /// <summary>
    ///     The statics for the column
    /// </summary>
    [JsonPropertyName("columnStatistic")]
    public StatisticField ColumnStatistic { get; set; }

    /// <summary>
    ///     Current estimate
    /// </summary>
    [JsonPropertyName("currentEstimateStatistic")]
    public StatisticField CurrentEstimateStatistic { get; set; }

    /// <summary>
    ///     Tells if the estimate statistics are required
    /// </summary>
    [JsonPropertyName("estimateStatisticRequired")]
    public bool EstimateStatisticRequired { get; set; }

    /// <summary>
    ///     Estimate statistics
    /// </summary>
    [JsonPropertyName("estimateStatistic")]
    public StatisticField EstimateStatistic { get; set; }

    /// <summary>
    ///     Tracking statistics
    /// </summary>
    [JsonPropertyName("trackingStatistic")]
    public StatisticField TrackingStatistic { get; set; }

    /// <summary>
    ///     Status of the issue
    /// </summary>
    [JsonPropertyName("status")]
    public Status Status { get; set; }

    /// <summary>
    ///     Versions in which this issue is fixed
    /// </summary>
    [JsonPropertyName("fixVersions")]
    public IEnumerable<string> FixVersions { get; set; }

    /// <summary>
    ///     Project where the issue belongs to
    /// </summary>
    [JsonPropertyName("projectId")]
    public long ProjectId { get; set; }

    /// <summary>
    ///     Number of linked pages, used to display the count of pages for the sprint.
    /// </summary>
    [JsonPropertyName("linkedPagesCount")]
    public int LinkedPagesCount { get; set; }
}