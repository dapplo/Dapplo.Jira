// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira;

/// <summary>
///     This holds all the work log related extension methods
/// </summary>
public static class WorkLogDomainExtensions
{
#pragma warning disable IDE0090 // Use 'new(...)'
    private static readonly LogSource Log = new LogSource();
#pragma warning restore IDE0090 // Use 'new(...)'

    /// <summary>
    ///     Get worklogs information
    /// </summary>
    /// <param name="jiraClient">IWorkLogDomain to bind the extension method to</param>
    /// <param name="issueKey">the issue key</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Worklogs</returns>
    public static async Task<Worklogs> GetAsync(this IWorkLogDomain jiraClient, string issueKey, CancellationToken cancellationToken = default)
    {
        if (issueKey == null)
        {
            throw new ArgumentNullException(nameof(issueKey));
        }

        Log.Debug().WriteLine("Retrieving worklogs information for {0}", issueKey);
        var worklogUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "worklog");
        jiraClient.Behaviour.MakeCurrent();

        var response = await worklogUri.GetAsAsync<HttpResponse<Worklogs, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///     Get worklogs information for the updated worklogs object
    /// </summary>
    /// <param name="jiraClient">IWorkLogDomain to bind the extension method to</param>
    /// <param name="updatedWorklogs">UpdatedWorklogs</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>IList{WorkLog}</returns>
    public static Task<IList<Worklog>> GetAsync(this IWorkLogDomain jiraClient, UpdatedWorklogs updatedWorklogs, CancellationToken cancellationToken = default)
    {
        var updatedWorklogIds = updatedWorklogs.Select(wl => wl.Id);
        return GetAsync(jiraClient, updatedWorklogIds, cancellationToken);
    }

    /// <summary>
    ///     Get worklogs information for the supplied list of worklog IDs
    /// </summary>
    /// <param name="jiraClient">IWorkLogDomain to bind the extension method to</param>
    /// <param name="ids">IEnumerable with worklog ids</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>IList{WorkLog}</returns>
    public static async Task<IList<Worklog>> GetAsync(this IWorkLogDomain jiraClient, IEnumerable<long> ids, CancellationToken cancellationToken = default)
    {
        var worklogUri = jiraClient.JiraRestUri
            .AppendSegments("worklog", "list")
            .ExtendQuery("expand", "comment");
        jiraClient.Behaviour.MakeCurrent();
        var idContainer = new IdContainer
        {
            Ids = ids
        };
        var response = await worklogUri.PostAsync<HttpResponse<IList<Worklog>, Error>>(idContainer, cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///     Get recently updated worklogs information
    /// </summary>
    /// <param name="jiraClient">IWorkLogDomain to bind the extension method to</param>
    /// <param name="since">timestamp of since we need to get the updated worklogs</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>UpdatedWorklogs</returns>
    public static async Task<UpdatedWorklogs> GetUpdatedAsync(this IWorkLogDomain jiraClient, DateTimeOffset since, CancellationToken cancellationToken = default)
    {

        Log.Debug().WriteLine("Retrieving update worklogs information since {0}", since);
        var worklogUri = jiraClient.JiraRestUri
            .AppendSegments("worklog", "updated")
            .ExtendQuery("since", since.ToUnixTimeMilliseconds())
            .ExtendQuery("expand", "properties");
        jiraClient.Behaviour.MakeCurrent();

        var response = await worklogUri.GetAsAsync<HttpResponse<UpdatedWorklogs, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///     Log work for the specified issue
    /// </summary>
    /// <param name="jiraClient">IWorkLogDomain to bind the extension method to</param>
    /// <param name="issueKey">key for the issue</param>
    /// <param name="worklog">Worklog with the work which needs to be logged</param>
    /// <param name="adjustEstimate">
    ///     allows you to provide specific instructions to update the remaining time estimate of the
    ///     issue.
    /// </param>
    /// <param name="adjustValue">
    ///     e.g. "2d".
    ///     When "new" is selected for adjustEstimate the new value for the remaining estimate field.
    ///     When "manual" is selected for adjustEstimate the amount to reduce the remaining estimate by.
    /// </param>
    /// <param name="cancellationToken">CancellationToken</param>
    public static async Task<Worklog> CreateAsync(this IWorkLogDomain jiraClient, string issueKey, Worklog worklog, AdjustEstimate adjustEstimate = AdjustEstimate.Auto,
        string adjustValue = null, CancellationToken cancellationToken = default)
    {
        if (issueKey == null)
        {
            throw new ArgumentNullException(nameof(issueKey));
        }

        if (worklog == null)
        {
            throw new ArgumentNullException(nameof(worklog));
        }

        var worklogUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "worklog");
        if (adjustEstimate != AdjustEstimate.Auto)
        {
            worklogUri = worklogUri.ExtendQuery("adjustEstimate", adjustEstimate.EnumValueOf());
            switch (adjustEstimate)
            {
                case AdjustEstimate.Manual:
                    worklogUri = worklogUri.ExtendQuery("reduceBy", adjustValue);
                    break;
                case AdjustEstimate.New:
                    worklogUri = worklogUri.ExtendQuery("newEstimate", adjustValue);
                    break;
            }
        }

        jiraClient.Behaviour.MakeCurrent();

        var response = await worklogUri.PostAsync<HttpResponse<Worklog, Error>>(worklog, cancellationToken).ConfigureAwait(false);
        return response.HandleErrors(HttpStatusCode.Created);
    }

    /// <summary>
    ///     Update work log for the specified issue
    /// </summary>
    /// <param name="jiraClient">IWorkLogDomain to bind the extension method to</param>
    /// <param name="issueKey">key for the issue</param>
    /// <param name="worklog">Worklog with the work which needs to be updated</param>
    /// <param name="adjustEstimate">
    ///     allows you to provide specific instructions to update the remaining time estimate of the
    ///     issue.
    /// </param>
    /// <param name="adjustValue">
    ///     e.g. "2d".
    ///     When "new" is selected for adjustEstimate the new value for the remaining estimate field.
    ///     When "manual" is selected for adjustEstimate the amount to reduce the remaining estimate by.
    /// </param>
    /// <param name="cancellationToken">CancellationToken</param>
    public static async Task UpdateAsync(this IWorkLogDomain jiraClient, string issueKey, Worklog worklog, AdjustEstimate adjustEstimate = AdjustEstimate.Auto,
        string adjustValue = null, CancellationToken cancellationToken = default)
    {
        if (issueKey == null)
        {
            throw new ArgumentNullException(nameof(issueKey));
        }

        if (worklog == null)
        {
            throw new ArgumentNullException(nameof(worklog));
        }

        var worklogUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "worklog", worklog.Id);
        worklogUri = worklogUri.ExtendQuery("adjustEstimate", adjustEstimate.EnumValueOf());
        if (adjustEstimate != AdjustEstimate.Auto)
        {
            switch (adjustEstimate)
            {
                case AdjustEstimate.Manual:
                    worklogUri = worklogUri.ExtendQuery("reduceBy", adjustValue);
                    break;
                case AdjustEstimate.New:
                    worklogUri = worklogUri.ExtendQuery("newEstimate", adjustValue);
                    break;
            }
        }

        jiraClient.Behaviour.MakeCurrent();

        var response = await worklogUri.PutAsync<HttpResponseWithError<Error>>(worklog, cancellationToken).ConfigureAwait(false);
        response.HandleStatusCode();
    }

    /// <summary>
    ///     Delete the spefified Worklog
    /// </summary>
    /// <param name="jiraClient">IWorkLogDomain to bind the extension method to</param>
    /// <param name="issueKey">Key of the issue to delete to worklog for</param>
    /// <param name="worklog">Worklog to delete</param>
    /// <param name="adjustEstimate">
    ///     allows you to provide specific instructions to update the remaining time estimate of the
    ///     issue.
    /// </param>
    /// <param name="adjustValue">
    ///     e.g. "2d".
    ///     When "new" is selected for adjustEstimate the new value for the remaining estimate field.
    ///     When "manual" is selected for adjustEstimate the amount to reduce the remaining estimate by.
    /// </param>
    /// <param name="cancellationToken">CancellationToken</param>
    public static async Task DeleteAsync(this IWorkLogDomain jiraClient, string issueKey, Worklog worklog, AdjustEstimate adjustEstimate = AdjustEstimate.Auto,
        string adjustValue = null, CancellationToken cancellationToken = default)
    {
        if (issueKey == null)
        {
            throw new ArgumentNullException(nameof(issueKey));
        }

        if (worklog == null)
        {
            throw new ArgumentNullException(nameof(worklog));
        }

        var worklogUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "worklog", worklog.Id);
        if (adjustEstimate != AdjustEstimate.Auto)
        {
            worklogUri = worklogUri.ExtendQuery("adjustEstimate", adjustEstimate.EnumValueOf());
            switch (adjustEstimate)
            {
                case AdjustEstimate.Manual:
                    worklogUri = worklogUri.ExtendQuery("reduceBy", adjustValue);
                    break;
                case AdjustEstimate.New:
                    worklogUri = worklogUri.ExtendQuery("newEstimate", adjustValue);
                    break;
            }
        }

        jiraClient.Behaviour.MakeCurrent();

        var response = await worklogUri.DeleteAsync<HttpResponse>(cancellationToken).ConfigureAwait(false);
        response.HandleStatusCode(HttpStatusCode.NoContent);
    }
}
