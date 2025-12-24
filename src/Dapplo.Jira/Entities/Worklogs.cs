// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Worklog information
/// </summary>
public class Worklogs : PageableResult, IEnumerable<Worklog>
{
    /// <summary>
    ///     The worklog items
    /// </summary>
    [JsonPropertyName("worklogs")]
    public IList<Worklog> Elements { get; set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// IEnumerator implementation
    /// </summary>
    /// <returns>IEnumerator of type TResultType</returns>
    public IEnumerator<Worklog> GetEnumerator()
    {
        return Elements.GetEnumerator();
    }
}