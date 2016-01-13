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

	Dapplo.Exchange is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Runtime.Serialization;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	/// Server Info is used for the version and title
	/// See: https://docs.atlassian.com/jira/REST/latest/#api/2/serverInfo
	/// </summary>
	[DataContract]
	public class ServerInfo
	{
		[DataMember(Name = "baseUrl")]
		public Uri BaseUrl { get; set; }

		[DataMember(Name = "version")]
		public string Version { get; set; }

		[DataMember(Name = "buildNumber")]
		public int BuildNumber { get; set; }

		[DataMember(Name = "buildDate")]
		public DateTimeOffset buildDate { get; set; }

		[DataMember(Name = "scmInfo")]
		public string ScmInfo { get; set; }

		[DataMember(Name = "serverTitle")]
		public string ServerTitle { get; set; }
	}
}
