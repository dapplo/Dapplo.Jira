// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Describes a link between Jira issues
/// </summary>
public class IssueLink : ReadOnlyBaseId<string>
{
    /// <summary>
    /// The comment for the issue link
    /// </summary>
    [JsonPropertyName("comment")]
    public Comment Comment { get; set; }

    /// <summary>
    ///
    /// </summary>
    [JsonPropertyName("type")]
    public IssueLinkType IssueLinkType { get; set; }

    /// <summary>
    /// This describes the issue where the issue link if pointing to
    /// </summary>
    [JsonPropertyName("outwardIssue")]
    public LinkedIssue OutwardIssue { get; set; }

    /// <summary>
    /// This describes the issue which is pointing to this
    /// </summary>
    [JsonPropertyName("inwardIssue")]
    public LinkedIssue InwardIssue { get; set; }
}