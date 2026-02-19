// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Comment information
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/attachment
/// </summary>
[JsonObject]
public class CommentsV2 : PageableResult
{
    /// <summary>
    ///     The actual commits
    /// </summary>
    [JsonProperty("comments")]
    public IList<CommentV2> Elements { get; set; }
}
