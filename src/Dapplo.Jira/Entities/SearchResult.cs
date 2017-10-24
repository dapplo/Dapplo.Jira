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
    ///     Pagable results
    /// </summary>
    /// <typeparam name="TResultType">The type for the results</typeparam>
    /// <typeparam name="TSearch">The type for the search parameter</typeparam>
	[JsonObject]
	public class SearchResult<TResultType, TSearch> : PageableResult, IEnumerable<TResultType>
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
		///     Results
		/// </summary>
		[JsonProperty("values")]
		public IList<TResultType> Values { get; set; }

	    /// <summary>
	    ///     Nummber of items in the result
	    /// </summary>
	    [JsonIgnore]
	    public int Count => Values?.Count ?? 0;

        /// <summary>
        ///     Is this the last page?
        /// </summary>
        [JsonIgnore]
	    public bool IsLastPage => !Total.HasValue || StartAt + Count >= Total;

        /// <summary>
        /// Retrieve the next page
        /// </summary>
        [JsonIgnore]
	    public Page NextPage => new Page
	    {
            StartAt = StartAt + Count,
            MaxResults = MaxResults
        };

        IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// IEnumerator implementation
		/// </summary>
		/// <returns>IEnumerator of type TResultType</returns>
		public IEnumerator<TResultType> GetEnumerator()
		{
			return Values.GetEnumerator();
		}
	}
}