// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using JiraGroup = Dapplo.Jira.Entities.Group;
using JiraGroupWithUsers = Dapplo.Jira.Entities.GroupWithUsers;

namespace Dapplo.Jira;

/// <summary>
///     This holds all the group related extension methods
/// </summary>
public static class GroupDomainExtensions
{
#pragma warning disable IDE0090 // Use 'new(...)'
    private static readonly LogSource Log = new LogSource();
#pragma warning restore IDE0090 // Use 'new(...)'

    /// <summary>
    ///     Search for groups
    ///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/group-findGroups
    /// </summary>
    /// <param name="jiraClient">IGroupDomain to bind the extension method to</param>
    /// <param name="query">A query string used to search for groups</param>
    /// <param name="excludeGroups">Comma separated list of groups to exclude from the search</param>
    /// <param name="maxResults">Maximum number of results returned, default is 20</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>SearchResult with Group</returns>
    public static async Task<SearchResult<JiraGroup, string>> SearchAsync(this IGroupDomain jiraClient, string query = null, 
        string excludeGroups = null, int maxResults = 20, CancellationToken cancellationToken = default)
    {
        Log.Debug().WriteLine("Searching for groups with query {0}", query);

        jiraClient.Behaviour.MakeCurrent();
        var searchUri = jiraClient.JiraRestUri.AppendSegments("groups", "picker");

        var queryParams = new Dictionary<string, object>
        {
            { "maxResults", maxResults }
        };

        if (!string.IsNullOrEmpty(query))
        {
            queryParams.Add("query", query);
        }

        if (!string.IsNullOrEmpty(excludeGroups))
        {
            queryParams.Add("exclude", excludeGroups);
        }

        searchUri = searchUri.ExtendQuery(queryParams);

        var response = await searchUri.GetAsAsync<HttpResponse<SearchResult<JiraGroup, string>, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///     Create a new group
    ///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/group-createGroup
    /// </summary>
    /// <param name="jiraClient">IGroupDomain to bind the extension method to</param>
    /// <param name="groupName">Name of the group to create</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Group</returns>
    public static async Task<JiraGroup> CreateAsync(this IGroupDomain jiraClient, string groupName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(groupName))
        {
            throw new ArgumentNullException(nameof(groupName));
        }

        Log.Debug().WriteLine("Creating group {0}", groupName);

        var groupUri = jiraClient.JiraRestUri.AppendSegments("group");
        jiraClient.Behaviour.MakeCurrent();

        var groupToCreate = new JiraGroup
        {
            Name = groupName
        };

        var response = await groupUri.PostAsync<HttpResponse<JiraGroup, Error>>(groupToCreate, cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///     Delete a group
    ///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/group-removeGroup
    /// </summary>
    /// <param name="jiraClient">IGroupDomain to bind the extension method to</param>
    /// <param name="groupName">Name of the group to delete</param>
    /// <param name="swapGroup">Optional name of group to transfer visibility restrictions to</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Task</returns>
    public static async Task DeleteAsync(this IGroupDomain jiraClient, string groupName, string swapGroup = null, 
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(groupName))
        {
            throw new ArgumentNullException(nameof(groupName));
        }

        Log.Debug().WriteLine("Deleting group {0}", groupName);

        var groupUri = jiraClient.JiraRestUri.AppendSegments("group").ExtendQuery("groupname", groupName);
        
        if (!string.IsNullOrEmpty(swapGroup))
        {
            groupUri = groupUri.ExtendQuery("swapGroup", swapGroup);
        }

        jiraClient.Behaviour.MakeCurrent();
        var response = await groupUri.DeleteAsync<HttpResponse<string, Error>>(cancellationToken).ConfigureAwait(false);
        response.HandleErrors();
    }

    /// <summary>
    ///     Add user to group
    ///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/group-addUserToGroup
    /// </summary>
    /// <param name="jiraClient">IGroupDomain to bind the extension method to</param>
    /// <param name="groupName">Name of the group</param>
    /// <param name="username">Username of the user to add (deprecated, use accountId)</param>
    /// <param name="accountId">AccountId of the user to add</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Group</returns>
    public static async Task<JiraGroup> AddUserAsync(this IGroupDomain jiraClient, string groupName, string username = null, 
        string accountId = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(groupName))
        {
            throw new ArgumentNullException(nameof(groupName));
        }

        if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(accountId))
        {
            throw new ArgumentException("Either username or accountId must be provided");
        }

        Log.Debug().WriteLine("Adding user to group {0}", groupName);

        var groupUri = jiraClient.JiraRestUri.AppendSegments("group", "user").ExtendQuery("groupname", groupName);
        jiraClient.Behaviour.MakeCurrent();

        var userToAdd = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(accountId))
        {
            userToAdd.Add("accountId", accountId);
        }
        else
        {
            userToAdd.Add("name", username);
        }

        var response = await groupUri.PostAsync<HttpResponse<JiraGroup, Error>>(userToAdd, cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///     Remove user from group
    ///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/group-removeUserFromGroup
    /// </summary>
    /// <param name="jiraClient">IGroupDomain to bind the extension method to</param>
    /// <param name="groupName">Name of the group</param>
    /// <param name="username">Username of the user to remove (deprecated, use accountId)</param>
    /// <param name="accountId">AccountId of the user to remove</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Task</returns>
    public static async Task RemoveUserAsync(this IGroupDomain jiraClient, string groupName, string username = null, 
        string accountId = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(groupName))
        {
            throw new ArgumentNullException(nameof(groupName));
        }

        if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(accountId))
        {
            throw new ArgumentException("Either username or accountId must be provided");
        }

        Log.Debug().WriteLine("Removing user from group {0}", groupName);

        var groupUri = jiraClient.JiraRestUri.AppendSegments("group", "user").ExtendQuery("groupname", groupName);
        
        if (!string.IsNullOrEmpty(accountId))
        {
            groupUri = groupUri.ExtendQuery("accountId", accountId);
        }
        else
        {
            groupUri = groupUri.ExtendQuery("username", username);
        }

        jiraClient.Behaviour.MakeCurrent();
        var response = await groupUri.DeleteAsync<HttpResponse<string, Error>>(cancellationToken).ConfigureAwait(false);
        response.HandleErrors();
    }

    /// <summary>
    ///     Get users from a group
    ///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/group-getUsersFromGroup
    /// </summary>
    /// <param name="jiraClient">IGroupDomain to bind the extension method to</param>
    /// <param name="groupName">Name of the group</param>
    /// <param name="includeInactiveUsers">Include inactive users in the result</param>
    /// <param name="startAt">Index of the first user to return (0-based)</param>
    /// <param name="maxResults">Maximum number of users to return, default is 50</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>GroupWithUsers containing the users</returns>
    public static async Task<JiraGroupWithUsers> GetUsersAsync(this IGroupDomain jiraClient, string groupName, 
        bool includeInactiveUsers = false, int startAt = 0, int maxResults = 50, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(groupName))
        {
            throw new ArgumentNullException(nameof(groupName));
        }

        Log.Debug().WriteLine("Getting users from group {0}", groupName);

        jiraClient.Behaviour.MakeCurrent();
        var groupUri = jiraClient.JiraRestUri.AppendSegments("group").ExtendQuery(new Dictionary<string, object>
        {
            { "groupname", groupName },
            { "includeInactiveUsers", includeInactiveUsers },
            { "startAt", startAt },
            { "maxResults", maxResults }
        });

        var response = await groupUri.GetAsAsync<HttpResponse<JiraGroupWithUsers, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }
}
