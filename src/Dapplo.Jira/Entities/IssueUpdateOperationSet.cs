// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
/// This defines a set operation
/// </summary>
public class IssueUpdateOperationSet : IIssueUpdateOperation
{
    /// <summary>
    /// The values to set
    /// </summary>
    [JsonPropertyName("set")]
    public IDictionary<string, string> Set { get; set; }
}