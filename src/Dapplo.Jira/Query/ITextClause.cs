// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Query;

/// <summary>
///     An interface for a date time calculations clause
/// </summary>
public interface ITextClause
{
    /// <summary>
    ///     Negates the expression
    /// </summary>
    ITextClause Not { get; }

    /// <summary>
    ///     This allows fluent constructs like Text.Contains(customernumber)
    /// </summary>
    IFinalClause Contains(string value);
}