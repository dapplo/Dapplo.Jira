// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Column config for the Board configuration
/// </summary>
public class ColumnConfig
{
    /// <summary>
    ///     Columns for the Board configuration
    /// </summary>
    [JsonPropertyName("columns")]
    public IList<Column> Columns { get; set; }

    /// <summary>
    ///     ConstraintType for the Board configuration, whatever this means
    /// </summary>
    [JsonPropertyName("constraintType")]
    public string ConstraintType { get; set; }
}