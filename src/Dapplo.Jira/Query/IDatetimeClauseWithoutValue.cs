// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2019 Dapplo
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

using System;

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     An interface for a date time calculations clause
	/// </summary>
	public interface IDatetimeClauseWithoutValue
	{
		/// <summary>
		///     Specify a DateTime to compare against
		/// </summary>
		/// <param name="dateTime">DateTime</param>
		/// <returns>this</returns>
		IFinalClause DateTime(DateTime dateTime);

		/// <summary>
		///     Use the endOfDay function as the value to compare
		/// </summary>
		/// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
		/// <returns>this</returns>
		IFinalClause EndOfDay(TimeSpan? timeSpan = null);

		/// <summary>
		///     Use the endOfMonth function as the value to compare
		/// </summary>
		/// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
		/// <returns>this</returns>
		IFinalClause EndOfMonth(TimeSpan? timeSpan = null);

		/// <summary>
		///     Use the endOfWeek function as the value to compare
		/// </summary>
		/// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
		/// <returns>this</returns>
		IFinalClause EndOfWeek(TimeSpan? timeSpan = null);

		/// <summary>
		///     Use the endOfYear function as the value to compare
		/// </summary>
		/// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
		/// <returns>this</returns>
		IFinalClause EndOfYear(TimeSpan? timeSpan = null);

		/// <summary>
		///     Use the startOfDay function as the value to compare
		/// </summary>
		/// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
		/// <returns>this</returns>
		IFinalClause StartOfDay(TimeSpan? timeSpan = null);

		/// <summary>
		///     Use the startOfMonth function as the value to compare
		/// </summary>
		/// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
		/// <returns>this</returns>
		IFinalClause StartOfMonth(TimeSpan? timeSpan = null);

		/// <summary>
		///     Use the startOfWeek function as the value to compare
		/// </summary>
		/// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
		/// <returns>this</returns>
		IFinalClause StartOfWeek(TimeSpan? timeSpan = null);

		/// <summary>
		///     Use the startOfYear function as the value to compare
		/// </summary>
		/// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
		/// <returns>this</returns>
		IFinalClause StartOfYear(TimeSpan? timeSpan = null);
	}
}