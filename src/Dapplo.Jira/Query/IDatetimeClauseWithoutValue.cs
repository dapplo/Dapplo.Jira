// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dapplo.Jira.Query
{
    /// <summary>
    ///     An interface for a date time calculations clause
    /// </summary>
    public interface IDatetimeClauseWithoutValue
    {
        /// <summary>
        ///     Specify a DateTime to compare against
        /// </summary>
        /// <param name="dateTime">DateTime</param>
        /// <returns>this</returns>
        IFinalClause DateTime(DateTimeOffset dateTime);

        /// <summary>
        ///     Use the endOfDay function as the value to compare
        /// </summary>
        /// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
        /// <returns>this</returns>
        IFinalClause EndOfDay(TimeSpan? timeSpan = null);

        /// <summary>
        ///     Use the endOfMonth function as the value to compare
        /// </summary>
        /// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
        /// <returns>this</returns>
        IFinalClause EndOfMonth(TimeSpan? timeSpan = null);

        /// <summary>
        ///     Use the endOfWeek function as the value to compare
        /// </summary>
        /// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
        /// <returns>this</returns>
        IFinalClause EndOfWeek(TimeSpan? timeSpan = null);

        /// <summary>
        ///     Use the endOfYear function as the value to compare
        /// </summary>
        /// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
        /// <returns>this</returns>
        IFinalClause EndOfYear(TimeSpan? timeSpan = null);

        /// <summary>
        ///     Use the startOfDay function as the value to compare
        /// </summary>
        /// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
        /// <returns>this</returns>
        IFinalClause StartOfDay(TimeSpan? timeSpan = null);

        /// <summary>
        ///     Use the startOfMonth function as the value to compare
        /// </summary>
        /// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
        /// <returns>this</returns>
        IFinalClause StartOfMonth(TimeSpan? timeSpan = null);

        /// <summary>
        ///     Use the startOfWeek function as the value to compare
        /// </summary>
        /// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
        /// <returns>this</returns>
        IFinalClause StartOfWeek(TimeSpan? timeSpan = null);

        /// <summary>
        ///     Use the startOfYear function as the value to compare
        /// </summary>
        /// <param name="timeSpan">optional TimeSpan to offset the comparison</param>
        /// <returns>this</returns>
        IFinalClause StartOfYear(TimeSpan? timeSpan = null);
    }
}
