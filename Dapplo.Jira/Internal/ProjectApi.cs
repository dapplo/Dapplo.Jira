//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Jira
// 
//  Dapplo.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;
using Dapplo.Log;

#endregion

namespace Dapplo.Jira.Internal
{
	/// <summary>
	///     This holds all the project related methods
	/// </summary>
	internal class ProjectApi : IProjectApi
	{
		private static readonly LogSource Log = new LogSource();
		private readonly JiraApi _jiraApi;

		internal ProjectApi(JiraApi jiraApi)
		{
			_jiraApi = jiraApi;
		}

		/// <inheritdoc />
		public async Task<Project> GetAsync(string projectKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (projectKey == null)
			{
				throw new ArgumentNullException(nameof(projectKey));
			}

			Log.Debug().WriteLine("Retrieving project {0}", projectKey);

			var projectUri = _jiraApi.JiraRestUri.AppendSegments("project", projectKey);

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetProject?.Length > 0)
			{
				projectUri = projectUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetProject));
			}

			_jiraApi.Behaviour.MakeCurrent();
			var response = await projectUri.GetAsAsync<HttpResponse<Project, Error>>(cancellationToken).ConfigureAwait(false);
			return _jiraApi.HandleErrors(response);
		}

		/// <inheritdoc />
		public async Task<IList<ProjectDigest>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			Log.Debug().WriteLine("Retrieving projects");

			var projectsUri = _jiraApi.JiraRestUri.AppendSegments("project");

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetProjects?.Length > 0)
			{
				projectsUri = projectsUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetProjects));
			}

			_jiraApi.Behaviour.MakeCurrent();
			var response = await projectsUri.GetAsAsync<HttpResponse<IList<ProjectDigest>, Error>>(cancellationToken).ConfigureAwait(false);
			return _jiraApi.HandleErrors(response);
		}
	}
}