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
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Domains;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Internal;

#endregion

namespace Dapplo.Jira
{
    /// <summary>
    ///     This holds all the greenhopper related extensions methods
    /// </summary>
    public static class GreenhopperExtensions
    {
        /// <summary>
        ///     Get the sprint report for the desired sprint in the context of the target board.
        ///     See: <i>Not documented endpont</i>
        ///     Endpoint pattern: https://{domain}.atlassian.net/rest/greenhopper/1.0/rapid/charts/sprintreport?rapidViewId={rapidViewId}&sprintId={sprintId}
        /// </summary>
        /// <param name="jiraClient">IGreenhopperDomain to bind the extension method to</param>
        /// <param name="rapidViewId">key for the target board</param>
        /// <param name="sprintId">key for the desired sprint</param>
        /// /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task<SprintReport> GetSprintReportAsync(this IGreenhopperDomain jiraClient, long rapidViewId, long sprintId, CancellationToken cancellationToken = default(CancellationToken))
        {
            jiraClient.Behaviour.MakeCurrent();
            var sprintReportUri = jiraClient.JiraGreenhopperRestUri.AppendSegments("rapid", "charts", "sprintreport")
                                        .ExtendQuery("rapidViewId", rapidViewId)
                                        .ExtendQuery("sprintId", sprintId);
            var response = await sprintReportUri.GetAsAsync<HttpResponse<SprintReport, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }
    }
}
