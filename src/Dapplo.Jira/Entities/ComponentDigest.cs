// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Component information (digest)
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/component
/// </summary>
[JsonObject]
public class ComponentDigest : BaseProperties<long>
{
    /// <summary>
    /// Is the assignee type valid?
    ///     TODO: Needs comment
    /// </summary>
    [JsonProperty("isAssigneeTypeValid")]
    public bool IsAssigneeTypeValid { get; set; }
}