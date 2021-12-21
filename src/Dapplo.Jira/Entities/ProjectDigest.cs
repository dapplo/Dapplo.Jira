// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Project information (digest)
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/project
/// </summary>
[JsonObject]
public class ProjectDigest : BaseProperties<long>
{
    /// <summary>
    ///     Avatar for this project
    /// </summary>
    [JsonProperty("avatarUrls")]
    public AvatarUrls Avatar { get; set; }

    /// <summary>
    ///     The projects category
    /// </summary>
    [JsonProperty("projectCategory")]
    public ProjectCategory Category { get; set; }

    /// <summary>
    ///     Key for this project (the prefix of the issues in the project)
    /// </summary>
    [JsonProperty("key")]
    public string Key { get; set; }

    /// <summary>
    ///     User who is the lead for the project
    /// </summary>
    [JsonProperty("lead")]
    public User Lead { get; set; }

    /// <summary>
    ///     Name of the project
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     All project keys associated with the project
    /// </summary>
    [JsonProperty("projectKeys")]
    public IList<string> ProjectKeys { get; set; }
}