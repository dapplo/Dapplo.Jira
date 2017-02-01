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
using System.Runtime.Serialization;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Server Info is used for the version and title
	///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/serverInfo
	/// </summary>
	[DataContract]
	public class ServerInfo
	{
		/// <summary>
		///     The base URL for this server
		/// </summary>
		[DataMember(Name = "baseUrl", EmitDefaultValue = false)]
		public Uri BaseUrl { get; set; }

		/// <summary>
		///     Date of the build
		/// </summary>
		[DataMember(Name = "buildDate", EmitDefaultValue = false)]
		public DateTimeOffset BuildDate { get; set; }

		/// <summary>
		///     Build number (internal information)
		/// </summary>
		[DataMember(Name = "buildNumber", EmitDefaultValue = false)]
		public int BuildNumber { get; set; }

		/// <summary>
		///     Git commit id (at lease until the SCM is changed)
		/// </summary>
		[DataMember(Name = "scmInfo", EmitDefaultValue = false)]
		public string ScmInfo { get; set; }

		/// <summary>
		///     Title of the server
		/// </summary>
		[DataMember(Name = "serverTitle", EmitDefaultValue = false)]
		public string ServerTitle { get; set; }

		/// <summary>
		///     Version of the software
		/// </summary>
		[DataMember(Name = "version", EmitDefaultValue = false)]
		public string Version { get; set; }
	}
}