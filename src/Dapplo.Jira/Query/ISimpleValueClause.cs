// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Query;

/// <summary>
///     An interface for a date time calculations clause
/// </summary>
public interface ISimpleValueClause
{
    /// <summary>
    ///     Negates the expression
    /// </summary>
    ISimpleValueClause Not { get; }

    /// <summary>
    ///     This allows fluent constructs like Space.In("DEV", "PRODUCTION")
    /// </summary>
    IFinalClause In(params string[] values);

    /// <summary>
    ///     This allows fluent constructs like Space.Is("DEV")
    /// </summary>
    IFinalClause Is(string value);
}