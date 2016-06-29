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
	///     Priority information
	/// </summary>
	[DataContract]
	public class Priority : BaseProperties<int>
	{
		/// <summary>
		///     Url to the icon for this priority
		/// </summary>
		[DataMember(Name = "iconUrl")]
		public Uri IconUrl { get; set; }

		/// <summary>
		///     Name of the priority
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>
		///     Status color
		/// </summary>
		[DataMember(Name = "statusColor")]
		public string StatusColor { get; set; }

		/// <summary>
		///     Description of the priority
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; }
	}
}