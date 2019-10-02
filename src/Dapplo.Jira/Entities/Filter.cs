// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2019 Dapplo
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

using System;
using System.Collections.Generic;
using Dapplo.Jira.Query;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Filter information
	/// </summary>
	[JsonObject]
	public class Filter : BaseProperties<long>
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public Filter()
		{
		}

		/// <summary>
		/// Constructor with a JQL clause
		/// </summary>
		/// <param name="jql">IFinalClause</param>
		public Filter(IFinalClause jql)
		{
			Jql = jql.ToString();
		}

		/// <summary>
		/// Constructor with a JQL clause and name
		/// </summary>
		/// <param name="name">Name</param>
		/// <param name="jql">IFinalClause</param>
		public Filter(string name, IFinalClause jql)
		{
			Name = name;
			Jql = jql.ToString();
		}

		/// <summary>
		///     Description of the filter
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>
		///     Is the filter a favorite?
		/// </summary>
		[JsonProperty("favorite")]
		public bool IsFavorite { get; set; }

		/// <summary>
		///     The JQL (query) for the filter
		/// </summary>
		[JsonProperty("jql")]
		public string Jql { get; set; }

		/// <summary>
		///     Name for the filter
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///     User who owns the filter
		/// </summary>
		[JsonProperty("owner")]
		public User Owner { get; set; }

		/// <summary>
		///     The URL to show the search results
		/// </summary>
		[JsonProperty("searchUrl")]
		public Uri SearchUri { get; set; }

		/// <summary>
		///     The permissions for sharing
		/// </summary>
		[JsonProperty("sharePermissions")]
		public IList<SharePermission> SharePermissions { get; set; }

		/// <summary>
		///     The subscriptions for this filter
		/// </summary>
		[JsonProperty("subscriptions")]
		public Subscriptions Subscriptions { get; set; }

		/// <summary>
		///     The url to view the filter
		/// </summary>
		[JsonProperty("viewUrl")]
		public Uri ViewUri { get; set; }
	}
}