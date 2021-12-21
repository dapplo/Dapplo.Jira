// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Query;

/// <summary>
///     A clause for simple values like ancestor, Id, label, space and more
/// </summary>
public class TextClause : ITextClause
{
    private readonly Clause clause;
    private bool negate;

    internal TextClause(Fields textField)
    {
        this.clause = new Clause
        {
            Field = textField
        };
    }


    /// <inheritDoc />
    public ITextClause Not
    {
        get
        {
            this.negate = !this.negate;
            return this;
        }
    }

    /// <inheritDoc />
    public IFinalClause Contains(string value)
    {
        this.clause.Operator = Operators.Contains;
        this.clause.Value = $"\"{value}\"";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }
}
