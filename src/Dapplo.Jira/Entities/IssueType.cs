// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     IssueType information
	/// </summary>
	[JsonObject]
	public class IssueType : BaseProperties<long>
	{
		/// <summary>
		///     Description of the issue type
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>
		///     URL to the icon for the issue type
		/// </summary>
		[JsonProperty("iconUrl")]
		public Uri IconUri { get; set; }

		/// <summary>
		///     Is the issue type a sub task?
		/// </summary>
		[JsonProperty("subtask")]
		public bool IsSubTask { get; set; }

		/// <summary>
		///     Name of the issue type
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }
	}
}