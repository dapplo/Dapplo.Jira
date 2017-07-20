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

using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

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
	    [JsonProperty(PropertyName = "expand")]
	    public string Expand { get; set; } = JiraConfig.ExpandSearch == null ? null: string.Join(",", JiraConfig.ExpandSearch);

        /// <summary>
        ///     Fields for this query
        /// </summary>
        [JsonProperty(PropertyName = "fields")]
		public IEnumerable<string> Fields { get; set; } = new List<string>(JiraConfig.SearchFields);

        /// <summary>
        ///     The JQL for this search
        /// </summary>
        [JsonProperty(PropertyName = "jql")]
		public string Jql { get; set; }

		/// <summary>
		///     Does the query (JQL) need to be validated?
		/// </summary>
		[JsonProperty(PropertyName = "validateQuery")]
		public bool ValidateQuery { get; set; } = true;

        /// <summary>
        /// Prepare the search for the next
        /// </summary>
        /// <returns>JqlIssueSearch</returns>
	    public static JqlIssueSearch NextPage(JqlIssueSearch jqlIssueSearch, Page page)
        {
            return new JqlIssueSearch
            {
                StartAt = page.StartAt,
                Expand = jqlIssueSearch.Expand,
                Fields = jqlIssueSearch.Fields,
                Jql = jqlIssueSearch.Jql,
                ValidateQuery = jqlIssueSearch.ValidateQuery,
                MaxResults = page.MaxResults
            };
        }
	}
}