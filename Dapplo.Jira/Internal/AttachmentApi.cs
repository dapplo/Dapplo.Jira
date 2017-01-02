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

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;
using Dapplo.Log;

namespace Dapplo.Jira.Internal
{
	/// <summary>
	/// Attachment API
	/// </summary>
	internal class AttachmentApi : IAttachmentApi
	{
		private static readonly LogSource Log = new LogSource();

		private readonly JiraApi _jiraApi;

		internal AttachmentApi(JiraApi jiraApi)
		{
			_jiraApi = jiraApi;
		}

		/// <inheritdoc />
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

			Log.Debug().WriteLine("Attaching to issue {0}", issueKey);

			var attachment = new AttachmentContainer<TContent>
			{
				Content = content,
				ContentType = contentType,
				FileName = filename
			};
			_jiraApi.Behaviour.MakeCurrent();
			var attachUri = _jiraApi.JiraRestUri.AppendSegments("issue", issueKey, "attachments");
			var response = await attachUri.PostAsync<HttpResponse<IList<Attachment>, Error>>(attachment, cancellationToken).ConfigureAwait(false);
			_jiraApi.HandleErrors(response);
			// Return the attachment, should be only one!
			return response.Response[0];
		}

		/// <inheritdoc />
		public Task DeleteAsync(Attachment attachment, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (attachment == null)
			{
				throw new ArgumentNullException(nameof(attachment));
			}

			return DeleteAsync(attachment.Id, cancellationToken);
		}

		/// <inheritdoc />
		public async Task DeleteAsync(long attachmentId, CancellationToken cancellationToken = default(CancellationToken))
		{
			Log.Debug().WriteLine("Deleting attachment {0}", attachmentId);

			_jiraApi.Behaviour.MakeCurrent();
			var deleteAttachmentUri = _jiraApi.JiraRestUri.AppendSegments("attachment", attachmentId);
			await deleteAttachmentUri.DeleteAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<TResponse> GetContentAsAsync<TResponse>(Attachment attachment, CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class
		{
			if (attachment == null)
			{
				throw new ArgumentNullException(nameof(attachment));
			}

			Log.Debug().WriteLine("Getting attachment content for {0}", attachment.Id);

			return await _jiraApi.GetUriContentAsync<TResponse>(attachment.ContentUri, cancellationToken).ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task<TResponse> GetThumbnailAsAsync<TResponse>(Attachment attachment, CancellationToken cancellationToken = default(CancellationToken))
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

			return await _jiraApi.GetUriContentAsync<TResponse>(attachment.ThumbnailUri, cancellationToken).ConfigureAwait(false);
		}

	}
}
