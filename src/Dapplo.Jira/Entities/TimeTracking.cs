// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Timetracking information
	/// </summary>
	[JsonObject]
	public class TimeTracking
	{
		/// <summary>
		///     The originaly estimated time for this issue
		/// </summary>
		[JsonProperty("originalEstimate")]
		public string OriginalEstimate { get; set; }

		/// <summary>
		///     The originaly estimated time for this issue
		/// </summary>
		[JsonProperty("originalEstimateSeconds")]
		public long? OriginalEstimateSeconds { get; set; }

		/// <summary>
		///     The remaining estimated time for this issue
		/// </summary>
		[JsonProperty("remainingEstimate")]
		public string RemainingEstimate { get; set; }


		/// <summary>
		///     The remaining estimated time, in seconds, for this issue
		/// </summary>
		[JsonProperty("remainingEstimateSeconds")]
		public long? RemainingEstimateSeconds { get; set; }

		/// <summary>
		///     Time spent in form of "4w 4d 2h"
		/// </summary>
		[JsonProperty("timeSpent")]
		public string TimeSpent { get; set; }

		/// <summary>
		///     Time spent in seconds
		/// </summary>
		[JsonProperty("timeSpentSeconds")]
		public long? TimeSpentSeconds { get; set; }
	}
}