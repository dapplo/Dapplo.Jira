// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Progress information
/// </summary>
public class ProgressInfo
{
    /// <summary>
    ///     Progress for the issue
    /// </summary>
    [JsonPropertyName("progress")]
    public long? Progress { get; set; }

    /// <summary>
    ///     The total progress
    /// </summary>
    [JsonPropertyName("total")]
    public long? Total { get; set; }
}