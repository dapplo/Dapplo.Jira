// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira;

/// <summary>
///	 This holds all the project related extensions methods
/// </summary>
public static class ProjectDomainExtensions
{
#pragma warning disable IDE0090 // Use 'new(...)'
    private static readonly LogSource Log = new LogSource();
#pragma warning restore IDE0090 // Use 'new(...)'

    /// <summary>
    ///	 Get projects information
    ///	 See: https://docs.atlassian.com/jira/REST/latest/#d2e2779
    /// </summary>
    /// <param name="jiraClient">IProjectDomain to bind the extension method to</param>
    /// <param name="projectKey">key of the project</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>ProjectDetails</returns>
    public static async Task<Project> GetAsync(this IProjectDomain jiraClient, string projectKey, CancellationToken cancellationToken = default)
    {
        if (projectKey == null)
        {
            throw new ArgumentNullException(nameof(projectKey));
        }

        Log.Debug().WriteLine("Retrieving project {0}", projectKey);

        var projectUri = jiraClient.JiraRestUri.AppendSegments("project", projectKey);

        // Add the configurable expand values, if the value is not null or empty
        if (JiraConfig.ExpandGetProject?.Length > 0)
        {
            projectUri = projectUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetProject));
        }

        jiraClient.Behaviour.MakeCurrent();
        var response = await projectUri.GetAsAsync<HttpResponse<Project, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///	 Get all visible projects
    ///	 More information <a href="https://docs.atlassian.com/jira/REST/latest/#d2e2779">here</a>
    /// </summary>
    /// <param name="jiraClient">IProjectDomain to bind the extension method to</param>
    /// <param name="recent">
    ///	 if this parameter is set then only projects recently accessed by the current user (if not logged
    ///	 in then based on HTTP session) will be returned (maximum count limited to the specified number but no more than
    ///	 20).
    /// </param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>list of ProjectDigest</returns>
    public static async Task<IList<ProjectDigest>> GetAllAsync(this IProjectDomain jiraClient, int? recent = null,
        CancellationToken cancellationToken = default)
    {
        Log.Debug().WriteLine("Retrieving projects");

        var projectsUri = jiraClient.JiraRestUri.AppendSegments("project");
        if (recent.HasValue)
        {
            projectsUri = projectsUri.ExtendQuery("recent", recent);
        }

        // Add the configurable expand values, if the value is not null or empty
        if (JiraConfig.ExpandGetProjects?.Length > 0)
        {
            projectsUri = projectsUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetProjects));
        }

        jiraClient.Behaviour.MakeCurrent();
        var response = await projectsUri.GetAsAsync<HttpResponse<IList<ProjectDigest>, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///	 Get component information
    ///	 More information <a href="https://docs.atlassian.com/jira/REST/cloud/#api/2/component-getComponent">here</a>
    /// </summary>
    /// <param name="jiraClient">IProjectDomain to bind the extension method to</param>
    /// <param name="componentId">long with ID of the component to retrieve</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Component</returns>
    public static async Task<Component> GetComponentAsync(this IProjectDomain jiraClient, long componentId, CancellationToken cancellationToken = default)
    {
        Log.Debug().WriteLine("Retrieving component with id {0}", componentId);

        var componentUri = jiraClient.JiraRestUri.AppendSegments("component", componentId);

        jiraClient.Behaviour.MakeCurrent();
        var response = await componentUri.GetAsAsync<HttpResponse<Component, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///	 Create a component, 
    ///	 More information <a href="https://docs.atlassian.com/jira/REST/cloud/#api/2/component-createComponent">here</a>
    /// </summary>
    /// <param name="jiraClient">IProjectDomain to bind the extension method to</param>
    /// <param name="component">Component to create</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Component with the details, like id</returns>
    public static async Task<Component> CreateComponentAsync(this IProjectDomain jiraClient, Component component, CancellationToken cancellationToken = default)
    {
        Log.Debug().WriteLine("Creating component {0}", component.Name);

        var componentUri = jiraClient.JiraRestUri.AppendSegments("component");

        jiraClient.Behaviour.MakeCurrent();
        var response = await componentUri.PostAsync<HttpResponse<Component, Error>>(component, cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///	 Update a component, if Lead is an empty string ("") the component lead will be removed.
    ///	 More information <a href="https://docs.atlassian.com/jira/REST/cloud/#api/2/component-updateComponent">here</a>
    /// </summary>
    /// <param name="jiraClient">IProjectDomain to bind the extension method to</param>
    /// <param name="component">Component to update</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Component</returns>
    public static async Task<Component> UpdateComponentAsync(this IProjectDomain jiraClient, Component component, CancellationToken cancellationToken = default)
    {
        Log.Debug().WriteLine("Updating component {0}", component.Name);

        var componentUri = jiraClient.JiraRestUri.AppendSegments("component", component.Id);

        jiraClient.Behaviour.MakeCurrent();
        var response = await componentUri.PutAsync<HttpResponse<Component, Error>>(component, cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///	 Delete a component
    ///	 More information <a href="https://docs.atlassian.com/jira/REST/cloud/#api/2/component-delete">here</a>
    /// </summary>
    /// <param name="jiraClient">IProjectDomain to bind the extension method to</param>
    /// <param name="componentId">long with id of the component to delete</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public static async Task DeleteComponentAsync(this IProjectDomain jiraClient, long componentId, CancellationToken cancellationToken = default)
    {
        Log.Debug().WriteLine("Deleting component {0}", componentId);

        var componentUri = jiraClient.JiraRestUri.AppendSegments("component", componentId);

        jiraClient.Behaviour.MakeCurrent();
        var response = await componentUri.DeleteAsync<HttpResponse>(cancellationToken).ConfigureAwait(false);
        response.HandleStatusCode(HttpStatusCode.NoContent);
    }

    /// <summary>
    /// Retrieve the users who can create issues for the specified project
    /// </summary>
    /// <param name="jiraClient">IProjectDomain</param>
    /// <param name="projectKey">string with the key of the project</param>
    /// <param name="userpattern">optional string with a pattern to match the user to</param>
    /// <param name="startAt">optional int with the start, used for paging</param>
    /// <param name="maxResults">optional int with the maximum number of results, default is 50</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>IEnumerable with User</returns>
    public static Task<IEnumerable<User>> GetIssueCreatorsAsync(this IProjectDomain jiraClient, string projectKey, string userpattern = null, int? startAt = null,
        int? maxResults = null, CancellationToken cancellationToken = default)
    {
        return jiraClient.User.GetAssignableUsersAsync(userpattern, projectKey, startAt: startAt, maxResults: maxResults, cancellationToken: cancellationToken);
    }

    /// <summary>
    ///	 Get security level information
    ///	 More information <a href="https://docs.atlassian.com/software/jira/docs/api/REST/8.5.3/#api/2/project/{projectKeyOrId}/securitylevel">here</a>
    /// </summary>
    /// <param name="jiraClient">IProjectDomain to bind the extension method to</param>
    /// <param name="projectKey">project key</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>IEnumerable with SecurityLinks</returns>
    public static async Task<IEnumerable<SecurityLevel>> GetSecurityLevelsAsync(this IProjectDomain jiraClient, string projectKey,
        CancellationToken cancellationToken = default)
    {
        Log.Debug().WriteLine("Retrieving SecurityLevels for {0}", projectKey);

        var securityLevelUri = jiraClient.JiraRestUri.AppendSegments("project", projectKey, "securitylevel");

        jiraClient.Behaviour.MakeCurrent();
        var response = await securityLevelUri.GetAsAsync<HttpResponse<SecurityLevels, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors().Levels;
    }

    /// <summary>
    ///	 Get all versions (releases) for a project
    ///	 More information <a href="https://docs.atlassian.com/jira/REST/cloud/#api/2/project/{projectKeyOrId}/versions">here</a>
    /// </summary>
    /// <param name="jiraClient">IProjectDomain to bind the extension method to</param>
    /// <param name="projectKey">project key or ID</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>IList with Version information</returns>
    public static async Task<IList<Entities.Version>> GetVersionsAsync(this IProjectDomain jiraClient, string projectKey,
        CancellationToken cancellationToken = default)
    {
        if (projectKey == null)
        {
            throw new ArgumentNullException(nameof(projectKey));
        }

        Log.Debug().WriteLine("Retrieving versions for project {0}", projectKey);

        var versionsUri = jiraClient.JiraRestUri.AppendSegments("project", projectKey, "versions");

        jiraClient.Behaviour.MakeCurrent();
        var response = await versionsUri.GetAsAsync<HttpResponse<IList<Entities.Version>, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///	 Get a specific version (release) by ID
    ///	 More information <a href="https://docs.atlassian.com/jira/REST/cloud/#api/2/version/{id}">here</a>
    /// </summary>
    /// <param name="jiraClient">IProjectDomain to bind the extension method to</param>
    /// <param name="versionId">version ID</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Version information</returns>
    public static async Task<Entities.Version> GetVersionAsync(this IProjectDomain jiraClient, string versionId,
        CancellationToken cancellationToken = default)
    {
        if (versionId == null)
        {
            throw new ArgumentNullException(nameof(versionId));
        }

        Log.Debug().WriteLine("Retrieving version with ID {0}", versionId);

        var versionUri = jiraClient.JiraRestUri.AppendSegments("version", versionId);

        jiraClient.Behaviour.MakeCurrent();
        var response = await versionUri.GetAsAsync<HttpResponse<Entities.Version, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///	 Get all roles in a project
    ///	 More information <a href="https://docs.atlassian.com/jira/REST/latest/#api/2/project/{projectIdOrKey}/role">here</a>
    /// </summary>
    /// <param name="jiraClient">IProjectDomain to bind the extension method to</param>
    /// <param name="projectKey">project key or ID</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>IDictionary with role name and role URI</returns>
    public static async Task<IDictionary<string, Uri>> GetRolesAsync(this IProjectDomain jiraClient, string projectKey,
        CancellationToken cancellationToken = default)
    {
        if (projectKey == null)
        {
            throw new ArgumentNullException(nameof(projectKey));
        }

        Log.Debug().WriteLine("Retrieving roles for project {0}", projectKey);

        var rolesUri = jiraClient.JiraRestUri.AppendSegments("project", projectKey, "role");

        jiraClient.Behaviour.MakeCurrent();
        var response = await rolesUri.GetAsAsync<HttpResponse<IDictionary<string, Uri>, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///	 Get role details and actors for a specific role in a project
    ///	 More information <a href="https://docs.atlassian.com/jira/REST/latest/#api/2/project/{projectIdOrKey}/role/{id}">here</a>
    /// </summary>
    /// <param name="jiraClient">IProjectDomain to bind the extension method to</param>
    /// <param name="projectKey">project key or ID</param>
    /// <param name="roleId">role ID</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>ProjectRole with details and actors</returns>
    public static async Task<ProjectRole> GetRoleAsync(this IProjectDomain jiraClient, string projectKey, long roleId,
        CancellationToken cancellationToken = default)
    {
        if (projectKey == null)
        {
            throw new ArgumentNullException(nameof(projectKey));
        }

        Log.Debug().WriteLine("Retrieving role {0} for project {1}", roleId, projectKey);

        var roleUri = jiraClient.JiraRestUri.AppendSegments("project", projectKey, "role", roleId);

        jiraClient.Behaviour.MakeCurrent();
        var response = await roleUri.GetAsAsync<HttpResponse<ProjectRole, Error>>(cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///	 Add an actor (user or group) to a project role
    ///	 More information <a href="https://docs.atlassian.com/jira/REST/latest/#api/2/project/{projectIdOrKey}/role/{id}">here</a>
    /// </summary>
    /// <param name="jiraClient">IProjectDomain to bind the extension method to</param>
    /// <param name="projectKey">project key or ID</param>
    /// <param name="roleId">role ID</param>
    /// <param name="user">user name or account ID to add (mutually exclusive with group)</param>
    /// <param name="group">group name to add (mutually exclusive with user)</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>ProjectRole with updated actors</returns>
    public static async Task<ProjectRole> AddActorToRoleAsync(this IProjectDomain jiraClient, string projectKey, long roleId,
        string user = null, string group = null, CancellationToken cancellationToken = default)
    {
        if (projectKey == null)
        {
            throw new ArgumentNullException(nameof(projectKey));
        }

        if (user == null && group == null)
        {
            throw new ArgumentException("Either user or group must be specified");
        }

        if (user != null && group != null)
        {
            throw new ArgumentException("Cannot specify both user and group");
        }

        Log.Debug().WriteLine("Adding actor to role {0} in project {1}", roleId, projectKey);

        var roleUri = jiraClient.JiraRestUri.AppendSegments("project", projectKey, "role", roleId);

        var actorData = new Dictionary<string, object>();
        if (user != null)
        {
            actorData["user"] = new[] { user };
        }
        else
        {
            actorData["group"] = new[] { group };
        }

        jiraClient.Behaviour.MakeCurrent();
        var response = await roleUri.PostAsync<HttpResponse<ProjectRole, Error>>(actorData, cancellationToken).ConfigureAwait(false);
        return response.HandleErrors();
    }

    /// <summary>
    ///	 Remove an actor (user or group) from a project role
    ///	 More information <a href="https://docs.atlassian.com/jira/REST/latest/#api/2/project/{projectIdOrKey}/role/{id}">here</a>
    /// </summary>
    /// <param name="jiraClient">IProjectDomain to bind the extension method to</param>
    /// <param name="projectKey">project key or ID</param>
    /// <param name="roleId">role ID</param>
    /// <param name="user">user name or account ID to remove (mutually exclusive with group)</param>
    /// <param name="group">group name to remove (mutually exclusive with user)</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public static async Task RemoveActorFromRoleAsync(this IProjectDomain jiraClient, string projectKey, long roleId,
        string user = null, string group = null, CancellationToken cancellationToken = default)
    {
        if (projectKey == null)
        {
            throw new ArgumentNullException(nameof(projectKey));
        }

        if (user == null && group == null)
        {
            throw new ArgumentException("Either user or group must be specified");
        }

        if (user != null && group != null)
        {
            throw new ArgumentException("Cannot specify both user and group");
        }

        Log.Debug().WriteLine("Removing actor from role {0} in project {1}", roleId, projectKey);

        var roleUri = jiraClient.JiraRestUri.AppendSegments("project", projectKey, "role", roleId);

        if (user != null)
        {
            roleUri = roleUri.ExtendQuery("user", user);
        }
        else
        {
            roleUri = roleUri.ExtendQuery("group", group);
        }

        jiraClient.Behaviour.MakeCurrent();
        var response = await roleUri.DeleteAsync<HttpResponse>(cancellationToken).ConfigureAwait(false);
        response.HandleStatusCode(HttpStatusCode.NoContent);
    }
}
