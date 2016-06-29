#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016 Dapplo
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
using System.Runtime.Serialization;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     User information
	///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/user
	/// </summary>
	[DataContract]
	public class User
	{
		/// <summary>
		///     true if the user is active (license count)
		/// </summary>
		[DataMember(Name = "active")]
		public bool Active { get; set; }

		/// <summary>
		///     Avatar urls (links to 16x16, 24x24, 32x32, 48x48 icons) for this user
		/// </summary>
		[DataMember(Name = "avatarUrls")]
		public AvatarUrls Avatars { get; set; }

		/// <summary>
		///     Display name for the user
		/// </summary>
		[DataMember(Name = "displayName")]
		public string DisplayName { get; set; }

		/// <summary>
		///     Email address of the user
		/// </summary>
		[DataMember(Name = "emailAddress")]
		public string EmailAddress { get; set; }

		/// <summary>
		///     Name of the user
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>
		///     Link to this item (self)
		/// </summary>
		[DataMember(Name = "self")]
		public Uri Self { get; set; }
	}
}