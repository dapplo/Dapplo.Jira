//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Jira
// 
//  Dapplo.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Runtime.Serialization;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Worklog information
	/// </summary>
	[DataContract]
	public class Worklog : BaseProperties<string>
	{
		/// <summary>
		///     Author of this worklog
		/// </summary>
		[DataMember(Name = "author")]
		public User Author { get; set; }

		/// <summary>
		///     Comment for this worklog
		/// </summary>
		[DataMember(Name = "comment")]
		public string Comment { get; set; }

		/// <summary>
		///     When was the worklog created
		/// </summary>
		[DataMember(Name = "created")]
		public DateTimeOffset Create { get; set; }

		/// <summary>
		///     When was the worklog started
		/// </summary>
		[DataMember(Name = "started")]
		public DateTimeOffset Started { get; set; }

		/// <summary>
		///     Time spent in this worklog, this is a number and qualifier (h = hour, d = day etc)
		/// </summary>
		[DataMember(Name = "timeSpent")]
		public string TimeSpent { get; set; }

		/// <summary>
		///     Time spent in this worklog, in seconds
		/// </summary>
		[DataMember(Name = "timeSpentSeconds")]
		public long TimeSpentSeconds { get; set; }

		/// <summary>
		///     Who updated this worklog
		/// </summary>
		[DataMember(Name = "updateAuthor")]
		public User UpdateAuthor { get; set; }

		/// <summary>
		///     When was the worklog updated
		/// </summary>
		[DataMember(Name = "updated")]
		public DateTimeOffset Updated { get; set; }
	}
}