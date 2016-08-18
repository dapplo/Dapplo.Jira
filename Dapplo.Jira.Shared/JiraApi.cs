#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016 Dapplo
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
// along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.be 

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
		public JiraApi(Uri baseUri, IHttpSettings httpSettings = null)
		{
			if (baseUri == null)
			{
				throw new ArgumentNullException(nameof(baseUri));
			}
			JiraBaseUri = baseUri;
			JiraRestUri = baseUri.AppendSegments("rest", "api", "2");
			JiraAuthUri = baseUri.AppendSegments("rest", "auth", "1");

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

		/// <summary>
		///     The rest URI for your JIRA server
		/// </summary>
		public Uri JiraRestUri { get; }

		/// <summary>
		///     The base URI for JIRA auth api
		/// </summary>
		public Uri JiraAuthUri { get; }

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

		/// <summary>
		/// Returns the content, specified by the Urim from the JIRA server.
		/// This is used internally, but can also be used to get e.g. the icon for an issue type.
		/// </summary>
		/// <typeparam name="TResponse"></typeparam>
		/// <param name="contentUri">Uri</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>TResponse</returns>
		public async Task<TResponse> GetUriContentAsync<TResponse>(Uri contentUri, CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class
		{
			_behaviour.MakeCurrent();

			var response = await contentUri.GetAsAsync<HttpResponse<TResponse, string>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception($"Status: {response.StatusCode} Message: {response.ErrorResponse}");
			}
			return response.Response;
		}

		/// <summary>
		///     Retrieve the Avatar for the supplied avatarUrls object
		/// </summary>
		/// <typeparam name="TResponse">the type to return the result into. e.g. Bitmap,BitmapSource or MemoryStream</typeparam>
		/// <param name="avatarUrls">AvatarUrls object from User or Myself method, or a project from the projects</param>
		/// <param name="avatarSize">Use one of the AvatarSizes to specify the size you want to have</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Bitmap,BitmapSource or MemoryStream (etc) depending on TResponse</returns>
		public async Task<TResponse> GetAvatarAsync<TResponse>(AvatarUrls avatarUrls, AvatarSizes avatarSize = AvatarSizes.ExtraLarge,
			CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class
		{
			_behaviour.MakeCurrent();
			Uri avatarUri = avatarUrls.GetUri(avatarSize);

			return await GetUriContentAsync<TResponse>(avatarUri, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Get server information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e3828
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>ServerInfo</returns>
		public async Task<ServerInfo> GetServerInfoAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var serverInfoUri = JiraRestUri.AppendSegments("serverInfo");
			_behaviour.MakeCurrent();

			var response = await serverInfoUri.GetAsAsync<HttpResponse<ServerInfo, Error>>(cancellationToken).ConfigureAwait(false);
			return HandleErrors(response);
		}

		/// <summary>
		///     Helper method for handling errors in the response, if the response has an error an exception is thrown.
		///     Else the real response is returned.
		/// </summary>
		/// <typeparam name="TResponse">Type for the ok content</typeparam>
		/// <typeparam name="TError">Type for the error content</typeparam>
		/// <param name="response">TResponse</param>
		/// <returns></returns>
		private static TResponse HandleErrors<TResponse, TError>(HttpResponse<TResponse, TError> response) where TResponse : class where TError : Error
		{
			if (response.HasError)
			{
				var message = response.StatusCode.ToString();
				if (response.ErrorResponse.ErrorMessages != null)
				{
					message = string.Join(", ", response.ErrorResponse.ErrorMessages);
				}
				else if (response.ErrorResponse?.Message != null)
				{
					message = response.ErrorResponse?.Message;
				}
				throw new Exception($"Status: {response.StatusCode} Message: {message}");
			}

			return response.Response;
		}

		#region issue

		/// <summary>
		///     Add comment to the specified issue
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1139
		/// </summary>
		/// <param name="issueKey">key for the issue</param>
		/// <param name="body">the body of the comment</param>
		/// <param name="visibility">optional visibility role</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Attachment</returns>
		public async Task AddCommentAsync(string issueKey, string body, string visibility = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			var comment = new Comment
			{
				Body = body,
				Visibility = visibility == null ? null : new Visibility
				{
					Type = "role",
					Value = visibility
				}
			};
			_behaviour.MakeCurrent();
			var attachUri = JiraRestUri.AppendSegments("issue", issueKey, "comment");
			await attachUri.PostAsync(comment, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Get issue information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e4539
		/// </summary>
		/// <param name="issueKey">the issue key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Issue</returns>
		public async Task<Issue> GetIssueAsync(string issueKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			var issueUri = JiraRestUri.AppendSegments("issue", issueKey);
			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetIssue?.Length > 0)
			{
				issueUri = issueUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetIssue));
			}
			_behaviour.MakeCurrent();
			var response = await issueUri.GetAsAsync<HttpResponse<Issue, Error>>(cancellationToken).ConfigureAwait(false);
			HandleErrors(response);
			return response.Response;
		}

		/// <summary>
		///     Get possible transitions for the specified issue
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1289
		/// </summary>
		/// <param name="issueKey">the issue key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Issue</returns>
		public async Task<IList<Transition>> GetPossibleTransitionsAsync(string issueKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			var transitionsUri = JiraRestUri.AppendSegments("issue", issueKey, "transitions");
			if (JiraConfig.ExpandGetTransitions?.Length > 0)
			{
				transitionsUri = transitionsUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetTransitions));
			}
			_behaviour.MakeCurrent();
			var response = await transitionsUri.GetAsAsync<HttpResponse<Transitions, Error>>(cancellationToken).ConfigureAwait(false);
			HandleErrors(response);
			return response.Response.Items;
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
		public async Task<SearchResult> SearchAsync(string jql, int maxResults = 20, IEnumerable<string> fields = null,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			if (jql == null)
			{
				throw new ArgumentNullException(nameof(jql));
			}
			_behaviour.MakeCurrent();
			var search = new Search
			{
				Jql = jql,
				ValidateQuery = true,
				MaxResults = maxResults,
				Fields = fields ?? new List<string>(JiraConfig.SearchFields)
			};
			var searchUri = JiraRestUri.AppendSegments("search");

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandSearch?.Length > 0)
			{
				searchUri = searchUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandSearch));
			}

			var response = await searchUri.PostAsync<HttpResponse<SearchResult, Error>>(search, cancellationToken).ConfigureAwait(false);
			HandleErrors(response);
			return response.Response;
		}

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
		public async Task<Attachment> AttachAsync<TContent>(string issueKey, TContent content, string filename, string contentType = null,
			CancellationToken cancellationToken = default(CancellationToken))
			where TContent : class
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}
			if (content == null)
			{
				throw new ArgumentNullException(nameof(content));
			}
			var attachment = new AttachmentContainer<TContent>
			{
				Content = content,
				ContentType = contentType,
				FileName = filename
			};
			_behaviour.MakeCurrent();
			var attachUri = JiraRestUri.AppendSegments("issue", issueKey, "attachments");
			var response = await attachUri.PostAsync<HttpResponse<IList<Attachment>, Error>>(attachment, cancellationToken).ConfigureAwait(false);
			HandleErrors(response);
			// Return the attachment, should be only one!
			return response.Response[0];
		}

		/// <summary>
		///     Delete the specified attachment
		/// </summary>
		/// <param name="attachment">The Attachment to delete</param>
		/// <param name="cancellationToken">CancellationToken</param>
		public Task DeleteAttachmentAsync(Attachment attachment, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (attachment == null)
			{
				throw new ArgumentNullException(nameof(attachment));
			}

			return DeleteAttachmentAsync(attachment.Id, cancellationToken);
		}

		/// <summary>
		///     Delete the specified attachment
		/// </summary>
		/// <param name="attachmentId">Id from the attachment</param>
		/// <param name="cancellationToken">CancellationToken</param>
		public async Task DeleteAttachmentAsync(long attachmentId, CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			var deleteAttachmentUri = JiraRestUri.AppendSegments("attachment", attachmentId);
			await deleteAttachmentUri.DeleteAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Get the content for the specified attachment
		/// </summary>
		/// <typeparam name="TResponse">the type which is returned, can be decided by the client and should be supported by Dapplo.HttpExtensions or your own IHttpContentConverter</typeparam>
		/// <param name="attachment">the attachment</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>TResponse</returns>
		public async Task<TResponse> GetAttachmentContentAsAsync<TResponse>(Attachment attachment, CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class
		{
			if (attachment == null)
			{
				throw new ArgumentNullException(nameof(attachment));
			}
			return await GetUriContentAsync<TResponse>(attachment.ContentUri, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Get the thumbnail for the specified attachment
		/// </summary>
		/// <typeparam name="TResponse">the type which is returned, can be decided by the client and should be supported by Dapplo.HttpExtensions or your own IHttpContentConverter</typeparam>
		/// <param name="attachment">the attachment</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>TResponse</returns>
		public async Task<TResponse> GetAttachmentThumbnailAsAsync<TResponse>(Attachment attachment, CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class
		{
			if (attachment == null)
			{
				throw new ArgumentNullException(nameof(attachment));
			}
			if (attachment.ThumbnailUri == null)
			{
				return null;
			}

			return await GetUriContentAsync<TResponse>(attachment.ThumbnailUri, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Update comment
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1139
		/// </summary>
		/// <param name="issueKey">jira key to which the comment belongs</param>
		/// <param name="comment">Comment to update</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Attachment</returns>
		public async Task UpdateCommentAsync(string issueKey, Comment comment, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (issueKey == null)
			{
				throw new ArgumentNullException(nameof(issueKey));
			}

			_behaviour.MakeCurrent();
			var attachUri = JiraRestUri.AppendSegments("issue", issueKey, "comment", comment.Id);
			await attachUri.PutAsync(comment, cancellationToken).ConfigureAwait(false);
		}

		#endregion

		#region filter

		/// <summary>
		///     Get filter favorites
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1388
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>List of filter</returns>
		public async Task<IList<Filter>> GetFavoriteFiltersAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			_behaviour.MakeCurrent();
			var filterFavouriteUri = JiraRestUri.AppendSegments("filter", "favourite");

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetFavoriteFilters?.Length > 0)
			{
				filterFavouriteUri = filterFavouriteUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetFavoriteFilters));
			}

			var response = await filterFavouriteUri.GetAsAsync<HttpResponse<IList<Filter>, Error>>(cancellationToken).ConfigureAwait(false);
			return HandleErrors(response);
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
			var filterUri = JiraRestUri.AppendSegments("filter", id);

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetFilter?.Length > 0)
			{
				filterUri = filterUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetFilter));
			}

			var response = await filterUri.GetAsAsync<HttpResponse<Filter, Error>>(cancellationToken).ConfigureAwait(false);
			return HandleErrors(response);
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
			var filterUri = JiraRestUri.AppendSegments("filter", id);

			var response = await filterUri.DeleteAsync<HttpResponse<string, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.StatusCode != HttpStatusCode.NoContent)
			{
				throw new Exception(string.Join(", ", response.ErrorResponse.ErrorMessages));
			}
		}

		#endregion

		#region project

		/// <summary>
		///     Get projects information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2779
		/// </summary>
		/// <param name="projectKey">key of the project</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>ProjectDetails</returns>
		public async Task<Project> GetProjectAsync(string projectKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (projectKey == null)
			{
				throw new ArgumentNullException(nameof(projectKey));
			}
			var projectUri = JiraRestUri.AppendSegments("project", projectKey);

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetProject?.Length > 0)
			{
				projectUri = projectUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetProject));
			}

			_behaviour.MakeCurrent();
			var response = await projectUri.GetAsAsync<HttpResponse<Project, Error>>(cancellationToken).ConfigureAwait(false);
			return HandleErrors(response);
		}

		/// <summary>
		///     Get all visible projects
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2779
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>list of ProjectDigest</returns>
		public async Task<IList<ProjectDigest>> GetProjectsAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var projectsUri = JiraRestUri.AppendSegments("project");

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetProjects?.Length > 0)
			{
				projectsUri = projectsUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetProjects));
			}

			_behaviour.MakeCurrent();
			var response = await projectsUri.GetAsAsync<HttpResponse<IList<ProjectDigest>, Error>>(cancellationToken).ConfigureAwait(false);
			return HandleErrors(response);
		}

		#endregion

		#region user

		/// <summary>
		///     Get user information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e5339
		/// </summary>
		/// <param name="username"></param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>User</returns>
		public async Task<User> GetUserAsync(string username, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (username == null)
			{
				throw new ArgumentNullException(nameof(username));
			}
			var userUri = JiraRestUri.AppendSegments("user").ExtendQuery("username", username);
			_behaviour.MakeCurrent();

			var response = await userUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
			return HandleErrors(response);
		}

		/// <summary>
		///     Returns a list of users that match the search string.
		///     This resource cannot be accessed anonymously.
		///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/user-findUsers
		/// </summary>
		/// <param name="query">A query string used to search username, name or e-mail address</param>
		/// <param name="startAt"></param>
		/// <param name="maxResults">Maximum number of results returned, default is 20</param>
		/// <param name="includeActive">If true, then active users are included in the results (default true)</param>
		/// <param name="includeInactive">If true, then inactive users are included in the results (default false)</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>SearchResult</returns>
		public async Task<IList<User>> SearchUserAsync(string query, bool includeActive = true, bool includeInactive = false, int startAt = 0,
			int maxResults = 20, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (query == null)
			{
				throw new ArgumentNullException(nameof(query));
			}
			_behaviour.MakeCurrent();
			var searchUri = JiraRestUri.AppendSegments("user", "search").ExtendQuery(new Dictionary<string, object>
			{
				{
					"username", query
				},
				{
					"includeActive", includeActive
				},
				{
					"includeInactive", includeInactive
				},
				{
					"startAt", startAt
				},
				{
					"maxResults", maxResults
				}
			});

			var response = await searchUri.GetAsAsync<HttpResponse<IList<User>, Error>>(cancellationToken).ConfigureAwait(false);
			return HandleErrors(response);
		}

		/// <summary>
		///     Get currrent user information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e4253
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>User</returns>
		public async Task<User> WhoAmIAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var myselfUri = JiraRestUri.AppendSegments("myself");
			_behaviour.MakeCurrent();
			var response = await myselfUri.GetAsAsync<HttpResponse<User, Error>>(cancellationToken).ConfigureAwait(false);
			return HandleErrors(response);
		}

		#endregion

		#region Session

		/// <summary>
		///     Starts new session. No additional authorization requered.
		/// </summary>
		/// <remarks>
		///     Please be aware that although cookie-based authentication has many benefits, such as performance (not having to
		///     make multiple authentication calls), the session cookie can expire..
		/// </remarks>
		/// <param name="username">User username</param>
		/// <param name="password">User password</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>LoginInfo</returns>
		public async Task<LoginInfo> StartSessionAsync(string username, string password, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (username == null)
			{
				throw new ArgumentNullException(nameof(username));
			}
			if (password == null)
			{
				throw new ArgumentNullException(nameof(password));
			}
			if (!_behaviour.HttpSettings.UseCookies)
			{
				throw new ArgumentException("Cookies need to be enabled", nameof(IHttpSettings.UseCookies));
			}
			var sessionUri = JiraAuthUri.AppendSegments("session");

			_behaviour.MakeCurrent();

			var content = new StringContent($"{{ \"username\": \"{username}\", \"password\": \"{password}\"}}");
			content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

			var response = await sessionUri.PostAsync<HttpResponse<SessionResponse, Error>>(content, cancellationToken);

			HandleErrors(response);

			return response.Response.LoginInfo;
		}

		/// <summary>
		///     Ends session. No additional authorization required.
		/// </summary>
		public async Task EndSessionAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			// Find the cookie to expire
			var sessionCookies = _behaviour.CookieContainer.GetCookies(JiraBaseUri).Cast<Cookie>().ToList();

			// check if a cookie was found, if not skip the end session
			if (sessionCookies.Any())
			{
				var sessionUri = JiraAuthUri.AppendSegments("session");

				_behaviour.MakeCurrent();
				var response = await sessionUri.DeleteAsync<HttpResponseMessage>(cancellationToken);

				if (response.StatusCode != HttpStatusCode.NoContent)
				{
					throw new Exception($"Status: {response.StatusCode} Message: Failed to close jira session");
				}
				// Expire the cookie
				foreach (var sessionCookie in sessionCookies)
				{
					sessionCookie.Expired = true;
				}
				
			}
		}

		#endregion
	}
}