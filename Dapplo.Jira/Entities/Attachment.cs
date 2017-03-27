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
using Newtonsoft.Json;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Attachment information
	///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/attachment
	/// </summary>
	[JsonObject]
	public class Attachment : BaseProperties<long>
	{
		/// <summary>
		///     Who created the attachment
		/// </summary>
		[JsonProperty(PropertyName = "author")]
		public User Author { get; set; }

		/// <summary>
		///     Url which can be used to download the attachment
		/// </summary>
		[JsonProperty(PropertyName = "content")]
		public Uri ContentUri { get; set; }

		/// <summary>
		///     When was the attachment created
		/// </summary>
		[JsonProperty(PropertyName = "created")]
		public DateTimeOffset Created { get; set; }

		/// <summary>
		///     Filename of the attachment
		/// </summary>
		[JsonProperty(PropertyName = "filename")]
		public string Filename { get; set; }

		/// <summary>
		///     Mimetype for the attachment
		/// </summary>
		[JsonProperty(PropertyName = "mimeType")]
		public string MimeType { get; set; }

		/// <summary>
		///     Size (in bytes) of the attachment
		/// </summary>
		[JsonProperty(PropertyName = "size")]
		public long Size { get; set; }

		/// <summary>
		///     An URL to the thumbnail for this attachment
		/// </summary>
		[JsonProperty(PropertyName = "thumbnail")]
		public Uri ThumbnailUri { get; set; }
	}
}