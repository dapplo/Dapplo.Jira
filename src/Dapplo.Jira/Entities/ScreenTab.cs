// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Screen tab information
/// </summary>
[JsonObject]
public class ScreenTab : BaseProperties<long>
{
    /// <summary>
    ///     Name of the tab
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }
}
