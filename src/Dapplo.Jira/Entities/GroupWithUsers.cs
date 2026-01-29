// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Group with users information
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/group
/// </summary>
[JsonObject]
public class GroupWithUsers : PageableResult
{
    /// <summary>
    ///     Name of the group
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Link to this item (self)
    /// </summary>
    [JsonProperty("self")]
    public Uri Self { get; set; }

    /// <summary>
    ///     Users in this group
    /// </summary>
    [JsonProperty("users")]
    public PagedUsers Users { get; set; }

    /// <summary>
    ///     Expand options
    /// </summary>
    [JsonProperty("expand")]
    public string Expand { get; set; }
}

/// <summary>
///     Paged users within a group
/// </summary>
[JsonObject]
public class PagedUsers : PageableResult
{
    /// <summary>
    ///     The list of users
    /// </summary>
    [JsonProperty("items")]
    public IList<User> Items { get; set; }
}
