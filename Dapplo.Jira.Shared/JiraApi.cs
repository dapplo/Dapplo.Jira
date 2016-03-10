//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2016 Dapplo
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
//  You should have Config a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;

#endregion

namespace Dapplo.Jira
{
	/// <summary>
	///     Jira API, using Dapplo.HttpExtensions
	/// </summary>
	public class JiraApi
	{
		/// <summary>
		///     Store the specific HttpBehaviour, which contains a IHttpSettings and also some additional logic for making a
		///     HttpClient which works with Jira
		/// </summary>
		private readonly HttpBehaviour _behaviour;

		private string _password;

		private string _user;

		/// <summary>
		///     Create the JiraApi object, here the HttpClient is configured
		/// </summary>
		/// <param name="baseUri">Base URL, e.g. https://yourjiraserver</param>
		/// <param name="httpSettings">IHttpSettings or null for default</param>
		private JiraApi(Uri baseUri, IHttpSettings httpSettings)
		{
			if (baseUri == null)
			{
				throw new ArgumentNullException(nameof(baseUri));
			}
			JiraBaseUri = baseUri.AppendSegments("rest", "api", "2");

			_behaviour = new HttpBehaviour
			{
				HttpSettings = httpSettings,
				OnHttpRequestMessageCreated = (httpMessage) =>
				{
					httpMessage?.Headers.TryAddWithoutValidation("X-Atlassian-Token", "nocheck");
					if (!string.IsNullOrEmpty(_user) && _password != null)
					{
						httpMessage?.SetBasicAuthorization(_user, _password);
					}
					return httpMessage;
				}
			};
		}

		/// <summary>
		///     The base URI for your JIRA server
		/// </summary>
		public Uri JiraBaseUri { get; }

		/// <summary>
		///     The version of the JIRA server
		/// </summary>
		public string JiraVersion { get; private set; }

		/// <summary>
		///     The title of the JIRA server
		/// </summary>
		public string ServerTitle { get; private set; }

		/// <summary>
		///     Set Basic Authentication for the current client
		/// </summary>
		/// <param name="user">username</param>
		/// <param name="password">password</param>
		public void SetBasicAuthentication(string user, string password)
		{
			_user = user;
			_password = password;
		}

		#region write
		/// <summary>
		///     Attach content to the specified issue
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e3035
		/// </summary>
		/// <param name="issueKey"></param>
		/// <param name="content">the content can be anything what Dapplo.HttpExtensions supports</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Attachment</returns>
		public async Task<Attachment> AttachAsync(string issueKey, object content, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			var attachUri = JiraBaseUri.AppendSegments("issue", issueKey, "attachments");
			return await attachUri.PostAsync<Attachment>(content, cancellationToken).ConfigureAwait(false);
		}
		#endregion

		#region Read

		/// <summary>
		///     Retrieve the Avatar for the supplied avatarUrls object
		/// </summary>
		/// <typeparam name="TResponse">the type to return the result into. e.g. Bitmap,BitmapSource or MemoryStream</typeparam>
		/// <param name="avatarUrls">AvatarUrls object from User or Myself method, or a project from the projects</param>
		/// <param name="avatarSize">Use one of the AvatarSizes to specify the size you want to have</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Bitmap,BitmapSource or MemoryStream (etc) depending on TResponse</returns>
		public async Task<TResponse> AvatarAsync<TResponse>(AvatarUrls avatarUrls, AvatarSizes avatarSize = AvatarSizes.ExtraLarge, CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class
		{
			_behaviour.MakeCurrent();
			switch (avatarSize)
			{
				case AvatarSizes.Small:
					return await avatarUrls.Small.GetAsAsync<TResponse>(cancellationToken).ConfigureAwait(false);
				case AvatarSizes.Medium:
					return await avatarUrls.Medium.GetAsAsync<TResponse>(cancellationToken).ConfigureAwait(false);
				case AvatarSizes.Large:
					return await avatarUrls.Large.GetAsAsync<TResponse>(cancellationToken).ConfigureAwait(false);
				case AvatarSizes.ExtraLarge:
					return await avatarUrls.ExtraLarge.GetAsAsync<TResponse>(cancellationToken).ConfigureAwait(false);
			}
			throw new ArgumentException($"Unknown avatar size: {avatarSize}", nameof(avatarSize));
		}

		/// <summary>
		///     Get issue information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e4539
		/// </summary>
		/// <param name="issue">the issue key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Issue</returns>
		public async Task<Issue> IssueAsync(string issue, CancellationToken cancellationToken = default(CancellationToken))
		{
			var issueUri = JiraBaseUri.AppendSegments("issue", issue);
			_behaviour.MakeCurrent();
			return await issueUri.GetAsAsync<Issue>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Get currrent user information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e4253
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>User</returns>
		public async Task<User> MyselfAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var myselfUri = JiraBaseUri.AppendSegments("myself");
			_behaviour.MakeCurrent();
			return await myselfUri.GetAsAsync<User>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Get projects information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2779
		/// </summary>
		/// <param name="projectKey">key of the project</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>ProjectDetails</returns>
		public async Task<Project> ProjectAsync(string projectKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			var projectUri = JiraBaseUri.AppendSegments("project", projectKey);
			_behaviour.MakeCurrent();
			return await projectUri.GetAsAsync<Project>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Get all visible projects
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2779
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>list of ProjectDigest</returns>
		public async Task<IList<ProjectDigest>> ProjectsAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var projectUri = JiraBaseUri.AppendSegments("project");
			_behaviour.MakeCurrent();
			return await projectUri.GetAsAsync<IList<ProjectDigest>>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Search for issues, with a JQL (e.g. from a filter)
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2713
		/// </summary>
		/// <param name="jql">Jira Query Language, like SQL, for the search</param>
		/// <param name="maxResults">Maximum number of results returned, default is 20</param>
		/// <param name="fields">Jira fields to include, if null a default is taken</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>SearchResult</returns>
		public async Task<SearchResult> SearchAsync(string jql, int maxResults = 20, IList<string> fields = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			var search = new Search
			{
				Jql = jql,
				ValidateQuery = true,
				MaxResults = maxResults,
				Fields = fields ?? new List<string> { "summary", "status", "assignee", "key", "project"}
			};
			var searchUri = JiraBaseUri.AppendSegments("search");
			return await searchUri.PostAsync<SearchResult>(search, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Get server information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e3828
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>ServerInfo</returns>
		public async Task<ServerInfo> ServerInfoAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var serverInfoUri = JiraBaseUri.AppendSegments("serverInfo");
			_behaviour.MakeCurrent();
			return await serverInfoUri.GetAsAsync<ServerInfo>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Get user information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e5339
		/// </summary>
		/// <param name="username"></param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>user information</returns>
		public async Task<User> UserAsync(string username, CancellationToken cancellationToken = default(CancellationToken))
		{
			var userUri = JiraBaseUri.AppendSegments("user").ExtendQuery("username", username);
			_behaviour.MakeCurrent();
			return await userUri.GetAsAsync<User>(cancellationToken).ConfigureAwait(false);
		}

		#endregion

		#region Initializer

		/// <summary>
		///     Create the JiraApi, and initialize
		///     This overload is created to prevent needed a reference to Dapplo.HttpExtensions for the IHttpSettings
		/// </summary>
		/// <param name="baseUri">Base URL, e.g. https://yourjiraserver</param>
		/// <returns>JiraApi (in a Task)</returns>
		public static async Task<JiraApi> CreateAndInitializeAsync(Uri baseUri)
		{
			return await CreateAndInitializeAsync(baseUri, null);
		}

		/// <summary>
		///     Create the JiraApi, and initialize
		/// </summary>
		/// <param name="baseUri">Base URL, e.g. https://yourjiraserver</param>
		/// <param name="httpSettings">IHttpSettings or null for default</param>
		/// <returns>JiraApi (in a Task)</returns>
		public static async Task<JiraApi> CreateAndInitializeAsync(Uri baseUri, IHttpSettings httpSettings)
		{
			var jiraApi = new JiraApi(baseUri, httpSettings);
			await jiraApi.InitializeAsync();
			return jiraApi;
		}

		/// <summary>
		///     Get filter favorites
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1388
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>dynamic (JsonArray)</returns>
		public async Task<dynamic> FiltersAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			var filterFavouriteUri = JiraBaseUri.AppendSegments("filter", "favourite");
			return await filterFavouriteUri.GetAsAsync<dynamic>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     This will just create a connection and retrieve the server title / jira version
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Task</returns>
		public async Task InitializeAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var serverInfo = await ServerInfoAsync(cancellationToken);
			ServerTitle = serverInfo.ServerTitle;
			JiraVersion = serverInfo.Version;
		}

		#endregion
	}
}