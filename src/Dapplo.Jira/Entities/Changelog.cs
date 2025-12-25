// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Changelog informations
/// </summary>
/// <seealso cref="Dapplo.Jira.Entities.PageableResult" />
public class Changelog : PageableResult
{
    /// <summary>
    ///     The actual history in changelog
    /// </summary>
    [JsonPropertyName("histories")]
    public IList<History> Elements { get; set; }
}