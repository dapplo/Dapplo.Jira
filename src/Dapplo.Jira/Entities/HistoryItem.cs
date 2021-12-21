// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dapplo.Jira.Entities;

/// <summary>
///     One change on one specific field of one item
/// </summary>
[JsonObject]
public class HistoryItem
{
    /// <summary>
    ///     Gets or sets the field.
    /// </summary>
    /// <value>
    ///     The field.
    /// </value>
    [JsonProperty("field")]
    [ReadOnly(true)]
    public string Field { get; set; }

    /// <summary>
    ///     Gets or sets the type of the field.
    /// </summary>
    /// <value>
    ///     The type of the field.
    /// </value>
    [JsonProperty("fieldtype")]
    [ReadOnly(true)]
    [JsonConverter(typeof(StringEnumConverter))]
    public FieldType FieldType { get; set; }

    /// <summary>
    ///     The from value value of JIRA. Could be a number, string, ... depending of the type
    /// </summary>
    /// <value>
    ///     From.
    /// </value>
    [JsonProperty("from")]
    [ReadOnly(true)]
    public string From { get; set; }

    /// <summary>
    ///     A string representation of the from value of JIRA
    /// </summary>
    /// <value>
    ///     From string.
    /// </value>
    [JsonProperty("fromString")]
    [ReadOnly(true)]
    public string FromAsString { get; set; }

    /// <summary>
    ///     The to value value of JIRA. Could be a number, string, ... depending of the type
    /// </summary>
    /// <value>
    ///     From.
    /// </value>
    [JsonProperty("to")]
    [ReadOnly(true)]
    public string To { get; set; }

    /// <summary>
    ///     A string representation of the from value of JIRA
    /// </summary>
    /// <value>
    ///     From string.
    /// </value>
    [JsonProperty("toString")]
    [ReadOnly(true)]
    public string ToAsString { get; set; }
}