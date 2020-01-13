// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Container for the fields of an agile issue
	/// </summary>
	[JsonObject]
	public class AgileIssueFields : IssueFields
	{
		/// <summary>
		///     The closed sprint information
		/// </summary>
		[JsonProperty("closedSprints")]
		public IList<Sprint> ClosedSprints { get; set; }
	}
}