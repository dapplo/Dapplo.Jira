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
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;
using Dapplo.Log;

#endregion

namespace Dapplo.Jira
{
	/// <summary>
	///     The marker interface of the server domain
	/// </summary>
	public interface IServerDomain : IJiraDomain
	{
	}

	/// <summary>
	///     This holds all the server related extensions methods
	/// </summary>
	public static class ServerExtensions
	{
		private static readonly LogSource Log = new LogSource();

		/// <summary>
		///     Returns the content, specified by the Uri from the JIRA server.
		///     This is used internally, but can also be used to get e.g. the icon for an issue type.
		/// </summary>
		/// <typeparam name="TResponse"></typeparam>
		/// <param name="jiraClient">IServerDomain to bind the extension method to</param>
		/// <param name="contentUri">Uri</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>TResponse</returns>
		public static async Task<TResponse> GetUriContentAsync<TResponse>(this IServerDomain jiraClient, Uri contentUri, CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class
		{
			Log.Debug().WriteLine("Retrieving content from {0}", contentUri);

			jiraClient.Behaviour.MakeCurrent();

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
		/// <param name="jiraClient">IServerDomain to bind the extension method to</param>
		/// <param name="avatarUrls">AvatarUrls object from User or Myself method, or a project from the projects</param>
		/// <param name="avatarSize">Use one of the AvatarSizes to specify the size you want to have</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Bitmap,BitmapSource or MemoryStream (etc) depending on TResponse</returns>
		public static async Task<TResponse> GetAvatarAsync<TResponse>(this IServerDomain jiraClient, AvatarUrls avatarUrls, AvatarSizes avatarSize = AvatarSizes.ExtraLarge,
			CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class
		{
			var avatarUri = avatarUrls.GetUri(avatarSize);

			jiraClient.Behaviour.MakeCurrent();

			return await jiraClient.GetUriContentAsync<TResponse>(avatarUri, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Get server information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e3828
		/// </summary>
		/// <param name="jiraClient">IServerDomain to bind the extension method to</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>ServerInfo</returns>
		public static async Task<ServerInfo> GetInfoAsync(this IServerDomain jiraClient, CancellationToken cancellationToken = default(CancellationToken))
		{
			Log.Debug().WriteLine("Retrieving server information");

			var serverInfoUri = jiraClient.JiraRestUri.AppendSegments("serverInfo");
			jiraClient.Behaviour.MakeCurrent();

			var response = await serverInfoUri.GetAsAsync<HttpResponse<ServerInfo, Error>>(cancellationToken).ConfigureAwait(false);

			if (Log.IsDebugEnabled() && !response.HasError)
			{
				var serverInfo = response.Response;
				Log.Debug().WriteLine("Server title {0}, version {1}, uri {2}, build date {3}, build number {4}, scm info {5}", serverInfo.ServerTitle, serverInfo.Version, serverInfo.BaseUrl, serverInfo.BuildDate, serverInfo.BuildNumber, serverInfo.ScmInfo);
			}
			return response.HandleErrors();
		}

		/// <summary>
		///     Get server configuration 
		///     See <a href="https://docs.atlassian.com/jira/REST/cloud/#api/2/configuration-getConfiguration">get configuration</a>
		/// </summary>
		/// <param name="jiraClient">IServerDomain to bind the extension method to</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Configuration</returns>
		public static async Task<Configuration> GetConfigurationAsync(this IServerDomain jiraClient, CancellationToken cancellationToken = default(CancellationToken))
		{
			Log.Debug().WriteLine("Retrieving server configuration");

			var serverConfigurationUri = jiraClient.JiraRestUri.AppendSegments("configuration");
			jiraClient.Behaviour.MakeCurrent();

			var response = await serverConfigurationUri.GetAsAsync<HttpResponse<Configuration>>(cancellationToken).ConfigureAwait(false);
			return response.HandleErrors();
		}

		/// <summary>
		///		Admin ONLY!!! Use GetConfigurationAsync instead, which funny enough supplies you with the same information.
		///     Get time tracking configuration (sub-set of the configuration)
		///     See <a href="https://docs.atlassian.com/jira/REST/cloud/#api/2/configuration/timetracking-getSharedTimeTrackingConfiguration">get shared timetracking configuration</a>
		/// </summary>
		/// <param name="jiraClient">IServerDomain to bind the extension method to</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>TimeTrackingConfiguration</returns>
		public static async Task<TimeTrackingConfiguration> GetTimeTrackingConfigurationAsync(this IServerDomain jiraClient, CancellationToken cancellationToken = default(CancellationToken))
		{
			Log.Debug().WriteLine("Retrieving time tracking configuration");

			var timeTrackingConfigurationUri = jiraClient.JiraRestUri.AppendSegments("configuration", "timetracking", "options");
			jiraClient.Behaviour.MakeCurrent();

			var response = await timeTrackingConfigurationUri.GetAsAsync<HttpResponse<TimeTrackingConfiguration>>(cancellationToken).ConfigureAwait(false);
			return response.HandleErrors();
		}

		///  <summary>
		/// 	 Admin ONLY!!!
		///      Set time tracking configuration (sub-set of the configuration)
		///      See <a href="https://docs.atlassian.com/jira/REST/cloud/#api/2/configuration/timetracking-getSharedTimeTrackingConfiguration">get shared timetracking configuration</a>
		///  </summary>
		///  <param name="jiraClient">IServerDomain to bind the extension method to</param>
		/// <param name="timeTrackingConfiguration">TimeTrackingConfiguration to use</param>
		/// <param name="cancellationToken">CancellationToken</param>
		public static async Task SetTimeTrackingConfigurationAsync(this IServerDomain jiraClient, TimeTrackingConfiguration timeTrackingConfiguration, CancellationToken cancellationToken = default(CancellationToken))
		{
			Log.Debug().WriteLine("Retrieving time tracking configuration");

			var timeTrackingConfigurationUri = jiraClient.JiraRestUri.AppendSegments("configuration", "timetracking", "options");
			jiraClient.Behaviour.MakeCurrent();

			var response = await timeTrackingConfigurationUri.PutAsync<HttpResponse>(timeTrackingConfiguration, cancellationToken).ConfigureAwait(false);
			response.HandleStatusCode();
		}
	}
}