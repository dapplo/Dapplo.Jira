// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira;

/// <summary>
///     This holds all the screen related extension methods
/// </summary>
public static class ScreenDomainExtensions
{
#pragma warning disable IDE0090 // Use 'new(...)'
    private static readonly LogSource Log = new LogSource();
#pragma warning restore IDE0090 // Use 'new(...)'

    /// <summary>
    ///     Get all screens
    ///     See: https://docs.atlassian.com/software/jira/docs/api/REST/latest/#api/2/screens
    /// </summary>
    /// <param name="jiraClient">IScreenDomain to bind the extension method to</param>
    /// <param name="startAt">The index of the first screen to return (0 based)</param>
    /// <param name="maxResults">The maximum number of screens to return (defaults to 100)</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>List of screens</returns>
    public static async Task<Screens> GetAllScreensAsync(this IScreenDomain jiraClient, int startAt = 0, int maxResults = 100,
        CancellationToken cancellationToken = default)
    {
        Log.Debug().WriteLine("Retrieving all screens");

        var screensUri = jiraClient.JiraRestUri.AppendSegments("screens")
            .ExtendQuery("startAt", startAt)
            .ExtendQuery("maxResults", maxResults);

        jiraClient.Behaviour.MakeCurrent();
        var response = await screensUri.GetAsAsync<HttpResponse<Screens, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///     Get screen by ID
    ///     See: https://docs.atlassian.com/software/jira/docs/api/REST/latest/#api/2/screens-getScreen
    /// </summary>
    /// <param name="jiraClient">IScreenDomain to bind the extension method to</param>
    /// <param name="screenId">ID of the screen</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Screen</returns>
    public static async Task<Screen> GetScreenAsync(this IScreenDomain jiraClient, long screenId, CancellationToken cancellationToken = default)
    {
        if (screenId <= 0)
        {
            throw new ArgumentException("Screen ID must be greater than 0", nameof(screenId));
        }

        Log.Debug().WriteLine("Retrieving screen {0}", screenId);

        var screenUri = jiraClient.JiraRestUri.AppendSegments("screens", screenId);

        jiraClient.Behaviour.MakeCurrent();
        var response = await screenUri.GetAsAsync<HttpResponse<Screen, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///     Get all tabs for a screen
    ///     See: https://docs.atlassian.com/software/jira/docs/api/REST/latest/#api/2/screens/{screenId}/tabs
    /// </summary>
    /// <param name="jiraClient">IScreenDomain to bind the extension method to</param>
    /// <param name="screenId">ID of the screen</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>List of screen tabs</returns>
    public static async Task<IList<ScreenTab>> GetScreenTabsAsync(this IScreenDomain jiraClient, long screenId, CancellationToken cancellationToken = default)
    {
        if (screenId <= 0)
        {
            throw new ArgumentException("Screen ID must be greater than 0", nameof(screenId));
        }

        Log.Debug().WriteLine("Retrieving tabs for screen {0}", screenId);

        var tabsUri = jiraClient.JiraRestUri.AppendSegments("screens", screenId, "tabs");

        jiraClient.Behaviour.MakeCurrent();
        var response = await tabsUri.GetAsAsync<HttpResponse<IList<ScreenTab>, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///     Add a tab to a screen
    ///     See: https://docs.atlassian.com/software/jira/docs/api/REST/latest/#api/2/screens/{screenId}/tabs
    /// </summary>
    /// <param name="jiraClient">IScreenDomain to bind the extension method to</param>
    /// <param name="screenId">ID of the screen</param>
    /// <param name="tab">Screen tab to add</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Created screen tab</returns>
    public static async Task<ScreenTab> AddScreenTabAsync(this IScreenDomain jiraClient, long screenId, ScreenTab tab, CancellationToken cancellationToken = default)
    {
        if (screenId <= 0)
        {
            throw new ArgumentException("Screen ID must be greater than 0", nameof(screenId));
        }

        if (tab == null)
        {
            throw new ArgumentNullException(nameof(tab));
        }

        Log.Debug().WriteLine("Adding tab {0} to screen {1}", tab.Name, screenId);

        var tabsUri = jiraClient.JiraRestUri.AppendSegments("screens", screenId, "tabs");

        jiraClient.Behaviour.MakeCurrent();
        var response = await tabsUri.PostAsync<HttpResponse<ScreenTab, Error>>(tab, cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///     Get all fields for a screen tab
    ///     See: https://docs.atlassian.com/software/jira/docs/api/REST/latest/#api/2/screens/{screenId}/tabs/{tabId}/fields
    /// </summary>
    /// <param name="jiraClient">IScreenDomain to bind the extension method to</param>
    /// <param name="screenId">ID of the screen</param>
    /// <param name="tabId">ID of the tab</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>List of screen fields</returns>
    public static async Task<IList<ScreenField>> GetScreenFieldsAsync(this IScreenDomain jiraClient, long screenId, long tabId, CancellationToken cancellationToken = default)
    {
        if (screenId <= 0)
        {
            throw new ArgumentException("Screen ID must be greater than 0", nameof(screenId));
        }

        if (tabId <= 0)
        {
            throw new ArgumentException("Tab ID must be greater than 0", nameof(tabId));
        }

        Log.Debug().WriteLine("Retrieving fields for screen {0} tab {1}", screenId, tabId);

        var fieldsUri = jiraClient.JiraRestUri.AppendSegments("screens", screenId, "tabs", tabId, "fields");

        jiraClient.Behaviour.MakeCurrent();
        var response = await fieldsUri.GetAsAsync<HttpResponse<IList<ScreenField>, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///     Get available fields for a screen
    ///     See: https://docs.atlassian.com/software/jira/docs/api/REST/latest/#api/2/screens/{screenId}/availableFields
    /// </summary>
    /// <param name="jiraClient">IScreenDomain to bind the extension method to</param>
    /// <param name="screenId">ID of the screen</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>List of available fields</returns>
    public static async Task<IList<ScreenableField>> GetAvailableScreenFieldsAsync(this IScreenDomain jiraClient, long screenId, CancellationToken cancellationToken = default)
    {
        if (screenId <= 0)
        {
            throw new ArgumentException("Screen ID must be greater than 0", nameof(screenId));
        }

        Log.Debug().WriteLine("Retrieving available fields for screen {0}", screenId);

        var fieldsUri = jiraClient.JiraRestUri.AppendSegments("screens", screenId, "availableFields");

        jiraClient.Behaviour.MakeCurrent();
        var response = await fieldsUri.GetAsAsync<HttpResponse<IList<ScreenableField>, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///     Add a field to a screen tab
    ///     See: https://docs.atlassian.com/software/jira/docs/api/REST/latest/#api/2/screens/{screenId}/tabs/{tabId}/fields
    /// </summary>
    /// <param name="jiraClient">IScreenDomain to bind the extension method to</param>
    /// <param name="screenId">ID of the screen</param>
    /// <param name="tabId">ID of the tab</param>
    /// <param name="fieldId">ID of the field to add</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Added screen field</returns>
    public static async Task<ScreenField> AddScreenFieldAsync(this IScreenDomain jiraClient, long screenId, long tabId, string fieldId, CancellationToken cancellationToken = default)
    {
        if (screenId <= 0)
        {
            throw new ArgumentException("Screen ID must be greater than 0", nameof(screenId));
        }

        if (tabId <= 0)
        {
            throw new ArgumentException("Tab ID must be greater than 0", nameof(tabId));
        }

        if (string.IsNullOrWhiteSpace(fieldId))
        {
            throw new ArgumentException("Field ID cannot be null or empty", nameof(fieldId));
        }

        Log.Debug().WriteLine("Adding field {0} to screen {1} tab {2}", fieldId, screenId, tabId);

        var fieldsUri = jiraClient.JiraRestUri.AppendSegments("screens", screenId, "tabs", tabId, "fields");

        var fieldData = new { fieldId };

        jiraClient.Behaviour.MakeCurrent();
        var response = await fieldsUri.PostAsync<HttpResponse<ScreenField, Error>>(fieldData, cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///     Remove a field from a screen tab
    ///     See: https://docs.atlassian.com/software/jira/docs/api/REST/latest/#api/2/screens/{screenId}/tabs/{tabId}/fields/{id}
    /// </summary>
    /// <param name="jiraClient">IScreenDomain to bind the extension method to</param>
    /// <param name="screenId">ID of the screen</param>
    /// <param name="tabId">ID of the tab</param>
    /// <param name="fieldId">ID of the field to remove</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public static async Task RemoveScreenFieldAsync(this IScreenDomain jiraClient, long screenId, long tabId, string fieldId, CancellationToken cancellationToken = default)
    {
        if (screenId <= 0)
        {
            throw new ArgumentException("Screen ID must be greater than 0", nameof(screenId));
        }

        if (tabId <= 0)
        {
            throw new ArgumentException("Tab ID must be greater than 0", nameof(tabId));
        }

        if (string.IsNullOrWhiteSpace(fieldId))
        {
            throw new ArgumentException("Field ID cannot be null or empty", nameof(fieldId));
        }

        Log.Debug().WriteLine("Removing field {0} from screen {1} tab {2}", fieldId, screenId, tabId);

        var fieldUri = jiraClient.JiraRestUri.AppendSegments("screens", screenId, "tabs", tabId, "fields", fieldId);

        jiraClient.Behaviour.MakeCurrent();
        var response = await fieldUri.DeleteAsync<HttpResponse>(cancellationToken).ConfigureAwait(false);
        response.HandleStatusCode(HttpStatusCode.NoContent);
    }
}
