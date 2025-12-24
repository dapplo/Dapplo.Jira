// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Extensions;

/// <summary>
/// Extension methods for Uri to replace Dapplo.HttpExtensions functionality
/// </summary>
public static class UriExtensions
{
    /// <summary>
    /// Append segments to a URI
    /// </summary>
    /// <param name="uri">Base URI</param>
    /// <param name="segments">Segments to append</param>
    /// <returns>New URI with appended segments</returns>
    public static Uri AppendSegments(this Uri uri, params string[] segments)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }

        if (segments == null || segments.Length == 0)
        {
            return uri;
        }

        var uriBuilder = new UriBuilder(uri);
        var path = uriBuilder.Path.TrimEnd('/');
        
        foreach (var segment in segments)
        {
            path += "/" + Uri.EscapeDataString(segment);
        }
        
        uriBuilder.Path = path;
        return uriBuilder.Uri;
    }

    /// <summary>
    /// Extend the query string of a URI
    /// </summary>
    /// <param name="uri">Base URI</param>
    /// <param name="key">Query parameter key</param>
    /// <param name="value">Query parameter value</param>
    /// <returns>New URI with extended query</returns>
    public static Uri ExtendQuery(this Uri uri, string key, string value)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }

        var uriBuilder = new UriBuilder(uri);
        var query = uriBuilder.Query;
        
        if (string.IsNullOrEmpty(query))
        {
            uriBuilder.Query = $"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}";
        }
        else
        {
            // Remove leading '?' if present
            query = query.TrimStart('?');
            uriBuilder.Query = $"{query}&{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}";
        }
        
        return uriBuilder.Uri;
    }

    /// <summary>
    /// Extend the query string of a URI with a long value
    /// </summary>
    /// <param name="uri">Base URI</param>
    /// <param name="key">Query parameter key</param>
    /// <param name="value">Query parameter value</param>
    /// <returns>New URI with extended query</returns>
    public static Uri ExtendQuery(this Uri uri, string key, long value)
    {
        return ExtendQuery(uri, key, value.ToString());
    }

    /// <summary>
    /// Extend the query string of a URI with an int value
    /// </summary>
    /// <param name="uri">Base URI</param>
    /// <param name="key">Query parameter key</param>
    /// <param name="value">Query parameter value</param>
    /// <returns>New URI with extended query</returns>
    public static Uri ExtendQuery(this Uri uri, string key, int value)
    {
        return ExtendQuery(uri, key, value.ToString());
    }
}
