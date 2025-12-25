// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Grasshopper Sprint Report
/// </summary>
public class SprintReportContents
{
    /// <summary>
    ///     The Completed Issues on the sprint
    /// </summary>
    [JsonPropertyName("completedIssues")]
    public IEnumerable<ReportIssue> CompletedIssues { get; set; }

    /// <summary>
    ///     The Issues Non Completed on the sprint
    /// </summary>
    [JsonPropertyName("issuesNotCompletedInCurrentSprint")]
    public IEnumerable<ReportIssue> IssuesNotCompletedInCurrentSprint { get; set; }

    /// <summary>
    ///     The Puntes Issues on the sprint
    /// </summary>
    [JsonPropertyName("puntedIssues")]
    public IEnumerable<ReportIssue> PuntedIssues { get; set; }

    /// <summary>
    ///     The Issues Completed on another sprint
    /// </summary>
    [JsonPropertyName("issuesCompletedInAnotherSprint")]
    public IEnumerable<ReportIssue> IssuesCompletedInAnotherSprint { get; set; }

    /// <summary>
    ///     Initial Estimate sum for completed issues
    /// </summary>
    [JsonPropertyName("completedIssuesInitialEstimateSum")]
    public ValueField CompletedIssuesInitialEstimateSum { get; set; }

    /// <summary>
    ///     Estimate sum for completed issues
    /// </summary>
    [JsonPropertyName("completedIssuesEstimateSum")]
    public ValueField CompletedIssuesEstimateSum { get; set; }

    /// <summary>
    ///     Initial Estimate sum for issues not completed
    /// </summary>
    [JsonPropertyName("issuesNotCompletedInitialEstimateSum")]
    public ValueField IssuesNotCompletedInitialEstimateSum { get; set; }

    /// <summary>
    ///     Estimate sum for issues not completed
    /// </summary>
    [JsonPropertyName("issuesNotCompletedEstimateSum")]
    public ValueField IssuesNotCompletedEstimateSum { get; set; }

    /// <summary>
    ///     Estimate sum for all issues
    /// </summary>
    [JsonPropertyName("allIssuesEstimateSum")]
    public ValueField AllIssuesEstimateSum { get; set; }

    /// <summary>
    ///     Initial Estimate sum for punted issues
    /// </summary>
    [JsonPropertyName("puntedIssuesInitialEstimateSum")]
    public ValueField PuntedIssuesInitialEstimateSum { get; set; }

    /// <summary>
    ///     Estimate sum for punted issues
    /// </summary>
    [JsonPropertyName("puntedIssuesEstimateSum")]
    public ValueField PuntedIssuesEstimateSum { get; set; }

    /// <summary>
    ///     Initial Estimate sum for issues completed in another sprint
    /// </summary>
    [JsonPropertyName("issuesCompletedInAnotherSprintInitialEstimateSum")]
    public ValueField IssuesCompletedInAnotherSprintInitialEstimateSum { get; set; }

    /// <summary>
    ///     Estimate sum for issues completed in another sprint
    /// </summary>
    [JsonPropertyName("issuesCompletedInAnotherSprintEstimateSum")]
    public ValueField IssuesCompletedInAnotherSprintEstimateSum { get; set; }

    /// <summary>
    ///     Keys for Issues added during the sprint
    /// </summary>
    [JsonPropertyName("issueKeysAddedDuringSprint")]
    public IDictionary<string, bool> IssueKeysAddedDuringSprint { get; set; }
}