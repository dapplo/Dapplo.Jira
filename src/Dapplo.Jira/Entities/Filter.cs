// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Filter information
/// </summary>
[JsonObject]
public class Filter : BaseProperties<long>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public Filter()
    {
    }

    /// <summary>
    /// Constructor with a JQL clause
    /// </summary>
    /// <param name="jql">IFinalClause</param>
    public Filter(IFinalClause jql)
    {
        Jql = jql.ToString();
    }

    /// <summary>
    /// Constructor with a JQL clause and name
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="jql">IFinalClause</param>
    public Filter(string name, IFinalClause jql)
    {
        Name = name;
        Jql = jql.ToString();
    }

    /// <summary>
    ///     Description of the filter
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Is the filter a favorite?
    /// </summary>
    [JsonProperty("favorite")]
    public bool IsFavorite { get; set; }

    /// <summary>
    ///     The JQL (query) for the filter
    /// </summary>
    [JsonProperty("jql")]
    public string Jql { get; set; }

    /// <summary>
    ///     Name for the filter
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     User who owns the filter
    /// </summary>
    [JsonProperty("owner")]
    public User Owner { get; set; }

    /// <summary>
    ///     The URL to show the search results
    /// </summary>
    [JsonProperty("searchUrl")]
    public Uri SearchUri { get; set; }

    /// <summary>
    ///     The permissions for sharing
    /// </summary>
    [JsonProperty("sharePermissions")]
    public IList<SharePermission> SharePermissions { get; set; }

    /// <summary>
    ///     The subscriptions for this filter
    /// </summary>
    [JsonProperty("subscriptions")]
    public Subscriptions Subscriptions { get; set; }

    /// <summary>
    ///     The url to view the filter
    /// </summary>
    [JsonProperty("viewUrl")]
    public Uri ViewUri { get; set; }
}