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

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Search result information, see <a href="https://docs.atlassian.com/jira/REST/latest/#api/2/search-search">here</a>
	/// </summary>
	[DataContract]
	public class SearchResult : PageableResult, IEnumerable<Issue>
	{
		/// <summary>
		///     Expand values
		/// </summary>
		[DataMember(Name = "expand", EmitDefaultValue = false)]
		public string Expand { get; set; }

		/// <summary>
		///     List of issues
		/// </summary>
		[DataMember(Name = "issues", EmitDefaultValue = false)]
		public IList<Issue> Issues { get; set; }

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<Issue> GetEnumerator()
		{
			return Issues.GetEnumerator();
		}
	}
}