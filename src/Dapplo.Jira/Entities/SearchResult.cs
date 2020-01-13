// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

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