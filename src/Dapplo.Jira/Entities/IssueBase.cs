// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Issue information
	///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/issue
	/// </summary>
	[JsonObject]
	public class IssueBase : BaseProperties<string>
	{
		/// <summary>
		///     Key of the issue
		/// </summary>
		[JsonProperty("key")]
		public string Key { get; set; }

		/// <summary>
		///     Gets or sets the changelogs.
		/// </summary>
		/// <value>
		///     The changelogs.
		/// </value>
		[JsonProperty("changelog")]
		public Changelog Changelog { get; set; }
	}
}