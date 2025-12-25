// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Project information (retrieved via /project/id)
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/project
/// </summary>
public class Project : ProjectDigest
{
    /// <summary>
    ///     AssigneeType describes how the assignment of tickets works, if this says project-lead every ticket will be assigned
    ///     to the person which that role.
    /// </summary>
    [JsonPropertyName("assigneeType")]
    public string AssigneeType { get; set; }

    /// <summary>
    ///     Url to browse the tickets with
    /// </summary>
    [JsonPropertyName("url")]
    public Uri BrowseUrl { get; set; }

    /// <summary>
    ///     Components for this project, this is only a "digest" retrieve the component details for more information.
    /// </summary>
    [JsonPropertyName("components")]
    public IList<ComponentDigest> Components { get; set; }

    /// <summary>
    ///     The description of the project
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     TODO: Uncertain what this is, please comment!
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; }

    /// <summary>
    ///     Possible issue types for this project
    /// </summary>
    [JsonPropertyName("issueTypes")]
    public IList<IssueType> IssueTypes { get; set; }

    /// <summary>
    ///     Urls to the possible roles for this project
    /// </summary>
    [JsonPropertyName("roles")]
    public IDictionary<string, Uri> Roles { get; set; }

    /// <summary>
    ///     Possible versions for this project
    /// </summary>
    [JsonPropertyName("versions")]
    public IList<Version> Versions { get; set; }

    /// <summary>
    ///     The project type e.g. software, service_desk, business
    /// </summary>
    [JsonPropertyName("projectTypeKey")]
    public string ProjectTypeKey { get; set; }
}