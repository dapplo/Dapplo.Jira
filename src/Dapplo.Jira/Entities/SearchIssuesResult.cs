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

using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Search result information, see <a href="https://docs.atlassian.com/jira/REST/latest/#api/2/search-search">here</a>
	/// </summary>
	[JsonObject]
	public class SearchIssuesResult<TIssue, TSearch> : PageableResult, IEnumerable<TIssue> where TIssue : IssueBase
	{
        /// <summary>
        /// The original search value, used to continue searches
        /// </summary>
        [JsonIgnore]
        public TSearch SearchParameter { get; set; }

		/// <summary>
		///     Expand values
		/// </summary>
		[JsonProperty("expand")]
		public string Expand { get; set; }

		/// <summary>
		///     List of issues
		/// </summary>
		[JsonProperty("issues")]
		public IList<TIssue> Issues { get; set; }

	    /// <summary>
	    ///     Nummber of items in the result
	    /// </summary>
	    [JsonIgnore]
	    public int Count => Issues?.Count ?? 0;

        /// <summary>
        ///     Is this the last page?
        /// </summary>
        [JsonIgnore]
	    public bool IsLastPage => !Total.HasValue || StartAt + Count >= Total;

	    /// <summary>
	    /// Retrieve the next page, this is based upon the number of items that was returned
	    /// </summary>
	    [JsonIgnore]
	    public Page NextPage => new Page
	    {
	        StartAt = StartAt + (Issues?.Count ?? 0),
	        MaxResults = MaxResults
	    };

        IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Generic IEnumerator implementation
		/// </summary>
		/// <returns>IEnumerator with TIssue</returns>
		public IEnumerator<TIssue> GetEnumerator()
		{
			return Issues.GetEnumerator();
		}
	}
}