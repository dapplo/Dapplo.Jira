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

using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

#endregion

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
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }

		/// <summary>
		///     Is this field a custom field?
		/// </summary>
		[DataMember(Name = "custom", EmitDefaultValue = false)]
		public bool IsCustom { get; set; }

		/// <summary>
		///     Is this field orderable?
		/// </summary>
		[DataMember(Name = "orderable", EmitDefaultValue = false)]
		public bool IsOrderable { get; set; }

		/// <summary>
		///     Is this field navigable?
		/// </summary>
		[DataMember(Name = "navigable", EmitDefaultValue = false)]
		public bool IsNavigable { get; set; }

		/// <summary>
		///     Is this field searchable?
		/// </summary>
		[DataMember(Name = "searchable", EmitDefaultValue = false)]
		public bool IsSearchable { get; set; }

		/// <summary>
		///     Aliases in where clauses
		/// </summary>
		[DataMember(Name = "clauseNames", EmitDefaultValue = false)]
		public IList<string> ClauseNames { get; set; }

		/// <summary>
		///     Schema for the field
		/// </summary>
		[DataMember(Name = "schema", EmitDefaultValue = false)]
		public Schema Schema { get; set; }
	}
}