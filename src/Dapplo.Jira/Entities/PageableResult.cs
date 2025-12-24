// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Container for pageable information in a result
/// </summary>
public class PageableResult : Page
{
    /// <summary>
    ///     The total results
    /// </summary>
    [JsonPropertyName("total")]
    public int? Total { get; set; }

    /// <summary>
    ///     Specifies if there are more results
    /// </summary>
    [JsonPropertyName("isLast")]
    public bool IsLastPage { get; set; }
}