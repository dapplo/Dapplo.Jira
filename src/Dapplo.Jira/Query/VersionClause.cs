// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Query;

/// <summary>
///     A clause for version values like fixVersion and more
/// </summary>
public class VersionClause : IVersionClause
{
    private readonly Clause clause;

    private bool negate;

    /// <summary>
    /// Constructor of a version clause with a field
    /// </summary>
    /// <param name="versionField">Fields</param>
    public VersionClause(Fields versionField)
    {
        this.clause = new Clause
        {
            Field = versionField
        };
    }

    /// <inheritDoc />
    public IVersionClause Not
    {
        get
        {
            this.negate = !this.negate;
            return this;
        }
    }

    /// <inheritDoc />
    public IFinalClause Is(string version)
    {
        this.clause.Operator = Operators.EqualTo;
        this.clause.Value = $"\"{version}\"";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause In(params string[] versions)
    {
        this.clause.Operator = Operators.In;
        this.clause.Value = "(" + string.Join(", ", versions.Select(version => $"\"{version}\"")) + ")";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause InReleasedVersions(string project = null)
    {
        this.clause.Operator = Operators.In;
        this.clause.Value = $"releasedVersions({project})";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause InLatestReleasedVersion(string project)
    {
        this.clause.Operator = Operators.In;
        this.clause.Value = $"latestReleasedVersion({project})";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }

    /// <inheritDoc />
    public IFinalClause InUnreleasedVersions(string project = null)
    {
        this.clause.Operator = Operators.In;
        this.clause.Value = $"unreleasedVersions({project})";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }


    /// <inheritDoc />
    public IFinalClause InEarliestUnreleasedVersion(string project)
    {
        this.clause.Operator = Operators.In;
        this.clause.Value = $"earliestUnreleasedVersion({project})";
        if (this.negate)
        {
            this.clause.Negate();
        }

        return this.clause;
    }
}
