// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Progress information
/// </summary>
[JsonObject]
public class ProgressInfo
{
    /// <summary>
    ///     Progress for the issue
    /// </summary>
    [JsonProperty("progress")]
    public long? Progress { get; set; }

    /// <summary>
    ///     The total progress
    /// </summary>
    [JsonProperty("total")]
    public long? Total { get; set; }
}