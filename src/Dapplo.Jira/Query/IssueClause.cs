// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Query;

/// <summary>
///     A clause for content identifying values like ancestor, content, id and parent
/// </summary>
public class IssueClause : IIssueClause
{
    private readonly Clause clause = new()
    {
        Field = Fields.IssueKey
    };

    private bool negate;

    /// <inheritDoc />
    public IIssueClause Not
    {
        get
        {
            this.negate = !this.negate;
            return this;
        }
    }

    /// <inheritDoc />
    public IFinalClause Is(string issueKey)
    {
        this.clause.Operator = Operators.EqualTo;
        this.clause.Value = issueKey;
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause Is(Issue issue) => Is(issue.Key);

    /// <inheritDoc />
    public IFinalClause In(params string[] issueKeys)
    {
        this.clause.Operator = Operators.In;
        this.clause.Value = "(" + string.Join(", ", issueKeys) + ")";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause In(params Issue[] issues) => In(issues.Select(issue => issue.Key).ToArray());

    /// <inheritDoc />
    public IFinalClause InIssueHistory()
    {
        this.clause.Operator = Operators.In;
        this.clause.Value = "issueHistory()";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause InLinkedIssues(string issueKey, string linkType = null)
    {
        this.clause.Operator = Operators.In;
        var linkTypeArgument = string.IsNullOrEmpty(linkType) ? "" : $", {linkType}";

        this.clause.Value = $"linkedIssues({issueKey}{linkTypeArgument})";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause InLinkedIssues(Issue issue, string linkType = null) => InLinkedIssues(issue.Key, linkType);

    /// <inheritDoc />
    public IFinalClause InVotedIssues()
    {
        this.clause.Operator = Operators.In;
        this.clause.Value = "votedIssues()";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause InWatchedIssues()
    {
        this.clause.Operator = Operators.In;
        this.clause.Value = "watchedIssues()";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }
}
