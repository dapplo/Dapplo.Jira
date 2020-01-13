// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Project category information
	/// </summary>
	[JsonObject]
	public class ProjectCategory : BaseProperties<long>
	{
		/// <summary>
		///     Description for the category
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>
		///     Name of the category
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }
	}
}