// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Column for the Board column config
	/// </summary>
	[JsonObject]
	public class Column
	{
		/// <summary>
		///     Name for the column
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///     States for the column
		/// </summary>
		[JsonProperty("statuses")]
		public IList<Status> States { get; set; }
	}
}