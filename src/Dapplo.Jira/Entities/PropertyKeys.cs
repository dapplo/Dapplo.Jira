// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Container for property keys
/// </summary>
[JsonObject]
public class PropertyKeys
{
    /// <summary>
    ///     The list of property keys
    /// </summary>
    [JsonProperty("keys")]
    public IList<PropertyKey> Keys { get; set; }
}
