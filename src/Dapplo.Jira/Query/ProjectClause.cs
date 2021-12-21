// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Query;

/// <summary>
///     A clause for searching projects belonging to a project
/// </summary>
public class ProjectClause : IProjectClause
{
    private readonly Clause clause = new()
    {
        Field = Fields.Project
    };

    private bool negate;

    /// <inheritDoc />
    public IProjectClause Not
    {
        get
        {
            this.negate = !this.negate;
            return this;
        }
    }

    /// <inheritDoc />
    public IFinalClause Is(string projectKey)
    {
        this.clause.Operator = Operators.EqualTo;
        this.clause.Value = projectKey;
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause Is(Project project) => Is(project.Key);

    /// <inheritDoc />
    public IFinalClause In(params string[] projectKeys)
    {
        this.clause.Operator = Operators.In;
        this.clause.Value = "(" + string.Join(", ", projectKeys) + ")";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause In(params Project[] projects) => In(projects.Select(project => project.Key).ToArray());

    /// <inheritDoc />
    public IFinalClause InProjectsLeadByUser() => InFunction("projectsLeadByUser()");

    /// <inheritDoc />
    public IFinalClause InProjectsWhereUserHasPermission() => InFunction("projectsWhereUserHasPermission()");

    /// <inheritDoc />
    public IFinalClause InProjectsWhereUserHasRole() => InFunction("projectsWhereUserHasRole()");

    /// <summary>
    /// Create clause for a function
    /// </summary>
    /// <param name="functionName">Name of the function</param>
    /// <returns>IFinalClause</returns>
    private IFinalClause InFunction(string functionName)
    {
        this.clause.Operator = Operators.In;
        this.clause.Value = functionName;
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }
}
