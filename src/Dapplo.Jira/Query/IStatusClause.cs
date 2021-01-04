// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Query
{
    /// <summary>
    ///     An interface for status based clauses
    /// </summary>
    public interface IStatusClause
    {
        /// <summary>
        ///     Negates the expression
        /// </summary>
        IStatusClause Not { get; }

        /// <summary>
        ///     This allows fluent constructs like IssueKey.In(BUG-1234, FEATURE-5678)
        /// </summary>
        IFinalClause In(params string[] states);

        /// <summary>
        ///     This allows fluent constructs like Id.Is(12345)
        /// </summary>
        IFinalClause Is(string state);

        /// <summary>
        ///     This allows fluent constructs like Status.Was("Closed")
        /// TODO: Add the different predicates like after and before etc... See: <a href="https://confluence.atlassian.com/jira/advanced-searching-179442050.html#id-__JQLWAScaveats-Status">here</a>
        /// </summary>
        IFinalClause Was(string state);
    }
}
