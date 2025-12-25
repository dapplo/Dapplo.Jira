// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Information on the custom field id for the ranking information
/// </summary>
public class RankingCustomFieldInfo
{
    /// <summary>
    ///     Id of the Rank custom field
    /// </summary>
    [JsonPropertyName("rankCustomFieldId")]
    public long? RankCustomFieldId { get; set; }
}