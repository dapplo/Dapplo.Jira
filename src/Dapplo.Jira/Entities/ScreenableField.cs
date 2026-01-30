// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Screenable field information (fields available to be added to screens)
/// </summary>
[JsonObject]
public class ScreenableField
{
    /// <summary>
    ///     ID of the field
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    ///     Name of the field
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }
}
