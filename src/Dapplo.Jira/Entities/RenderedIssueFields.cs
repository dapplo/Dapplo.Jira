// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Container for the rendered fields
/// </summary>
public class RenderedIssueFields
{
    /// <summary>
    ///     The summary of the time spend on this issue
    /// </summary>
    [JsonPropertyName("aggregatetimespent")]
    public string AggregateTimeSpent { get; set; }

    /// <summary>
    ///     User who this issue is assigned to
    /// </summary>
    [JsonPropertyName("assignee")]
    public User Assignee { get; set; }

    /// <summary>
    ///     Attachments for this issue
    /// </summary>
    [JsonPropertyName("attachment")]
    public IList<RenderedAttachment> Attachments { get; set; }

    /// <summary>
    ///     Container for the comments for this issue
    /// </summary>
    [JsonPropertyName("comment")]
    public RenderedComments Comments { get; set; }

    /// <summary>
    ///     Components for this issue
    /// </summary>
    [JsonPropertyName("components")]
    public IList<Component> Components { get; set; }

    /// <summary>
    ///     When was this issue created
    /// </summary>
    [JsonPropertyName("created")]
    public string Created { get; set; }

    /// <summary>
    ///     User who created this issue
    /// </summary>
    [JsonPropertyName("creator")]
    public User Creator { get; set; }

    /// <summary>
    ///     All custom field values, or rather those that don't have a matching
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, object> CustomFields { get; } = new Dictionary<string, object>();

    /// <summary>
    ///     Description of this issue
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Versions where this issue is fixed
    /// </summary>
    [JsonPropertyName("fixVersions")]
    public IList<Version> FixVersions { get; set; }

    /// <summary>
    ///     Type of the issue
    /// </summary>
    [JsonPropertyName("issuetype")]
    public IssueType IssueType { get; set; }

    /// <summary>
    ///     Labels for this issue
    /// </summary>
    [JsonPropertyName("labels")]
    public IList<string> Labels { get; set; }

    /// <summary>
    ///     When was this issue viewed (by whom??)
    /// </summary>
    [JsonPropertyName("lastViewed")]
    public string LastViewed { get; set; }

    /// <summary>
    ///     Priority for this issue
    /// </summary>
    [JsonPropertyName("priority")]
    public Priority Priority { get; set; }

    /// <summary>
    ///     Progress for this issue
    /// </summary>
    [JsonPropertyName("progress")]
    public ProgressInfo Progress { get; set; }

    /// <summary>
    ///     Project to which this issue belongs
    /// </summary>
    [JsonPropertyName("project")]
    public Project Project { get; set; }

    /// <summary>
    ///     What user reported the issue?
    /// </summary>
    [JsonPropertyName("reporter")]
    public User Reporter { get; set; }

    /// <summary>
    ///     The resolution for this issue
    /// </summary>
    [JsonPropertyName("resolution")]
    public Resolution Resolution { get; set; }

    /// <summary>
    ///     Resolution date for this issue
    /// </summary>
    [JsonPropertyName("resolutiondate")]
    public string ResolutionData { get; set; }

    /// <summary>
    ///     Current status of the issue
    /// </summary>
    [JsonPropertyName("status")]
    public Status Status { get; set; }

    /// <summary>
    ///     Summary for the issue
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    /// <summary>
    ///     How much time is spent on this issue
    /// </summary>
    [JsonPropertyName("timespent")]
    public string TimeSpent { get; set; }

    /// <summary>
    ///     Time tracking information
    /// </summary>
    [JsonPropertyName("timetracking")]
    public TimeTracking TimeTracking { get; set; }

    /// <summary>
    ///     When was the last update
    /// </summary>
    [JsonPropertyName("updated")]
    public string Updated { get; set; }

    /// <summary>
    ///     Version for which this ticket is
    /// </summary>
    [JsonPropertyName("versions")]
    public IList<Version> Versions { get; set; }

    /// <summary>
    ///     Information on the watches for the ticket
    /// </summary>
    [JsonPropertyName("watches")]
    public Watches Watches { get; set; }

    /// <summary>
    ///     The worklog entries
    /// </summary>
    [JsonPropertyName("worklog")]
    public Worklogs Worklogs { get; set; }

    /// <summary>
    /// All linked issues
    /// </summary>
    [JsonPropertyName("issueLinks")]
    public IList<IssueLink> IssueLinks { get; set; }
}