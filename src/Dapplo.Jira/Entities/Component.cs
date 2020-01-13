// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Component information, retrieved for /component/id
	///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/component
	/// </summary>
	[JsonObject]
	public class Component : ComponentDigest
	{
		/// <summary>
		///     TODO: Needs comment
		/// </summary>
		[JsonProperty("assignee")]
		public User Assignee { get; set; }

		/// <summary>
		///     TODO: Needs comment
		/// </summary>
		[JsonProperty("assigneeType")]
		public string AssigneeType { get; set; }

		/// <summary>
		///     Description of this component
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>
		///     Lead for this component
		/// </summary>
		[JsonProperty("lead")]
		public User Lead { get; set; }

		/// <summary>
		///     Name of the component
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///     Project key where this component belongs to
		/// </summary>
		[JsonProperty("project")]
		public string Project { get; set; }

		/// <summary>
		///     Id of the project where this component belongs to
		/// </summary>
		[JsonProperty("projectId")]
		public int ProjectId { get; set; }

		/// <summary>
		///     TODO: Needs comment
		/// </summary>
		[JsonProperty("realAssignee")]
		public User RealAssignee { get; set; }

		/// <summary>
		///     TODO: Needs comment
		/// </summary>
		[JsonProperty("realAssigneeType")]
		public string RealAssigneeType { get; set; }
	}
}