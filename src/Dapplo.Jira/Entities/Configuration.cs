// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Comment information
	///     See
	///     <a href="https://docs.atlassian.com/jira/REST/cloud/#api/2/configuration-getConfiguration">get configuration</a>
	/// </summary>
	[JsonObject]
	public class Configuration
	{
		/// <summary>
		///     Is voting enabled?
		/// </summary>
		[JsonProperty("votingEnabled")]
		public bool IsVotingEnabled { get; set; }

		/// <summary>
		///     Is watching enabled?
		/// </summary>
		[JsonProperty("watchingEnabled")]
		public bool IsWatchingEnabled { get; set; }

		/// <summary>
		///     Are unassigned issues allowed?
		/// </summary>
		[JsonProperty("unassignedIssuesAllowed")]
		public bool AreUnassignedIssuesAllowed { get; set; }

		/// <summary>
		///     Are sub-tasks enabled?
		/// </summary>
		[JsonProperty("subTasksEnabled")]
		public bool AreSubTasksEnabled { get; set; }

		/// <summary>
		///     Is issue linking enabled?
		/// </summary>
		[JsonProperty("issueLinkingEnabled")]
		public bool IsIssueLinkingEnabled { get; set; }

		/// <summary>
		///     Is time tracking enabled?
		/// </summary>
		[JsonProperty("timeTrackingEnabled")]
		public bool IsTimeTrackingEnabled { get; set; }

		/// <summary>
		///     Are attachments enabled?
		/// </summary>
		[JsonProperty("attachmentsEnabled")]
		public bool AreAttachmentsEnabled { get; set; }

		/// <summary>
		///     The configuration of the time tracking
		/// </summary>
		[JsonProperty("timeTrackingConfiguration")]
		public TimeTrackingConfiguration TimeTrackingConfiguration { get; set; }
	}
}