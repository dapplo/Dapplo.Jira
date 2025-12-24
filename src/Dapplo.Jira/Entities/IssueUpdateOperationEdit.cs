// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
/// This defines a edit operation
/// </summary>
public class IssueUpdateOperationEdit : IIssueUpdateOperation
{
    /// <summary>
    /// The values to edit
    /// </summary>
    [JsonPropertyName("edit")]
    public IDictionary<string, string> Edit { get; set; }
}