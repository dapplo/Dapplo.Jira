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