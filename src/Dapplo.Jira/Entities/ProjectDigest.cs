#region Dapplo 2017-2019 - GNU Lesser General Public License

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

#endregion

#region Usings

using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Project information (digest)
	///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/project
	/// </summary>
	[JsonObject]
	public class ProjectDigest : BaseProperties<long>
	{
		/// <summary>
		///     Avatar for this project
		/// </summary>
		[JsonProperty("avatarUrls")]
		public AvatarUrls Avatar { get; set; }

		/// <summary>
		///     The projects category
		/// </summary>
		[JsonProperty("projectCategory")]
		public ProjectCategory Category { get; set; }

		/// <summary>
		///     Key for this project (the prefix of the issues in the project)
		/// </summary>
		[JsonProperty("key")]
		public string Key { get; set; }

		/// <summary>
		///     User who is the lead for the project
		/// </summary>
		[JsonProperty("lead")]
		public User Lead { get; set; }

		/// <summary>
		///     Name of the project
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///     All project keys associated with the project
		/// </summary>
		[JsonProperty("projectKeys")]
		public IList<string> ProjectKeys { get; set; }
	}
}