// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Group information
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/group
/// </summary>
[JsonObject]
public class Group
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
}
