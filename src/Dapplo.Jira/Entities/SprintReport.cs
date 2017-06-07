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

using Newtonsoft.Json;

#endregion

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Grasshopper Sprint Report
    /// </summary>
    [JsonObject]
    public class SprintReport
    {
        /// <summary>
        ///     The Sprint Report contents
        /// </summary>
        [JsonProperty(PropertyName = "contents")]
        public SprintReportContents Contents { get; set; }

        /// <summary>
        ///     Sprint information
        /// </summary>
        [JsonProperty(PropertyName = "sprint")]
        public SprintInReport Sprint { get; set; }

        /// <summary>
        ///     Indicates if the sprint supports pages
        /// </summary>
        [JsonProperty(PropertyName = "supportsPages")]
        public bool SupportsPages { get; set; }
    }
}
