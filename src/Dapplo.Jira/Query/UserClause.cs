// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Query;

/// <summary>
/// This is the implementation of all user based where clauses
/// </summary>
public class UserClause : IUserClause
{
    private readonly Fields[] allowedFields =
    {
        Fields.Approvals, Fields.Assignee, Fields.Creator, Fields.Reporter, Fields.Voter, Fields.Watcher, Fields.WorkLogAuthor
    };

    private readonly Clause clause;
    private bool negate;

    internal UserClause(Fields userField)
    {
        if (this.allowedFields.All(field => userField != field))
        {
            throw new InvalidOperationException("Can't add function for the field {Field}");
        }

        this.clause = new Clause
        {
            Field = userField
        };
    }


    /// <inheritDoc />
    public IFinalClause IsCurrentUser
    {
        get
        {
            this.clause.Value = "currentUser()";
            if (this.negate)
            {
                this.clause.Negate();
            }

            return this.clause;
        }
    }

    /// <inheritDoc />
    public IUserClause Not
    {
        get
        {
            this.negate = !this.negate;
            return this;
        }
    }

    /// <inheritDoc />
    public IFinalClause Is(string user)
    {
        this.clause.Operator = Operators.EqualTo;
        this.clause.Value = $"\"{user}\"";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause Is(User user) => Is(user.Name);

    /// <inheritDoc />
    public IFinalClause In(params string[] users)
    {
        this.clause.Operator = Operators.In;
        this.clause.Value = "(" + string.Join(", ", users.Select(user => $"\"{user}\"")) + ")";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause In(params User[] users) => In(users.Select(user => user.Name).ToArray());

    /// <inheritDoc />
    public IFinalClause InCurrentUserAnd(params string[] users)
    {
        this.clause.Operator = Operators.In;
        this.clause.Value = "(currentUser(), " + string.Join(", ", users.Select(user => $"\"{user}\"")) + ")";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause InCurrentUserAnd(params User[] users) => InCurrentUserAnd(users.Select(user => user.Name).ToArray());
}
