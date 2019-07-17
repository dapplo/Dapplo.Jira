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

using System;
using System.ComponentModel;
using Newtonsoft.Json;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     User information
	///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/user
	/// </summary>
	[JsonObject]
	public class User
	{
		/// <summary>
		///     Use this to specify nobody
		/// </summary>
		/// <returns>A User which can be used in e.g. AssignAsync to remove the assignee</returns>
		public static User Nobody { get; } = new User {Name = null};

		/// <summary>
		///     Default assignee
		/// </summary>
		/// <returns>A User which can be used in e.g. AssignAsync to assign to the default user</returns>
		public static User Default { get; } = new User {Name = "-1"};

		/// <summary>
		///     true if the user is active (license count)
		/// </summary>
		[JsonProperty("active")]
		[ReadOnly(true)]
		public bool Active { get; set; }

		/// <summary>
		///     Avatar urls (links to 16x16, 24x24, 32x32, 48x48 icons) for this user
		/// </summary>
		[JsonProperty("avatarUrls")]
		[ReadOnly(true)]
		public AvatarUrls Avatars { get; set; }

		/// <summary>
		///     Display name for the user
		/// </summary>
		[JsonProperty("displayName")]
		[ReadOnly(true)]
		public string DisplayName { get; set; }

		/// <summary>
		///     Email address of the user
		/// </summary>
		[JsonProperty("emailAddress")]
		[ReadOnly(true)]
		public string EmailAddress { get; set; }

		/// <summary>
		///     Name of the user
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///     Link to this item (self)
		/// </summary>
		[JsonProperty("self")]
		[ReadOnly(true)]
		public Uri Self { get; set; }
	}
}