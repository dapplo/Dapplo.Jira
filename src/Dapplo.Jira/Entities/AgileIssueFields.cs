// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Container for the fields of an agile issue
/// </summary>
public class AgileIssueFields : IssueFields
{
    /// <summary>
    ///     The closed sprint information
    /// </summary>
    [JsonPropertyName("closedSprints")]
    public IList<Sprint> ClosedSprints { get; set; }
}