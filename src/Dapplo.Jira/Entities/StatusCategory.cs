// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     StatusCategory information
/// </summary>
[JsonObject]
public class StatusCategory : BaseProperties<long>
{
    /// <summary>
    ///     Name of the color
    /// </summary>
    [JsonProperty("colorName")]
    public string ColorName { get; set; }

    /// <summary>
    ///     Key for the status category
    /// </summary>
    [JsonProperty("key")]
    public string Key { get; set; }

    /// <summary>
    ///     Name of the status category
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }
}