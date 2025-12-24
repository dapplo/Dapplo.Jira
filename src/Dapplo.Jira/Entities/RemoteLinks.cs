// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Remote Links
/// </summary>
public class RemoteLinks
{
    /// <summary>
    ///     Remote link title or type
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>
    ///     Remote link url
    /// </summary>
    [JsonPropertyName("url")]
    public Uri Url { get; set; }
}