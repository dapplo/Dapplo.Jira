// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Remote Links
/// </summary>
[JsonObject]
public class RemoteLinks
{
    /// <summary>
    ///     Remote link title or type
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; }

    /// <summary>
    ///     Remote link url
    /// </summary>
    [JsonProperty("url")]
    public Uri Url { get; set; }
}