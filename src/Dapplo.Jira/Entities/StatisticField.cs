// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Statistic Field information
/// </summary>
public class StatisticField
{
    /// <summary>
    ///     The Statistic Field Id
    /// </summary>
    [JsonPropertyName("statFieldId")]
    public string StatFieldId { get; set; }

    /// <summary>
    ///     The Statistic Field value
    /// </summary>
    [JsonPropertyName("statFieldValue")]
    public ValueField StatFieldValue { get; set; }
}