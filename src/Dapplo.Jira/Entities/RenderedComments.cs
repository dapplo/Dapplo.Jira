// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Comment information
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/attachment
/// </summary>
public class RenderedComments : PageableResult
{
    /// <summary>
    ///     The actual commits
    /// </summary>
    [JsonPropertyName("comments")]
    public IList<RenderedComment> Elements { get; set; }
}