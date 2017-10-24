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

using Newtonsoft.Json;

#endregion

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