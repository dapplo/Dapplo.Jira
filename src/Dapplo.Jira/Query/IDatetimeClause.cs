// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Query
{
    /// <summary>
    ///     An interface for a date time calculations clause
    /// </summary>
    public interface IDatetimeClause
    {
        /// <summary>
        /// Is the queried timestamp after the specified timestamp?
        /// </summary>
        IDatetimeClauseWithoutValue After { get; }

        /// <summary>
        ///  Is the queried timestamp after or on the specified timestamp?
        /// </summary>
        IDatetimeClauseWithoutValue AfterOrOn { get; }

        /// <summary>
        ///  Is the queried timestamp before the specified timestamp?
        /// </summary>
        IDatetimeClauseWithoutValue Before { get; }

        /// <summary>
        ///  Is the queried timestamp before or on the specified timestamp?
        /// </summary>
        IDatetimeClauseWithoutValue BeforeOrOn { get; }

        /// <summary>
        ///  Is the queried timestamp the same as the specified timestamp?
        /// </summary>
        IDatetimeClauseWithoutValue On { get; }
    }
}
