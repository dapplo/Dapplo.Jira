// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Worklog information
/// </summary>
public class UpdatedWorklogs : IEnumerable<UpdatedWorklog>
{
    /// <summary>
    ///     The worklog items
    /// </summary>
    [JsonPropertyName("values")]
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
    [JsonPropertyName("since")]
    public long Since { get; set; }

    /// <summary>
    ///     The date and time, as a UNIX timestamp in milliseconds, until which updated worklogs are returned.
    /// </summary>
    [JsonPropertyName("until")]
    public long Until { get; set; }

    /// <summary>
    ///     The date and time, as a UNIX timestamp in milliseconds, until which updated worklogs are returned.
    /// </summary>
    [JsonPropertyName("lastPage")]
    public bool IsLastPage { get; set; }

    /// <summary>
    ///     The date and time, as a UNIX timestamp in milliseconds, until which updated worklogs are returned.
    /// </summary>
    [JsonPropertyName("nextPage")]
    public string NextPageUrl { get; set; }
}