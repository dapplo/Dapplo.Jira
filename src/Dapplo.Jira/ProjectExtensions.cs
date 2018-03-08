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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Domains;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Internal;
using Dapplo.Log;

#endregion

namespace Dapplo.Jira
{
    /// <summary>
    ///     This holds all the project related extensions methods
    /// </summary>
    public static class ProjectExtensions
    {
        private static readonly LogSource Log = new LogSource();

        /// <summary>
        ///     Get projects information
        ///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2779
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
        ///     Get all visible projects
        ///     More information <a href="https://docs.atlassian.com/jira/REST/latest/#d2e2779">here</a>
        /// </summary>
        /// <param name="jiraClient">IProjectDomain to bind the extension method to</param>
        /// <param name="recent">
        ///     if this parameter is set then only projects recently accessed by the current user (if not logged
        ///     in then based on HTTP session) will be returned (maximum count limited to the specified number but no more than
        ///     20).
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
        ///     Get component information
        ///     More information <a href="https://docs.atlassian.com/jira/REST/cloud/#api/2/component-getComponent">here</a>
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
        ///     Create a component, 
        ///     More information <a href="https://docs.atlassian.com/jira/REST/cloud/#api/2/component-createComponent">here</a>
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
        ///     Update a component, if Lead is an empty string ("") the component lead will be removed.
        ///     More information <a href="https://docs.atlassian.com/jira/REST/cloud/#api/2/component-updateComponent">here</a>
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
        ///     Delete a component
        ///     More information <a href="https://docs.atlassian.com/jira/REST/cloud/#api/2/component-delete">here</a>
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
        public static Task<IEnumerable<User>> GetIssueCreatorsAsync(this IProjectDomain jiraClient, string projectKey, string userpattern = null, int? startAt = null, int? maxResults = null, CancellationToken cancellationToken = default)
        {
            return jiraClient.User.GetAssignableUsersAsync(userpattern, projectKey, startAt: startAt, maxResults: maxResults, cancellationToken: cancellationToken);
        }
    }
}