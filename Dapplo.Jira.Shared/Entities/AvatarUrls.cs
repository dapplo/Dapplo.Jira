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

using System;
using System.Runtime.Serialization;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	/// Avatar information, has 16x16,24x24,32x32,48x48 Urls
	/// See: https://docs.atlassian.com/jira/REST/latest/#api/2/project
	/// </summary>
	[DataContract]
	public class AvatarUrls
	{
		[DataMember(Name = "16x16")]
		public Uri ExtraSmall { get; set; }

		[DataMember(Name = "24x24")]
		public Uri Small { get; set; }

		[DataMember(Name = "32x32")]
		public Uri Large { get; set; }

		[DataMember(Name = "48x48")]
		public Uri ExtraLarge { get; set; }
	}
}
