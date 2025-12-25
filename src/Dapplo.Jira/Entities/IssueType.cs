// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     IssueType information
/// </summary>
public class IssueType : BaseProperties<long>
{
    /// <summary>
    ///     Description of the issue type
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     URL to the icon for the issue type
    /// </summary>
    [JsonPropertyName("iconUrl")]
    public Uri IconUri { get; set; }

    /// <summary>
    ///     Is the issue type a sub task?
    /// </summary>
    [JsonPropertyName("subtask")]
    public bool IsSubTask { get; set; }

    /// <summary>
    ///     Name of the issue type
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
}