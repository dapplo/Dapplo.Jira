// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Server Info is used for the version and title
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/serverInfo
/// </summary>
public class ServerInfo
{
    /// <summary>
    ///     The base URL for this server
    /// </summary>
    [JsonPropertyName("baseUrl")]
    public Uri BaseUrl { get; set; }

    /// <summary>
    ///     Date of the build
    /// </summary>
    [JsonPropertyName("buildDate")]
    public DateTimeOffset? BuildDate { get; set; }

    /// <summary>
    ///     Build number (internal information)
    /// </summary>
    [JsonPropertyName("buildNumber")]
    public int? BuildNumber { get; set; }

    /// <summary>
    ///     Git commit id (at lease until the SCM is changed)
    /// </summary>
    [JsonPropertyName("scmInfo")]
    public string ScmInfo { get; set; }

    /// <summary>
    ///     Title of the server
    /// </summary>
    [JsonPropertyName("serverTitle")]
    public string ServerTitle { get; set; }

    /// <summary>
    ///     Version of the software
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; set; }
}