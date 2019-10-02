// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2019 Dapplo
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

using Dapplo.HttpExtensions.Support;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     The attachment needs to be uploaded as a multi-part request
	/// </summary>
	[HttpRequest(MultiPart = true)]
	public class AttachmentContainer<T>
	{
		/// <summary>
		///     The actual content for the attachment
		/// </summary>
		[HttpPart(HttpParts.RequestContent)]
		public T Content { get; set; }

		/// <summary>
		///     The name of the content, this is always "file"
		/// </summary>
		[HttpPart(HttpParts.RequestMultipartName)]
		public string ContentName { get; } = "file";

		/// <summary>
		///     The (mime) type for the content
		/// </summary>
		[HttpPart(HttpParts.RequestContentType)]
		public string ContentType { get; set; } = "text/plain";


		/// <summary>
		///     Filename for the attachment
		/// </summary>
		[HttpPart(HttpParts.RequestMultipartFilename)]
		public string FileName { get; set; }
	}
}