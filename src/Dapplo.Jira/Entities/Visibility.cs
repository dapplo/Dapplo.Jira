// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Visibility information
	/// </summary>
	[JsonObject]
	public class Visibility
	{
		/// <summary>
		///     Type for the visibility
		/// </summary>
		[JsonProperty("type")]
		public string Type { get; set; }

		/// <summary>
		///     Value of the visibility
		/// </summary>
		[JsonProperty("value")]
		public string Value { get; set; }
	}
}