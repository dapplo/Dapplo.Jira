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

using System.Runtime.Serialization;
using Newtonsoft.Json;

#endregion

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
		[DataMember(Name = "workingHoursPerDay")]
		public int WorkingHoursPerDay { get; set; } = 8;

		/// <summary>
		///     The number of working days per week
		/// </summary>
		[DataMember(Name = "workingDaysPerWeek")]
		public int WorkingDaysPerWeek { get; set; } = 5;

		/// <summary>
		///     The time format used
		/// </summary>
		[DataMember(Name = "timeFormat")]
		public string TimeFormat { get; set; } = "pretty";

		/// <summary>
		///     The default unit
		/// </summary>
		[DataMember(Name = "defaultUnit")]
		public string DefaultUnit { get; set; } = "hour";
	}
}