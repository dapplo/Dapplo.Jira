// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
/// This defines an add operation
/// </summary>
[JsonObject]
public class IssueUpdateOperationAdd : IIssueUpdateOperation
{
    /// <summary>
    /// The values to add
    /// </summary>
    [JsonProperty("add")]
    public IDictionary<string, string> Add { get; set; }
}