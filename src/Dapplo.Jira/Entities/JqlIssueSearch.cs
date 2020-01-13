// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Search request information, see <a href="https://docs.atlassian.com/jira/REST/latest/#api/2/search-search">here</a>
	/// </summary>
	[JsonObject]
	public class JqlIssueSearch : Page
	{
		/// <summary>
		///     Expand values
		/// </summary>
		/// <value>
		///     The expands.
		/// </value>
		[JsonProperty("expand")]
		public IEnumerable<string> Expand { get; set; } = JiraConfig.ExpandSearch;

        /// <summary>
        ///     Fields for this query
        /// </summary>
        [JsonProperty("fields")]
		public IEnumerable<string> Fields { get; set; } = JiraConfig.SearchFields;

		/// <summary>
		///     The JQL for this search
		/// </summary>
		[JsonProperty("jql")]
		public string Jql { get; set; }

		/// <summary>
		///     Does the query (JQL) need to be validated?
		/// </summary>
		[JsonProperty("validateQuery")]
		public bool ValidateQuery { get; set; } = true;
	}
}