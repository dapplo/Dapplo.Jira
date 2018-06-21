using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	[JsonObject]
	public class History : BaseProperties<long>
	{
		/// <summary>
		///     Who created the comment
		/// </summary>
		[JsonProperty("author")]
		[ReadOnly(true)]
		public User Author { get; set; }

		/// <summary>
		///     When was the comment created
		/// </summary>
		[JsonProperty("created")]
		[ReadOnly(true)]
		public DateTimeOffset? Created { get; set; }

		/// <summary>
		///     list of fields that have been changed during this operation
		/// </summary>
		/// <value>
		///     The items.
		/// </value>
		[JsonProperty("items")]
		[ReadOnly(true)]
		public IList<HistoryItem> Items { get; set; }
	}
}