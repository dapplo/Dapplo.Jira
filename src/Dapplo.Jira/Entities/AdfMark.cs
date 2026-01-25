// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
/// Represents a mark applied to a node (e.g., strong, em, strike, link).
/// </summary>
[JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore)]
public class AdfMark
{
    /// <summary>
    /// The type of the mark (e.g., "strong", "em", "link", "textColor").
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// Attributes for the mark (e.g., href for links, color for textColor).
    /// </summary>
    [JsonProperty("attrs")]
    public Dictionary<string, object> Attrs { get; set; }
}
