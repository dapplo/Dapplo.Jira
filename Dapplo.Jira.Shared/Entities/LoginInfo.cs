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

using System.Runtime.Serialization;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Jira login info
	/// </summary>
	[DataContract]
	public class LoginInfo
	{
		/// <summary>
		///     Failed login count
		/// </summary>
		[DataMember(Name = "failedLoginCount")]
		public int FailedLoginCount { get; set; }

		/// <summary>
		///     Login count
		/// </summary>
		[DataMember(Name = "loginCount")]
		public int LoginCount { get; set; }

		/// <summary>
		///     Last failed login time
		/// </summary>
		[DataMember(Name = "lastFailedLoginTime")]
		public string LastFailedLoginTime { get; set; }

		/// <summary>
		///     Previous login time
		/// </summary>
		[DataMember(Name = "previousLoginTime")]
		public string PreviousLoginTime { get; set; }
	}
}