// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Information on the custom field id for the ranking information
	/// </summary>
	[JsonObject]
	public class RankingCustomFieldInfo
	{
		/// <summary>
		///     Id of the Rank custom field
		/// </summary>
		[JsonProperty("rankCustomFieldId")]
		public long? RankCustomFieldId { get; set; }
	}
}