// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dapplo.Jira.Query
{
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
				_clause.Value = $"\"{dateTime:yyyy-MM-dd HH:mm}\"";
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause EndOfDay(TimeSpan? timeSpan = null)
		{
			_clause.Value = $"endOfDay({timeSpan.TimeSpanToIncrement()})";
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause EndOfMonth(TimeSpan? timeSpan = null)
		{
			_clause.Value = $"endOfMonth({timeSpan.TimeSpanToIncrement()})";
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause EndOfWeek(TimeSpan? timeSpan = null)
		{
			_clause.Value = $"endOfWeek({timeSpan.TimeSpanToIncrement()})";
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause EndOfYear(TimeSpan? timeSpan = null)
		{
			_clause.Value = $"endOfYear({timeSpan.TimeSpanToIncrement()})";
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause StartOfDay(TimeSpan? timeSpan = null)
		{
			_clause.Value = $"startOfDay({timeSpan.TimeSpanToIncrement()})";
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause StartOfMonth(TimeSpan? timeSpan = null)
		{
			_clause.Value = $"startOfMonth({timeSpan.TimeSpanToIncrement()})";
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause StartOfWeek(TimeSpan? timeSpan = null)
		{
			_clause.Value = $"startOfWeek({timeSpan.TimeSpanToIncrement()})";
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause StartOfYear(TimeSpan? timeSpan = null)
		{
			_clause.Value = $"startOfYear({timeSpan.TimeSpanToIncrement()})";
			return _clause;
		}
	}
}