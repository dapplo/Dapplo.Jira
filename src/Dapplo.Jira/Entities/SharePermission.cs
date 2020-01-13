// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Share Permission information
	/// </summary>
	[JsonObject]
	public class SharePermission
	{
		/// <summary>
		///     Id for the share permission
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		///     Project for the permissions
		/// </summary>
		[JsonProperty("project")]
		public ProjectDigest Project { get; set; }

		/// <summary>
		///     Share type can be project
		/// </summary>
		[JsonProperty("type")]
		public string Type { get; set; }
	}
}