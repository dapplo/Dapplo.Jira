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
	public class IssueWithFields<TFields> : IssueBase where TFields : IssueFields
	{
		/// <summary>
		///     Fields for the issue
		/// </summary>
		[JsonProperty("fields")]
		public TFields Fields { get; set; }

		/// <summary>
		///     Fields for the issue, but wiki markup is now rendered to HTML
		///     This will be in the response when expand=renderedFields
		/// </summary>
		[JsonProperty("renderedFields")]
		public TFields RenderedFields { get; set; }
	}
}