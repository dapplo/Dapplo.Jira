// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Time tracking configuration
	/// </summary>
	[JsonObject]
	public class TimeTrackingConfiguration
	{
		/// <summary>
		///     The number of working hours per day
		/// </summary>
		[JsonProperty("workingHoursPerDay")]
		public float WorkingHoursPerDay { get; set; } = 8;

		/// <summary>
		///     The number of working days per week
		/// </summary>
		[JsonProperty("workingDaysPerWeek")]
		public float WorkingDaysPerWeek { get; set; } = 5;

		/// <summary>
		///     The time format used
		/// </summary>
		[JsonProperty("timeFormat")]
		public string TimeFormat { get; set; } = "pretty";

		/// <summary>
		///     The default unit
		/// </summary>
		[JsonProperty("defaultUnit")]
		public string DefaultUnit { get; set; } = "hour";
	}
}