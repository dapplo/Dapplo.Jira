// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Worklog information
	/// </summary>
	[JsonObject]
	public class Worklogs : PageableResult, IEnumerable<Worklog>
	{
		/// <summary>
		///     The worklog items
		/// </summary>
		[JsonProperty("worklogs")]
		public IList<Worklog> Elements { get; set; }

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// IEnumerator implementation
		/// </summary>
		/// <returns>IEnumerator of type TResultType</returns>
		public IEnumerator<Worklog> GetEnumerator()
		{
			return Elements.GetEnumerator();
		}
	}
}