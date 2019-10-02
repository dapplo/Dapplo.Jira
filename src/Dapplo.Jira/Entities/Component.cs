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

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Component information, retrieved for /component/id
	///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/component
	/// </summary>
	[JsonObject]
	public class Component : ComponentDigest
	{
		/// <summary>
		///     TODO: Needs comment
		/// </summary>
		[JsonProperty("assignee")]
		public User Assignee { get; set; }

		/// <summary>
		///     TODO: Needs comment
		/// </summary>
		[JsonProperty("assigneeType")]
		public string AssigneeType { get; set; }

		/// <summary>
		///     Description of this component
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>
		///     Lead for this component
		/// </summary>
		[JsonProperty("lead")]
		public User Lead { get; set; }

		/// <summary>
		///     Name of the component
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///     Project key where this component belongs to
		/// </summary>
		[JsonProperty("project")]
		public string Project { get; set; }

		/// <summary>
		///     Id of the project where this component belongs to
		/// </summary>
		[JsonProperty("projectId")]
		public int ProjectId { get; set; }

		/// <summary>
		///     TODO: Needs comment
		/// </summary>
		[JsonProperty("realAssignee")]
		public User RealAssignee { get; set; }

		/// <summary>
		///     TODO: Needs comment
		/// </summary>
		[JsonProperty("realAssigneeType")]
		public string RealAssigneeType { get; set; }
	}
}