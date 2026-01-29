// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Represents an issue property with a value
/// </summary>
[JsonObject]
public class IssueProperty
{
    /// <summary>
    ///     The property key
    /// </summary>
    [JsonProperty("key")]
    public string Key { get; set; }

    /// <summary>
    ///     The property value (can be any JSON object)
    /// </summary>
    [JsonProperty("value")]
    public object Value { get; set; }
}
