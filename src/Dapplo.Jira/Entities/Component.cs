// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Component information, retrieved for /component/id
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/component
/// </summary>
public class Component : ComponentDigest
{
    /// <summary>
    ///     TODO: Needs comment
    /// </summary>
    [JsonPropertyName("assignee")]
    public User Assignee { get; set; }

    /// <summary>
    ///     TODO: Needs comment
    /// </summary>
    [JsonPropertyName("assigneeType")]
    public string AssigneeType { get; set; }

    /// <summary>
    ///     Description of this component
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Lead for this component
    /// </summary>
    [JsonPropertyName("lead")]
    public User Lead { get; set; }

    /// <summary>
    ///     Name of the component
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Project key where this component belongs to
    /// </summary>
    [JsonPropertyName("project")]
    public string Project { get; set; }

    /// <summary>
    ///     Id of the project where this component belongs to
    /// </summary>
    [JsonPropertyName("projectId")]
    public int ProjectId { get; set; }

    /// <summary>
    ///     TODO: Needs comment
    /// </summary>
    [JsonPropertyName("realAssignee")]
    public User RealAssignee { get; set; }

    /// <summary>
    ///     TODO: Needs comment
    /// </summary>
    [JsonPropertyName("realAssigneeType")]
    public string RealAssigneeType { get; set; }
}