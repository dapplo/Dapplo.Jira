// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Project information (retrieved via /project/id)
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/project
/// </summary>
[JsonObject]
public class Project : ProjectDigest
{
    /// <summary>
    ///     AssigneeType describes how the assignment of tickets works, if this says project-lead every ticket will be assigned
    ///     to the person which that role.
    /// </summary>
    [JsonProperty("assigneeType")]
    public string AssigneeType { get; set; }

    /// <summary>
    ///     Url to browse the tickets with
    /// </summary>
    [JsonProperty("url")]
    public Uri BrowseUrl { get; set; }

    /// <summary>
    ///     Components for this project, this is only a "digest" retrieve the component details for more information.
    /// </summary>
    [JsonProperty("components")]
    public IList<ComponentDigest> Components { get; set; }

    /// <summary>
    ///     The description of the project
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>
    ///     TODO: Uncertain what this is, please comment!
    /// </summary>
    [JsonProperty("email")]
    public string Email { get; set; }

    /// <summary>
    ///     Possible issue types for this project
    /// </summary>
    [JsonProperty("issueTypes")]
    public IList<IssueType> IssueTypes { get; set; }

    /// <summary>
    ///     Urls to the possible roles for this project
    /// </summary>
    [JsonProperty("roles")]
    public IDictionary<string, Uri> Roles { get; set; }

    /// <summary>
    ///     Possible versions for this project
    /// </summary>
    [JsonProperty("versions")]
    public IList<Version> Versions { get; set; }

    /// <summary>
    ///     The project type e.g. software, service_desk, business
    /// </summary>
    [JsonProperty("projectTypeKey")]
    public string ProjectTypeKey { get; set; }
}