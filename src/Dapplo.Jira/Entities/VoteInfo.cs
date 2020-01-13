// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Vote information
	/// </summary>
	[JsonObject]
	public class VoteInfo
	{
		/// <summary>
		///     Does the issue have votes?
		/// </summary>
		[JsonProperty("hasVoted")]
		public bool HasVoted { get; set; }

		/// <summary>
		///     Link to itself
		/// </summary>
		[JsonProperty("self")]
		public Uri Self { get; set; }

		/// <summary>
		///     Who are the voters
		/// </summary>
		[JsonProperty("voters")]
		public IList<User> Voters { get; set; }

		/// <summary>
		///     How many votes does it have
		/// </summary>
		[JsonProperty("votes")]
		public long? Votes { get; set; }
	}
}