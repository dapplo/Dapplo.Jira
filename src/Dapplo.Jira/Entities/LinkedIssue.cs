// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
/// The ID or key of a linked issue.
/// </summary>
public class LinkedIssue : ReadOnlyBaseId<string>
{
    /// <summary>
    /// Issue key of linked issue
    /// </summary>
    [JsonPropertyName("key")]
    public string Key { get; set; }
}