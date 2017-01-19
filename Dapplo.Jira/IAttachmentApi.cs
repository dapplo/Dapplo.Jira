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

using System.Threading;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;

#endregion

namespace Dapplo.Jira
{
	/// <summary>
	///     API for attachments
	/// </summary>
	public interface IAttachmentApi
	{
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
		Task<Attachment> AttachAsync<TContent>(string issueKey, TContent content, string filename, string contentType = null, CancellationToken cancellationToken = default(CancellationToken)) where TContent : class;

		/// <summary>
		///     Delete the specified attachment
		/// </summary>
		/// <param name="attachmentId">Id from the attachment</param>
		/// <param name="cancellationToken">CancellationToken</param>
		Task DeleteAsync(long attachmentId, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Delete the specified attachment
		/// </summary>
		/// <param name="attachment">The Attachment to delete</param>
		/// <param name="cancellationToken">CancellationToken</param>
		Task DeleteAsync(Attachment attachment, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Get the content for the specified attachment
		/// </summary>
		/// <typeparam name="TResponse">
		///     the type which is returned, can be decided by the client and should be supported by
		///     Dapplo.HttpExtensions or your own IHttpContentConverter
		/// </typeparam>
		/// <param name="attachment">the attachment</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>TResponse</returns>
		Task<TResponse> GetContentAsAsync<TResponse>(Attachment attachment, CancellationToken cancellationToken = default(CancellationToken)) where TResponse : class;

		/// <summary>
		///     Get the thumbnail for the specified attachment
		/// </summary>
		/// <typeparam name="TResponse">
		///     the type which is returned, can be decided by the client and should be supported by
		///     Dapplo.HttpExtensions or your own IHttpContentConverter
		/// </typeparam>
		/// <param name="attachment">the attachment</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>TResponse</returns>
		Task<TResponse> GetThumbnailAsAsync<TResponse>(Attachment attachment, CancellationToken cancellationToken = default(CancellationToken)) where TResponse : class;
	}
}