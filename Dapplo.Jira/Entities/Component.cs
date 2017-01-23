//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Jira
// 
//  Dapplo.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System.Runtime.Serialization;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Component information, retrieved for /component/id
	///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/component
	/// </summary>
	[DataContract]
	public class Component : ComponentDigest
	{
		/// <summary>
		///     TODO: Needs comment
		/// </summary>
		[DataMember(Name = "assignee", EmitDefaultValue = false)]
		public User Assignee { get; set; }

		/// <summary>
		///     TODO: Needs comment
		/// </summary>
		[DataMember(Name = "assigneeType", EmitDefaultValue = false)]
		public string AssigneeType { get; set; }

		/// <summary>
		///     Description of this component
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; }

		/// <summary>
		///     Lead for this component
		/// </summary>
		[DataMember(Name = "lead", EmitDefaultValue = false)]
		public User Lead { get; set; }

		/// <summary>
		///     Name of the component
		/// </summary>
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }

		/// <summary>
		///     Project key where this component belongs to
		/// </summary>
		[DataMember(Name = "project", EmitDefaultValue = false)]
		public string Project { get; set; }

		/// <summary>
		///     Id of the project where this component belongs to
		/// </summary>
		[DataMember(Name = "projectId", EmitDefaultValue = false)]
		public int ProjectId { get; set; }

		/// <summary>
		///     TODO: Needs comment
		/// </summary>
		[DataMember(Name = "realAssignee", EmitDefaultValue = false)]
		public User RealAssignee { get; set; }

		/// <summary>
		///     TODO: Needs comment
		/// </summary>
		[DataMember(Name = "realAssigneeType", EmitDefaultValue = false)]
		public string RealAssigneeType { get; set; }
	}
}