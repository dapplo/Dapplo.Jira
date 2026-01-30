// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Project role information
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/project/{projectIdOrKey}/role
/// </summary>
[JsonObject]
public class ProjectRole : BaseProperties<long>
{
    /// <summary>
    ///     Name of the project role
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Description of the project role
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Actors assigned to this role
    /// </summary>
    [JsonProperty("actors")]
    public IList<RoleActor> Actors { get; set; }

    /// <summary>
    ///     Scope of the role
    /// </summary>
    [JsonProperty("scope")]
    public RoleScope Scope { get; set; }
}
