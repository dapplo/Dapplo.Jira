// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Query;

/// <summary>
///     A clause for date time calculations
/// </summary>
public class DatetimeClause : IDatetimeClause, IDatetimeClauseWithoutValue
{
    private readonly Clause clause;

    internal DatetimeClause(Fields datetimeField)
    {
        this.clause = new Clause
        {
            Field = datetimeField
        };
    }

    /// <inheritDoc />
    public IDatetimeClauseWithoutValue On
    {
        get
        {
            this.clause.Operator = Operators.EqualTo;
            return this;
        }
    }

    /// <inheritDoc />
    public IDatetimeClauseWithoutValue Before
    {
        get
        {
            this.clause.Operator = Operators.LessThan;
            return this;
        }
    }

    /// <inheritDoc />
    public IDatetimeClauseWithoutValue BeforeOrOn
    {
        get
        {
            this.clause.Operator = Operators.LessThanEqualTo;
            return this;
        }
    }

    /// <inheritDoc />
    public IDatetimeClauseWithoutValue After
    {
        get
        {
            this.clause.Operator = Operators.GreaterThan;
            return this;
        }
    }

    /// <inheritDoc />
    public IDatetimeClauseWithoutValue AfterOrOn
    {
        get
        {
            this.clause.Operator = Operators.GreaterThanEqualTo;
            return this;
        }
    }

    /// <inheritDoc />
    public IFinalClause DateTime(DateTimeOffset dateTime)
    {
        if (dateTime.Minute == 0 && dateTime.Hour == 0)
        {
            this.clause.Value = $"\"{dateTime:yyyy-MM-dd}\"";
        }
        else
        {
            this.clause.Value = $"\"{dateTime:yyyy-MM-dd HH:mm}\"";
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause EndOfDay(TimeSpan? timeSpan = null)
    {
        this.clause.Value = $"endOfDay({timeSpan.TimeSpanToIncrement()})";
        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause EndOfMonth(TimeSpan? timeSpan = null)
    {
        this.clause.Value = $"endOfMonth({timeSpan.TimeSpanToIncrement()})";
        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause EndOfWeek(TimeSpan? timeSpan = null)
    {
        this.clause.Value = $"endOfWeek({timeSpan.TimeSpanToIncrement()})";
        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause EndOfYear(TimeSpan? timeSpan = null)
    {
        this.clause.Value = $"endOfYear({timeSpan.TimeSpanToIncrement()})";
        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause StartOfDay(TimeSpan? timeSpan = null)
    {
        this.clause.Value = $"startOfDay({timeSpan.TimeSpanToIncrement()})";
        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause StartOfMonth(TimeSpan? timeSpan = null)
    {
        this.clause.Value = $"startOfMonth({timeSpan.TimeSpanToIncrement()})";
        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause StartOfWeek(TimeSpan? timeSpan = null)
    {
        this.clause.Value = $"startOfWeek({timeSpan.TimeSpanToIncrement()})";
        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause StartOfYear(TimeSpan? timeSpan = null)
    {
        this.clause.Value = $"startOfYear({timeSpan.TimeSpanToIncrement()})";
        return this.clause;
    }
}
