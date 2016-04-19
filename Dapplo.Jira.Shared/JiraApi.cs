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
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;
using System.Net;

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
		public JiraApi(Uri baseUri, IHttpSettings httpSettings = null)
		{
			if (baseUri == null)
			{
				throw new ArgumentNullException(nameof(baseUri));
			}
			JiraBaseUri = baseUri.AppendSegments("rest", "api", "2");

			_behaviour = new HttpBehaviour
			{
				HttpSettings = httpSettings ?? HttpExtensionsGlobals.HttpSettings,
				OnHttpRequestMessageCreated = httpMessage =>
				{
					httpMessage?.Headers.TryAddWithoutValidation("X-Atlassian-Token", "no-check");
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

		#region write

		/// <summary>
		///     Attach content to the specified issue
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e3035
		/// </summary>
		/// <param name="issueKey"></param>
		/// <param name="content">the content can be anything what Dapplo.HttpExtensions supports</param>
		/// <param name="filename">Filename for the attachment</param>
		/// <param name="contentType">content-type for the attachment</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Attachment</returns>
		public async Task<IList<Attachment>> AttachAsync<TContent>(string issueKey, TContent content, string filename, string contentType = null, CancellationToken cancellationToken = default(CancellationToken))
			where TContent : class
		{
			var attachment = new AttachmentContainer<TContent>
			{
				Content = content,
				ContentType = contentType,
				FileName = filename
			};
			_behaviour.MakeCurrent();
			var attachUri = JiraBaseUri.AppendSegments("issue", issueKey, "attachments");
			var response = await attachUri.PostAsync<HttpResponse<IList<Attachment>, string>>(attachment, cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse);
			}
			return response.Response;
		}

		#endregion

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

		#region Read

		/// <summary>
		///     Retrieve the Avatar for the supplied avatarUrls object
		/// </summary>
		/// <typeparam name="TResponse">the type to return the result into. e.g. Bitmap,BitmapSource or MemoryStream</typeparam>
		/// <param name="avatarUrls">AvatarUrls object from User or Myself method, or a project from the projects</param>
		/// <param name="avatarSize">Use one of the AvatarSizes to specify the size you want to have</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Bitmap,BitmapSource or MemoryStream (etc) depending on TResponse</returns>
		public async Task<TResponse> GetAvatarAsync<TResponse>(AvatarUrls avatarUrls, AvatarSizes avatarSize = AvatarSizes.ExtraLarge, CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class
		{
			_behaviour.MakeCurrent();
			Uri avatarUri;

			switch (avatarSize)
			{
				case AvatarSizes.Small:
					avatarUri = avatarUrls.Small;
					break;
				case AvatarSizes.Medium:
					avatarUri = avatarUrls.Medium;
					break;
				case AvatarSizes.Large:
					avatarUri = avatarUrls.Large;
					break;
				case AvatarSizes.ExtraLarge:
					avatarUri = avatarUrls.ExtraLarge;
					break;
				default:
					throw new ArgumentException($"Unknown avatar size: {avatarSize}", nameof(avatarSize));
			}
			var response = await avatarUri.GetAsAsync<HttpResponse<TResponse, string>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse);
			}
			return response.Response;
		}

		/// <summary>
		///     Get issue information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e4539
		/// </summary>
		/// <param name="issue">the issue key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Issue</returns>
		public async Task<Issue> GetIssueAsync(string issue, CancellationToken cancellationToken = default(CancellationToken))
		{
			var issueUri = JiraBaseUri.AppendSegments("issue", issue);
			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetIssue?.Length > 0)
			{
				issueUri = issueUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetIssue));
			}
			_behaviour.MakeCurrent();
			var response = await issueUri.GetAsAsync<HttpResponse<Issue, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(string.Join(", ", response.ErrorResponse.ErrorMessages));
			}
			return response.Response;
		}

		/// <summary>
		///     Get currrent user information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e4253
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>User</returns>
		public async Task<User> WhoAmIAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var myselfUri = JiraBaseUri.AppendSegments("myself");
			_behaviour.MakeCurrent();
			var response = await myselfUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(string.Join(", ", response.ErrorResponse.ErrorMessages));
			}
			return response.Response;
		}

		/// <summary>
		///     Get projects information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2779
		/// </summary>
		/// <param name="projectKey">key of the project</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>ProjectDetails</returns>
		public async Task<Project> GetProjectAsync(string projectKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			var projectUri = JiraBaseUri.AppendSegments("project", projectKey);

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetProject?.Length > 0)
			{
				projectUri = projectUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetProject));
			}

			_behaviour.MakeCurrent();
			var response = await projectUri.GetAsAsync<HttpResponse<Project, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(string.Join(", ", response.ErrorResponse.ErrorMessages));
			}
			return response.Response;
		}

		/// <summary>
		///     Get all visible projects
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2779
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>list of ProjectDigest</returns>
		public async Task<IList<ProjectDigest>> GetProjectsAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var projectsUri = JiraBaseUri.AppendSegments("project");

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetProjects?.Length > 0)
			{
				projectsUri = projectsUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetProjects));
			}

			_behaviour.MakeCurrent();
			var response = await projectsUri.GetAsAsync<HttpResponse<IList<ProjectDigest>, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(string.Join(", ", response.ErrorResponse.ErrorMessages));
			}
			return response.Response;
		}

		/// <summary>
		///     Search for issues, with a JQL (e.g. from a filter)
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2713
		/// </summary>
		/// <param name="jql">Jira Query Language, like SQL, for the search</param>
		/// <param name="maxResults">Maximum number of results returned, default is 20</param>
		/// <param name="fields">Jira fields to include, if null the defaults from the JiraConfig.SearchFields are taken</param>
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
				Fields = fields ?? new List<string>(JiraConfig.SearchFields)
			};
			var searchUri = JiraBaseUri.AppendSegments("search");

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandSearch?.Length > 0)
			{
				searchUri = searchUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandSearch));
			}

			var response = await searchUri.PostAsync<HttpResponse<SearchResult, Error>>(search, cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(string.Join(", ", response.ErrorResponse.ErrorMessages));
			}
			return response.Response;
		}

		/// <summary>
		/// Returns a list of users that match the search string.
		/// This resource cannot be accessed anonymously.
		/// See: https://docs.atlassian.com/jira/REST/latest/#api/2/user-findUsers
		/// </summary>
		/// <param name="query">A query string used to search username, name or e-mail address</param>
		/// <param name="startAt"></param>
		/// <param name="maxResults">Maximum number of results returned, default is 20</param>
		/// <param name="includeActive">If true, then active users are included in the results (default true)</param>
		/// <param name="includeInactive">If true, then inactive users are included in the results (default false)</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>SearchResult</returns>
		public async Task<IList<User>> SearchUserAsync(string query, bool includeActive = true, bool includeInactive= false, int startAt = 0, int maxResults = 20, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			var searchUri = JiraBaseUri.AppendSegments("user","search").ExtendQuery(new Dictionary<string, object>
			{
				{
					"username", query
				},
				{
					"includeActive", $"{includeActive}"
				},
				{
					"includeInactive", $"{includeInactive}"
				},
				{
					"startAt", startAt
				},
				{
					"maxResults", maxResults
				}
			});

			var response = await searchUri.GetAsAsync<HttpResponse<IList<User>, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(string.Join(", ", response.ErrorResponse.ErrorMessages));
			}
			return response.Response;
		}

		/// <summary>
		///     Get server information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e3828
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>ServerInfo</returns>
		public async Task<ServerInfo> GetServerInfoAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var serverInfoUri = JiraBaseUri.AppendSegments("serverInfo");
			_behaviour.MakeCurrent();

			var response = await serverInfoUri.GetAsAsync<HttpResponse<ServerInfo, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(string.Join(", ", response.ErrorResponse.ErrorMessages));
			}
			return response.Response;
		}

		/// <summary>
		///     Get user information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e5339
		/// </summary>
		/// <param name="username"></param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>User</returns>
		public async Task<User> GetUserAsync(string username, CancellationToken cancellationToken = default(CancellationToken))
		{
			var userUri = JiraBaseUri.AppendSegments("user").ExtendQuery("username", username);
			_behaviour.MakeCurrent();

			var response = await userUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(string.Join(", ", response.ErrorResponse.ErrorMessages));
			}
			return response.Response;
		}

		/// <summary>
		///     Get filter favorites
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1388
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>List of filter</returns>
		public async Task<IList<Filter>> GetFavoriteFiltersAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			var filterFavouriteUri = JiraBaseUri.AppendSegments("filter", "favourite");

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetFavoriteFilters?.Length > 0)
			{
				filterFavouriteUri = filterFavouriteUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetFavoriteFilters));
			}

			var response = await filterFavouriteUri.GetAsAsync<HttpResponse<IList<Filter>, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(string.Join(", ", response.ErrorResponse.ErrorMessages));
			}
			return response.Response;
		}

		/// <summary>
		///     Get filter
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1388
		/// </summary>
		/// <param name="id">filter id</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Filter</returns>
		public async Task<Filter> GetFilterAsync(long id, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			var filterUri = JiraBaseUri.AppendSegments("filter", id);

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetFilter?.Length > 0)
			{
				filterUri = filterUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetFilter));
			}

			var response = await filterUri.GetAsAsync<HttpResponse<Filter, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(string.Join(", ", response.ErrorResponse.ErrorMessages));
			}
			return response.Response;
		}

		/// <summary>
		///     Delete filter
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1388
		/// </summary>
		/// <param name="id">filter id</param>
		/// <param name="cancellationToken">CancellationToken</param>
		public async Task DeleteFilterAsync(long id, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			var filterUri = JiraBaseUri.AppendSegments("filter", id);

			var response = await filterUri.DeleteAsync<HttpResponse<string, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.StatusCode != HttpStatusCode.NoContent)
			{
				throw new Exception(string.Join(", ", response.ErrorResponse.ErrorMessages));
			}
		}

		#endregion
	}
}