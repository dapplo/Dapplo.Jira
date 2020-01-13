// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Filter information
	/// </summary>
	[JsonObject]
	public class Field : BaseProperties<string>
	{
		/// <summary>
		///     Name of the field
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///     Is this field a custom field?
		/// </summary>
		[JsonProperty("custom")]
		public bool IsCustom { get; set; }

		/// <summary>
		///     Is this field orderable?
		/// </summary>
		[JsonProperty("orderable")]
		public bool IsOrderable { get; set; }

		/// <summary>
		///     Is this field navigable?
		/// </summary>
		[JsonProperty("navigable")]
		public bool IsNavigable { get; set; }

		/// <summary>
		///     Is this field searchable?
		/// </summary>
		[JsonProperty("searchable")]
		public bool IsSearchable { get; set; }

		/// <summary>
		///     Aliases in where clauses
		/// </summary>
		[JsonProperty("clauseNames")]
		public IList<string> ClauseNames { get; set; }

		/// <summary>
		///     Schema for the field
		/// </summary>
		[JsonProperty("schema")]
		public Schema Schema { get; set; }
	}
}