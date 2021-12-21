// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Query;

/// <summary>
/// Where clauses for the field type
/// </summary>
public class TypeClause : ITypeClause
{
    private readonly Clause clause = new()
    {
        Field = Fields.Type
    };

    private bool negate;

    /// <inheritDoc />
    public ITypeClause Not
    {
        get
        {
            this.negate = !this.negate;
            return this;
        }
    }

    /// <inheritDoc />
    public IFinalClause In(params string[] types)
    {
        this.clause.Operator = Operators.In;
        this.clause.Value = "(" + string.Join(", ", types) + ")";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause In(params IssueType[] issueTypes) => In(issueTypes.Select(issueType => issueType.Id).ToArray());


    /// <inheritDoc />
    public IFinalClause Is(string type)
    {
        this.clause.Operator = Operators.EqualTo;
        this.clause.Value = type;
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause Is(IssueType type) => Is(type.Id);

    /// <inheritDoc />
    public IFinalClause In(params long[] issueTypeIds) => In(issueTypeIds.Select(issueTypeId => issueTypeId.ToString()).ToArray());

    /// <inheritDoc />
    public IFinalClause Is(long issueTypeId) => Is(issueTypeId.ToString());
}
