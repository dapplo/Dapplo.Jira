#region Dapplo 2017 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017 Dapplo
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
        public long Id { get; set; }

        public bool Hidden { get; set; }

        public string TypeName { get; set; }

        public string TypeId { get; set; }

        public string Summary { get; set; }

        public Uri TypeUrl { get; set; }

        public Uri PriorityUrl { get; set; }

        public string priorityName { get; set; }

        public bool Done { get; set; }

        public string Assignee { get; set; }

        public string AssigneeKey { get; set; }

        public Uri AvatarUrl { get; set; }

        public bool HasCustomUserAvatar { get; set; }

        public bool Flagged { get; set; }

        public string Epic { get; set; }

        public EpicField EpicField { get; set; }

        public StatisticField ColumnStatistic { get; set; }

        public StatisticField CurrentEstimateStatistic { get; set; }

        public bool EstimateStatisticRequired { get; set; }

        public StatisticField EstimateStatistic { get; set; }

        public StatisticField TrackingStatistic { get; set; }

        public string StatusId { get; set; }

        public string StatusName { get; set; }

        public Uri StatusUrl { get; set; }

        public Status Status { get; set; }

        public IEnumerable<string> FixVersions { get; set; }

        public long ProjectId { get; set; }

        public int LinkedPagesCount { get; set; }
    }
}
