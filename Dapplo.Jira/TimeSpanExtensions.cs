//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Jira
// 
//  Dapplo.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;

#endregion

namespace Dapplo.Jira
{
	public static class TimeSpanExtensions
	{
		/// <summary>
		///     Create an increment from the timespan.
		///     increment has of (+/-)nn(y|M|w|d|h|m)
		///     If the plus/minus(+/-) sign is omitted, plus is assumed.
		///     nn: number; y: year, M: month; w: week; d: day; h: hour; m: minute.
		/// </summary>
		/// <param name="timeSpan">TimeSpan to convert</param>
		/// <returns>string</returns>
		public static string TimeSpanToIncrement(this TimeSpan? timeSpan)
		{
			if (!timeSpan.HasValue)
			{
				return "";
			}
			if (timeSpan.Value.TotalMilliseconds < 0)
			{
				return $"-{timeSpan.TimeSpanToIncrement()}";
			}
			return $"{timeSpan.TimeSpanToIncrement()}";
		}

		/// <summary>
		///     Create an increment from the timespan.
		///     increment has of (+/-)nn(y|M|w|d|h|m)
		///     If the plus/minus(+/-) sign is omitted, plus is assumed.
		///     nn: number; y: year, M: month; w: week; d: day; h: hour; m: minute.
		/// </summary>
		/// <param name="timeSpan">TimeSpan to convert</param>
		/// <returns>string</returns>
		public static string TimeSpanToJiraTime(this TimeSpan? timeSpan)
		{
			if (!timeSpan.HasValue)
			{
				return "";
			}
			return timeSpan.Value.TimeSpanToJiraTime();
		}

		/// <summary>
		///     Create an jira time range.
		///     (+/-)nn(y|M|w|d|h|m)
		///     nn: number; y: year, M: month; w: week; d: day; h: hour; m: minute.
		/// </summary>
		/// <param name="timeSpan">TimeSpan to convert</param>
		/// <returns>string</returns>
		public static string TimeSpanToJiraTime(this TimeSpan timeSpan)
		{
			var days = timeSpan.TotalDays;
			if ((days > double.Epsilon || days < double.Epsilon) && days % 1 < double.Epsilon)
			{
				return $"\"{days}d\"";
			}
			var hours = timeSpan.TotalHours;
			if ((hours > double.Epsilon || hours < double.Epsilon) && hours % 1 < double.Epsilon)
			{
				return $"\"{hours}h\"";
			}
			return $"\"{(int) timeSpan.TotalMinutes}m\"";
		}
	}
}