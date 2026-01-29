// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Represents an actor (user or group) in a project role
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/project/{projectIdOrKey}/role
/// </summary>
[JsonObject]
public class RoleActor : BaseProperties<long>
{
    /// <summary>
    ///     Display name of the actor
    /// </summary>
    [JsonProperty("displayName")]
    public string DisplayName { get; set; }

    /// <summary>
    ///     Type of the actor (atlassian-user-role-actor, atlassian-group-role-actor)
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    ///     Name of the actor
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Avatar URLs for the actor
    /// </summary>
    [JsonProperty("avatarUrl")]
    public Uri AvatarUrl { get; set; }

    /// <summary>
    ///     Actor user (when type is atlassian-user-role-actor)
    /// </summary>
    [JsonProperty("actorUser")]
    public User ActorUser { get; set; }

    /// <summary>
    ///     Actor group (when type is atlassian-group-role-actor)
    /// </summary>
    [JsonProperty("actorGroup")]
    public Group ActorGroup { get; set; }
}
