// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Subscriptions information (looks more or less pagable, although I don't know how to specify the page size etc)
/// </summary>
public class Subscriptions
{
    /// <summary>
    ///     Where does this "page" end?
    /// </summary>
    [JsonPropertyName("end-index")]
    public long? EndIndex { get; set; }

    /// <summary>
    ///     The actual list of subscriptions
    /// </summary>
    [JsonPropertyName("items")]
    public IList<Subscription> Items { get; set; }

    /// <summary>
    ///     How many results are given back
    /// </summary>
    [JsonPropertyName("max-results")]
    public long? MaxResults { get; set; }

    /// <summary>
    ///     How many subscritions are there?
    ///     This could be more than the amount of items, not only due to rights (maybe the subscriber is not visible) but also
    ///     as the value is not expanded.
    /// </summary>
    [JsonPropertyName("size")]
    public long? Size { get; set; }

    /// <summary>
    ///     Where does this page start?
    /// </summary>
    [JsonPropertyName("start-index")]
    public long? StartIndex { get; set; }
}