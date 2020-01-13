// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Domains;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Internal;

namespace Dapplo.Jira
{
    /// <summary>
    ///     This holds all the greenhopper related extensions methods
    /// </summary>
    public static class GreenhopperExtensions
    {
        /// <summary>
        ///     This is a <i>not documented endpont</i>, and could fail with every update.
        ///     Get the sprint report for the desired sprint in the context of the target board.
        ///     The Greenhopper API is a leftover from when the agile functionality was not yet integrated.
        ///     Unfortunately they never finished the integration on the API level.
        ///     More information can be found in Atlassians Jira system: <a href="https://jira.atlassian.com/browse/JSWSERVER-12877">JSWSERVER-12877</a>
        ///     Endpoint pattern: https://{domain}.atlassian.net/rest/greenhopper/1.0/rapid/charts/sprintreport?rapidViewId={rapidViewId}&amp;sprintId={sprintId}
        /// </summary>
        /// <param name="jiraClient">IGreenhopperDomain to bind the extension method to</param>
        /// <param name="rapidViewId">key for the target board</param>
        /// <param name="sprintId">key for the desired sprint</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>SprintReport</returns>
        public static async Task<SprintReport> GetSprintReportAsync(this IGreenhopperDomain jiraClient, long rapidViewId, long sprintId, CancellationToken cancellationToken = default)
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
