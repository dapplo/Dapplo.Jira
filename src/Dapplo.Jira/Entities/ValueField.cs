// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Represents Values in numeric and string modes
/// </summary>
public class ValueField
{
    /// <summary>
    ///     The numeric value
    /// </summary>
    [JsonPropertyName("value")]
    public long Value { get; set; }

    /// <summary>
    ///     The string value
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; }
}