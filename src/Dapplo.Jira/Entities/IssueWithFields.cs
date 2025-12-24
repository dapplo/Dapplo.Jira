// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Issue information
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/issue
/// </summary>
public class IssueWithFields<TFields> : IssueBase where TFields : IssueFields
{
    /// <summary>
    ///     Fields for the issue
    /// </summary>
    [JsonPropertyName("fields")]
    public TFields Fields { get; set; }

    /// <summary>
    ///     Fields for the issue, but wiki markup is now rendered to HTML
    ///     This will be in the response when expand=renderedFields
    /// </summary>
    [JsonPropertyName("renderedFields")]
    public RenderedIssueFields RenderedFields { get; set; }

    /// <summary>
    ///		List of operations to perform on issue screen fields.
    ///		Note that fields included in here cannot be included in fields.
    /// </summary>
    [JsonPropertyName("update")]
    public IssueEdit Edit { get; set; }
}