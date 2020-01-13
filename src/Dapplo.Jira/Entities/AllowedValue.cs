// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Possible field information
	/// </summary>
	[JsonObject]
	public class AllowedValue : BaseProperties<long>
	{
		/// <summary>
		///     TODO: Describe
		/// </summary>
		[JsonProperty("autoCompleteUrl")]
		public Uri AutoCompleteUrl { get; set; }

		/// <summary>
		///     Name of the allowd value
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///     Possible operations
		/// </summary>
		[JsonProperty("operations")]
		public IList<string> Operations { get; set; }
	}
}