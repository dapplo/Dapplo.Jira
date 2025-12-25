// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
/// This defines an add operation
/// </summary>
public class IssueUpdateOperationAdd : IIssueUpdateOperation
{
    /// <summary>
    /// The values to add
    /// </summary>
    [JsonPropertyName("add")]
    public IDictionary<string, string> Add { get; set; }
}