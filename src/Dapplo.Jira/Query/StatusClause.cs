// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Query;

/// <summary>
///     A clause for checking the status
/// </summary>
public class StatusClause : IStatusClause
{
    private readonly Clause clause = new()
    {
        Field = Fields.Status
    };

    private bool negate;

    /// <inheritDoc />
    public IStatusClause Not
    {
        get
        {
            this.negate = !this.negate;
            return this;
        }
    }

    /// <inheritDoc />
    public IFinalClause Was(string state)
    {
        this.clause.Operator = Operators.Was;
        this.clause.Value = EscapeState(state);
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause Is(string state)
    {
        this.clause.Operator = Operators.EqualTo;
        this.clause.Value = EscapeState(state);
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause Is(Issue issue) => Is(issue.Key);

    /// <inheritDoc />
    public IFinalClause In(params string[] states)
    {
        this.clause.Operator = Operators.In;
        this.clause.Value = "(" + string.Join(", ", states.Select(EscapeState)) + ")";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    private string EscapeState(string state) => $"\"{state}\"";
}
