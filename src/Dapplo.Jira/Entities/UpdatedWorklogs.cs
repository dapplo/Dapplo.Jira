// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Worklog information
/// </summary>
[JsonObject]
public class UpdatedWorklogs : IEnumerable<UpdatedWorklog>
{
    /// <summary>
    ///     The worklog items
    /// </summary>
    [JsonProperty("values")]
    public IList<UpdatedWorklog> Elements { get; set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// IEnumerator implementation
    /// </summary>
    /// <returns>IEnumerator of type TResultType</returns>
    public IEnumerator<UpdatedWorklog> GetEnumerator()
    {
        return Elements.GetEnumerator();
    }

    /// <summary>
    ///     The date and time, as a UNIX timestamp in milliseconds, after which updated worklogs are returned.
    /// </summary>
    [JsonProperty("since")]
    public long Since { get; set; }

    /// <summary>
    ///     The date and time, as a UNIX timestamp in milliseconds, until which updated worklogs are returned.
    /// </summary>
    [JsonProperty("until")]
    public long Until { get; set; }

    /// <summary>
    ///     The date and time, as a UNIX timestamp in milliseconds, until which updated worklogs are returned.
    /// </summary>
    [JsonProperty("lastPage")]
    public bool IsLastPage { get; set; }

    /// <summary>
    ///     The date and time, as a UNIX timestamp in milliseconds, until which updated worklogs are returned.
    /// </summary>
    [JsonProperty("nextPage")]
    public string NextPageUrl { get; set; }
}