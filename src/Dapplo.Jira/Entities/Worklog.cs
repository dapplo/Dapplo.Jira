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
using System.ComponentModel;
using Dapplo.Jira.Json;
using Newtonsoft.Json;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Worklog information
	/// </summary>
	[JsonObject]
	public class Worklog : BaseProperties<string>
	{
		/// <summary>
		/// Default constructor for a work log
		/// </summary>
		public Worklog()
		{
		}

		/// <summary>
		/// Default constructor with a TimeSpan for the TimeSpentSeconds
		/// </summary>
		/// <param name="timeSpent">TimeSpan</param>
		public Worklog(TimeSpan timeSpent)
		{
			TimeSpentSeconds = (int) timeSpent.TotalSeconds;
		}

		/// <summary>
		///     Author of this worklog
		/// </summary>
		[JsonProperty("author")]
		public User Author { get; set; }

		/// <summary>
		///     Comment for this worklog
		/// </summary>
		[JsonProperty("comment")]
		public string Comment { get; set; }

		/// <summary>
		///     When was the worklog created
		/// </summary>
		[JsonProperty("created")]
		[ReadOnly(true)]
		public DateTimeOffset? Created { get; set; }

		/// <summary>
		///     When was the worklog started
		/// </summary>
		[JsonProperty("started")]
		[JsonConverter(typeof(CustomDateTimeOffsetConverter))]
		public DateTimeOffset? Started { get; set; }

		/// <summary>
		///     Time spent in this worklog, this is a number and qualifier (h = hour, d = day etc)
		/// </summary>
		[JsonProperty("timeSpent")]
		public string TimeSpent { get; set; }

		/// <summary>
		///     Time spent in this worklog, in seconds
		/// </summary>
		[JsonProperty("timeSpentSeconds")]
		public long? TimeSpentSeconds { get; set; }

		/// <summary>
		///     Who updated this worklog, this cannot be updated
		/// </summary>
		[JsonProperty("updateAuthor")]
		[ReadOnly(true)]
		public User UpdateAuthor { get; set; }

		/// <summary>
		///     When was the worklog updated, this cannot be updated
		/// </summary>
		[JsonProperty("updated")]
		[ReadOnly(true)]
		public DateTimeOffset? Updated { get; set; }

		/// <summary>
		///     Visibility
		/// </summary>
		[JsonProperty("visibility")]
		public Visibility Visibility { get; set; }
	}
}