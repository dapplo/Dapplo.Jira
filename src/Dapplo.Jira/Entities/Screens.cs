// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Screens result information
/// </summary>
[JsonObject]
public class Screens : PageableResult, IEnumerable<Screen>
{
    /// <summary>
    ///     The screen items
    /// </summary>
    [JsonProperty("values")]
    public IList<Screen> Values { get; set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// IEnumerator implementation
    /// </summary>
    /// <returns>IEnumerator of type Screen</returns>
    public IEnumerator<Screen> GetEnumerator()
    {
        return Values.GetEnumerator();
    }
}
