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

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     An interface for a date time calculations clause
	/// </summary>
	public interface IDatetimeClause
	{
		IDatetimeClauseWithoutValue After { get; }
		IDatetimeClauseWithoutValue AfterOrOn { get; }
		IDatetimeClauseWithoutValue Before { get; }
		IDatetimeClauseWithoutValue BeforeOrOn { get; }
		IDatetimeClauseWithoutValue On { get; }
	}

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

	/// <summary>
	///     A clause for date time calculations
	/// </summary>
	public class DatetimeClause : IDatetimeClause, IDatetimeClauseWithoutValue
	{
		private readonly Clause _clause;

		internal DatetimeClause(Fields datetimeField)
		{
			_clause = new Clause
			{
				Field = datetimeField
			};
		}

		/// <inheritDoc />
		public IDatetimeClauseWithoutValue On
		{
			get
			{
				_clause.Operator = Operators.EqualTo;
				return this;
			}
		}

		/// <inheritDoc />
		public IDatetimeClauseWithoutValue Before
		{
			get
			{
				_clause.Operator = Operators.LessThan;
				return this;
			}
		}

		/// <inheritDoc />
		public IDatetimeClauseWithoutValue BeforeOrOn
		{
			get
			{
				_clause.Operator = Operators.LessThanEqualTo;
				return this;
			}
		}

		/// <inheritDoc />
		public IDatetimeClauseWithoutValue After
		{
			get
			{
				_clause.Operator = Operators.GreaterThan;
				return this;
			}
		}

		/// <inheritDoc />
		public IDatetimeClauseWithoutValue AfterOrOn
		{
			get
			{
				_clause.Operator = Operators.GreaterThanEqualTo;
				return this;
			}
		}

		/// <inheritDoc />
		public IFinalClause DateTime(DateTime dateTime)
		{
			if (dateTime.Minute == 0 && dateTime.Hour == 0)
			{
				_clause.Value = $"\"{dateTime:yyyy-MM-dd}\"";
			}
			else
			{
				_clause.Value = $"\"{dateTime:yyyy-MM-dd HH-mm}\"";
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause EndOfDay(TimeSpan? timeSpan = null)
		{
			_clause.Value = $"endOfDay({TimeSpanToIncrement(timeSpan)})";
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause EndOfMonth(TimeSpan? timeSpan = null)
		{
			_clause.Value = $"endOfMonth({TimeSpanToIncrement(timeSpan)})";
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause EndOfWeek(TimeSpan? timeSpan = null)
		{
			_clause.Value = $"endOfWeek({TimeSpanToIncrement(timeSpan)})";
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause EndOfYear(TimeSpan? timeSpan = null)
		{
			_clause.Value = $"endOfYear({TimeSpanToIncrement(timeSpan)})";
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause StartOfDay(TimeSpan? timeSpan = null)
		{
			_clause.Value = $"startOfDay({TimeSpanToIncrement(timeSpan)})";
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause StartOfMonth(TimeSpan? timeSpan = null)
		{
			_clause.Value = $"startOfMonth({TimeSpanToIncrement(timeSpan)})";
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause StartOfWeek(TimeSpan? timeSpan = null)
		{
			_clause.Value = $"startOfWeek({TimeSpanToIncrement(timeSpan)})";
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause StartOfYear(TimeSpan? timeSpan = null)
		{
			_clause.Value = $"startOfYear({TimeSpanToIncrement(timeSpan)})";
			return _clause;
		}

		/// <summary>
		///     Create an increment from the timespan.
		///     increment has of (+/-)nn(y|M|w|d|h|m)
		///     If the plus/minus(+/-) sign is omitted, plus is assumed.
		///     nn: number; y: year, M: month; w: week; d: day; h: hour; m: minute.
		/// </summary>
		/// <param name="timeSpan">TimeSpan to convert</param>
		/// <returns>string</returns>
		private static string TimeSpanToIncrement(TimeSpan? timeSpan = null)
		{
			if (!timeSpan.HasValue)
			{
				return "";
			}
			var increment = timeSpan.Value;
			var days = increment.TotalDays;
			if ((days > double.Epsilon || days < double.Epsilon) && days % 1 < double.Epsilon)
			{
				return $"\"{days}d\"";
			}
			var hours = increment.TotalHours;
			if ((hours > double.Epsilon || hours < double.Epsilon) && hours % 1 < double.Epsilon)
			{
				return $"\"{hours}h\"";
			}
			return $"\"{(int) timeSpan.Value.TotalMinutes}m\"";
		}
	}
}