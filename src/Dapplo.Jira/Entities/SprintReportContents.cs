#region Dapplo 2017-2018 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2018 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Jira
// 
// Dapplo.Jira is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Jira is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings

using Newtonsoft.Json;
using System.Collections.Generic;

#endregion

namespace Dapplo.Jira.Entities
{
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
}