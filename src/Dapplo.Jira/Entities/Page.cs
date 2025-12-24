// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Container for paga information in a request, also the base for the PageableResult
/// </summary>
public class Page
{
    /// <summary>
    ///     Max of the results (this is the limit)
    /// </summary>
    [JsonPropertyName("maxResults")]
    public int? MaxResults { get; set; }

    /// <summary>
    ///     Where in the total this "page" is located
    /// </summary>
    [JsonPropertyName("startAt")]
    public int? StartAt { get; set; }
}