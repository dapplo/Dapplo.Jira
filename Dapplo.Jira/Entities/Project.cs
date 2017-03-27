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

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Project information (retrieved via /project/id)
	///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/project
	/// </summary>
	[JsonObject]
	public class Project : ProjectDigest
	{
		/// <summary>
		///     AssigneeType describes how the assignment of tickets works, if this says project-lead every ticket will be assigned
		///     to the person which that role.
		/// </summary>
		[DataMember(Name = "assigneeType", EmitDefaultValue = false)]
		public string AssigneeType { get; set; }

		/// <summary>
		///     Url to browse the tickets with
		/// </summary>
		[DataMember(Name = "url", EmitDefaultValue = false)]
		public Uri BrowseUrl { get; set; }

		/// <summary>
		///     Components for this project, this is only a "digest" retrieve the component details for more information.
		/// </summary>
		[DataMember(Name = "components", EmitDefaultValue = false)]
		public IList<ComponentDigest> Components { get; set; }

		/// <summary>
		///     The description of the project
		/// </summary>
		[DataMember(Name = "description", EmitDefaultValue = false)]
		public string Description { get; set; }

		/// <summary>
		///     TODO: Uncertain what this is, please comment!
		/// </summary>
		[DataMember(Name = "email", EmitDefaultValue = false)]
		public string Email { get; set; }

		/// <summary>
		///     Possible issue types for this project
		/// </summary>
		[DataMember(Name = "issueTypes", EmitDefaultValue = false)]
		public IList<IssueType> IssueTypes { get; set; }

		/// <summary>
		///     Urls to the possible roles for this project
		/// </summary>
		[DataMember(Name = "roles", EmitDefaultValue = false)]
		public IDictionary<string, Uri> Roles { get; set; }

		/// <summary>
		///     Possible versions for this project
		/// </summary>
		[DataMember(Name = "versions", EmitDefaultValue = false)]
		public IList<Version> Versions { get; set; }
	}
}