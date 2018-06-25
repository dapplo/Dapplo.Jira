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

using System;
using Newtonsoft.Json;
using System.Collections.Generic;

#endregion

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Report Issue information
    /// </summary>
    [JsonObject]
    public class ReportIssue : IssueBase
    {
        /// <summary>
        ///     Is this issue hidden?
        /// </summary>
        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        /// <summary>
        ///     Name of the type
        /// </summary>
        [JsonProperty("typeName")]
        public string TypeName { get; set; }

        /// <summary>
        ///     Type ID for the issue
        /// </summary>
        [JsonProperty("typeId")]
        public string TypeId { get; set; }

        /// <summary>
        ///     Issue summary
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; }

        /// <summary>
        ///     Url to the type of issue
        /// </summary>
        [JsonProperty("typeUrl")]
        public Uri TypeUrl { get; set; }

        /// <summary>
        ///     Url to the priority
        /// </summary>
        [JsonProperty("priorityUrl")]
        public Uri PriorityUrl { get; set; }

        /// <summary>
        ///     Name of the priority
        /// </summary>
        [JsonProperty("priorityName")]
        public string PriorityName { get; set; }

        /// <summary>
        ///     Is the issue done?
        /// </summary>
        [JsonProperty("done")]
        public bool Done { get; set; }

        /// <summary>
        ///     Who is this issue assigned to?
        /// </summary>
        [JsonProperty("assignee")]
        public string Assignee { get; set; }

        /// <summary>
        ///     Key of the person this issue is assigned to
        /// </summary>
        [JsonProperty("assigneeKey")]
        public string AssigneeKey { get; set; }

        /// <summary>
        ///     Uri to the avatar of the current user
        /// </summary>
        [JsonProperty("avatarUrl")]
        public Uri AvatarUrl { get; set; }

        /// <summary>
        ///     Does user have a customavatar?
        /// </summary>
        [JsonProperty("hasCustomUserAvatar")]
        public bool HasCustomUserAvatar { get; set; }

        /// <summary>
        ///     Is this issue flagged?
        /// </summary>
        [JsonProperty("flagged")]
        public bool Flagged { get; set; }

        /// <summary>
        ///     The epic this issue belongs to
        /// </summary>
        [JsonProperty("epic")]
        public string Epic { get; set; }

        /// <summary>
        ///     The epic field
        /// </summary>
        [JsonProperty("epicField")]
        public EpicField EpicField { get; set; }

        /// <summary>
        ///     The statics for the colum
        /// </summary>
        [JsonProperty("columnStatistic")]
        public StatisticField ColumnStatistic { get; set; }

        /// <summary>
        ///     Current estimate
        /// </summary>
        [JsonProperty("currentEstimateStatistic")]
        public StatisticField CurrentEstimateStatistic { get; set; }

        /// <summary>
        ///     Tells if the estimate statistics are required
        /// </summary>
        [JsonProperty("estimateStatisticRequired")]
        public bool EstimateStatisticRequired { get; set; }

        /// <summary>
        ///     Estimate statistics
        /// </summary>
        [JsonProperty("estimateStatistic")]
        public StatisticField EstimateStatistic { get; set; }

        /// <summary>
        ///     Tracking statistics
        /// </summary>
        [JsonProperty("trackingStatistic")]
        public StatisticField TrackingStatistic { get; set; }

        /// <summary>
        ///     Status of the issue
        /// </summary>
        [JsonProperty("status")]
        public Status Status { get; set; }

        /// <summary>
        ///     Versions in which this issue is fixed
        /// </summary>
        [JsonProperty("fixVersions")]
        public IEnumerable<string> FixVersions { get; set; }

        /// <summary>
        ///     Project where the issue belongs to
        /// </summary>
        [JsonProperty("projectId")]
        public long ProjectId { get; set; }

        /// <summary>
        ///     Number of linked pages, used to display the count of pages for the sprint.
        /// </summary>
        [JsonProperty("linkedPagesCount")]
        public int LinkedPagesCount { get; set; }
    }
}
