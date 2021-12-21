// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Query;

/// <summary>
///     A clause for simple values like container, macro and label
/// </summary>
public class SimpleValueClause : ISimpleValueClause
{
    private readonly Clause clause;
    private bool negate;

    internal SimpleValueClause(Fields simpleField)
    {
        this.clause = new Clause
        {
            Field = simpleField
        };
    }


    /// <inheritDoc />
    public ISimpleValueClause Not
    {
        get
        {
            this.negate = !this.negate;
            return this;
        }
    }

    /// <inheritDoc />
    public IFinalClause Is(string value)
    {
        this.clause.Operator = Operators.EqualTo;
        this.clause.Value = $"\"{value}\"";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause In(params string[] values)
    {
        this.clause.Operator = Operators.In;
        this.clause.Value = "(" + string.Join(", ", values.Select(value => $"\"{value}\"")) + ")";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }
}
