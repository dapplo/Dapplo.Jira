// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Grasshopper Sprint Report
/// </summary>
[JsonObject]
public class SprintReportContents
{
    /// <summary>
    ///     The Completed Issues on the sprint
    /// </summary>
    [JsonProperty("completedIssues")]
    public IEnumerable<ReportIssue> CompletedIssues { get; set; }

    /// <summary>
    ///     The Issues Non Completed on the sprint
    /// </summary>
    [JsonProperty("issuesNotCompletedInCurrentSprint")]
    public IEnumerable<ReportIssue> IssuesNotCompletedInCurrentSprint { get; set; }

    /// <summary>
    ///     The Puntes Issues on the sprint
    /// </summary>
    [JsonProperty("puntedIssues")]
    public IEnumerable<ReportIssue> PuntedIssues { get; set; }

    /// <summary>
    ///     The Issues Completed on another sprint
    /// </summary>
    [JsonProperty("issuesCompletedInAnotherSprint")]
    public IEnumerable<ReportIssue> IssuesCompletedInAnotherSprint { get; set; }

    /// <summary>
    ///     Initial Estimate sum for completed issues
    /// </summary>
    [JsonProperty("completedIssuesInitialEstimateSum")]
    public ValueField CompletedIssuesInitialEstimateSum { get; set; }

    /// <summary>
    ///     Estimate sum for completed issues
    /// </summary>
    [JsonProperty("completedIssuesEstimateSum")]
    public ValueField CompletedIssuesEstimateSum { get; set; }

    /// <summary>
    ///     Initial Estimate sum for issues not completed
    /// </summary>
    [JsonProperty("issuesNotCompletedInitialEstimateSum")]
    public ValueField IssuesNotCompletedInitialEstimateSum { get; set; }

    /// <summary>
    ///     Estimate sum for issues not completed
    /// </summary>
    [JsonProperty("issuesNotCompletedEstimateSum")]
    public ValueField IssuesNotCompletedEstimateSum { get; set; }

    /// <summary>
    ///     Estimate sum for all issues
    /// </summary>
    [JsonProperty("allIssuesEstimateSum")]
    public ValueField AllIssuesEstimateSum { get; set; }

    /// <summary>
    ///     Initial Estimate sum for punted issues
    /// </summary>
    [JsonProperty("puntedIssuesInitialEstimateSum")]
    public ValueField PuntedIssuesInitialEstimateSum { get; set; }

    /// <summary>
    ///     Estimate sum for punted issues
    /// </summary>
    [JsonProperty("puntedIssuesEstimateSum")]
    public ValueField PuntedIssuesEstimateSum { get; set; }

    /// <summary>
    ///     Initial Estimate sum for issues completed in another sprint
    /// </summary>
    [JsonProperty("issuesCompletedInAnotherSprintInitialEstimateSum")]
    public ValueField IssuesCompletedInAnotherSprintInitialEstimateSum { get; set; }

    /// <summary>
    ///     Estimate sum for issues completed in another sprint
    /// </summary>
    [JsonProperty("issuesCompletedInAnotherSprintEstimateSum")]
    public ValueField IssuesCompletedInAnotherSprintEstimateSum { get; set; }

    /// <summary>
    ///     Keys for Issues added during the sprint
    /// </summary>
    [JsonProperty("issueKeysAddedDuringSprint")]
    public IDictionary<string, bool> IssueKeysAddedDuringSprint { get; set; }
}