// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Filter information
/// </summary>
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
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Is the filter a favorite?
    /// </summary>
    [JsonPropertyName("favorite")]
    public bool IsFavorite { get; set; }

    /// <summary>
    ///     The JQL (query) for the filter
    /// </summary>
    [JsonPropertyName("jql")]
    public string Jql { get; set; }

    /// <summary>
    ///     Name for the filter
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    ///     User who owns the filter
    /// </summary>
    [JsonPropertyName("owner")]
    public User Owner { get; set; }

    /// <summary>
    ///     The URL to show the search results
    /// </summary>
    [JsonPropertyName("searchUrl")]
    public Uri SearchUri { get; set; }

    /// <summary>
    ///     The permissions for sharing
    /// </summary>
    [JsonPropertyName("sharePermissions")]
    public IList<SharePermission> SharePermissions { get; set; }

    /// <summary>
    ///     The subscriptions for this filter
    /// </summary>
    [JsonPropertyName("subscriptions")]
    public Subscriptions Subscriptions { get; set; }

    /// <summary>
    ///     The url to view the filter
    /// </summary>
    [JsonPropertyName("viewUrl")]
    public Uri ViewUri { get; set; }
}