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

namespace Dapplo.Jira
{
	/// <summary>
	///     The marker interface for the attachment domain
	/// </summary>
	public interface IAttachmentDomain : IJiraDomain
	{
	}

	/// <summary>
	///     This holds all the attachment related extension methods
	/// </summary>
	public static class AttachmentExtensions
	{
		private static readonly LogSource Log = new LogSource();

		/// <summary>
		///     Attach content to the specified issue
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e3035
		/// </summary>
		/// <param name="jiraClient">IAttachmentDomain to bind the extension method to</param>
		/// <param name="issueKey">the key of the issue to attach to</param>
		/// <param name="content">the content can be anything what Dapplo.HttpExtensions supports</param>
		/// <param name="filename">Filename for the attachment</param>
		/// <param name="contentType">content-type for the attachment</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Attachment</returns>
		public static async Task<Attachment> AttachAsync<TContent>(this IAttachmentDomain jiraClient, string issueKey, TContent content, string filename, string contentType = null,
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

			Log.Debug().WriteLine("Attaching to issue {0}", issueKey);

			var attachment = new AttachmentContainer<TContent>
			{
				Content = content,
				ContentType = contentType,
				FileName = filename
			};
			jiraClient.Behaviour.MakeCurrent();
			var attachUri = jiraClient.JiraRestUri.AppendSegments("issue", issueKey, "attachments");
			var response = await attachUri.PostAsync<HttpResponse<IList<Attachment>, Error>>(attachment, cancellationToken).ConfigureAwait(false);
			jiraClient.HandleErrors(response);
			// Return the attachment, should be only one!
			return response.Response[0];
		}

		/// <summary>
		///     Delete the specified attachment
		/// </summary>
		/// <param name="jiraClient">IAttachmentDomain to bind the extension method to</param>
		/// <param name="attachment">The Attachment to delete</param>
		/// <param name="cancellationToken">CancellationToken</param>
		public static Task DeleteAsync(this IAttachmentDomain jiraClient, Attachment attachment, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (attachment == null)
			{
				throw new ArgumentNullException(nameof(attachment));
			}

			return jiraClient.DeleteAsync(attachment.Id, cancellationToken);
		}

		/// <summary>
		///     Delete the specified attachment
		/// </summary>
		/// <param name="jiraClient">IAttachmentDomain to bind the extension method to</param>
		/// <param name="attachmentId">Id from the attachment</param>
		/// <param name="cancellationToken">CancellationToken</param>
		public static async Task DeleteAsync(this IAttachmentDomain jiraClient, long attachmentId, CancellationToken cancellationToken = default(CancellationToken))
		{
			Log.Debug().WriteLine("Deleting attachment {0}", attachmentId);

			jiraClient.Behaviour.MakeCurrent();
			var deleteAttachmentUri = jiraClient.JiraRestUri.AppendSegments("attachment", attachmentId);
			await deleteAttachmentUri.DeleteAsync(cancellationToken).ConfigureAwait(false);
		}


		/// <summary>
		///     Get the content for the specified attachment
		/// </summary>
		/// <typeparam name="TResponse">
		///     the type which is returned, can be decided by the client and should be supported by
		///     Dapplo.HttpExtensions or your own IHttpContentConverter
		/// </typeparam>
		/// <param name="jiraClient">IAttachmentDomain to bind the extension method to</param>
		/// <param name="attachment">the attachment</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>TResponse</returns>
		public static async Task<TResponse> GetContentAsAsync<TResponse>(this IAttachmentDomain jiraClient, Attachment attachment, CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class
		{
			if (attachment == null)
			{
				throw new ArgumentNullException(nameof(attachment));
			}

			Log.Debug().WriteLine("Getting attachment content for {0}", attachment.Id);

			return await jiraClient.GetUriContentAsync<TResponse>(attachment.ContentUri, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Get the thumbnail for the specified attachment
		/// </summary>
		/// <typeparam name="TResponse">
		///     the type which is returned, can be decided by the client and should be supported by
		///     Dapplo.HttpExtensions or your own IHttpContentConverter
		/// </typeparam>
		/// <param name="jiraClient">IAttachmentDomain to bind the extension method to</param>
		/// <param name="attachment">the attachment</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>TResponse</returns>
		public static async Task<TResponse> GetThumbnailAsAsync<TResponse>(this IAttachmentDomain jiraClient, Attachment attachment, CancellationToken cancellationToken = default(CancellationToken))
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

			Log.Debug().WriteLine("Deleting attachment  thumbnail {0}", attachment.Id);

			return await jiraClient.GetUriContentAsync<TResponse>(attachment.ThumbnailUri, cancellationToken).ConfigureAwait(false);
		}
	}
}