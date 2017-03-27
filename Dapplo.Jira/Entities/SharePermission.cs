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

using System.Runtime.Serialization;
using Newtonsoft.Json;

#endregion

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
		[DataMember(Name = "id", EmitDefaultValue = false)]
		public string Id { get; set; }

		/// <summary>
		///     Project for the permissions
		/// </summary>
		[DataMember(Name = "project", EmitDefaultValue = false)]
		public ProjectDigest Project { get; set; }

		/// <summary>
		///     Share type can be project
		/// </summary>
		[DataMember(Name = "type", EmitDefaultValue = false)]
		public string Type { get; set; }
	}
}