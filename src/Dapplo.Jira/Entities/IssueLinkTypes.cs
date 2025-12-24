// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
/// Container to retrieve every possible IssueLinkType
/// </summary>
public class IssueLinkTypes
{
    /// <summary>
    /// Possible issue link types
    /// </summary>
    [JsonPropertyName("issueLinkTypes")]
    public IList<IssueLinkType> Values { get; set; }
}