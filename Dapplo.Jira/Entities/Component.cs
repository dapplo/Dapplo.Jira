/*
	Dapplo - building blocks for desktop applications
	Copyright (C) 2015-2016 Dapplo

	For more information see: http://dapplo.net/
	Dapplo repositories are hosted on GitHub: https://github.com/dapplo

	This file is part of Dapplo.Jira

	Dapplo.Jira is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	Dapplo.Jira is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/>.
 */

using System.Runtime.Serialization;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	/// Component information
	/// See: https://docs.atlassian.com/jira/REST/latest/#api/2/component
	/// </summary>
	[DataContract]
	public class Component
	{
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "description")]
		public string Description { get; set; }

		[DataMember(Name = "lead")]
		public User Lead { get; set; }

		[DataMember(Name = "assigneeType")]
		public string AssigneeType { get; set; }

		[DataMember(Name = "assignee")]
		public User Assignee { get; set; }

		[DataMember(Name = "realAssigneeType")]
		public string RealAssigneeType { get; set; }

		[DataMember(Name = "realAssignee")]
		public User RealAssignee { get; set; }

		[DataMember(Name = "isAssigneeTypeValid")]
		public bool IsAssigneeTypeValid { get; set; }

		[DataMember(Name = "project")]
		public string Project { get; set; }

		[DataMember(Name = "projectId")]
		public int ProjectId { get; set; }
	}
}
