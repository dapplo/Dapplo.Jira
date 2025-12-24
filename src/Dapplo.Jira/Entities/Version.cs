// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Version information
/// </summary>
public class Version : BaseProperties<string>
{
    /// <summary>
    ///     Is this an archived version?
    /// </summary>
    [JsonPropertyName("archived")]
    public bool Archived { get; set; }

    /// <summary>
    ///     Description of the version
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Name of the version
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Is this version released?
    /// </summary>
    [JsonPropertyName("released")]
    public bool Released { get; set; }

    /// <summary>
    ///     Release date of the version
    /// </summary>
    [JsonPropertyName("releaseDate")]
    public DateTimeOffset? ReleaseDate { get; set; }

    /// <summary>
    ///     Start date of the version
    /// </summary>
    [JsonPropertyName("startDate")]
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    ///     Project ID this version belongs to
    /// </summary>
    [JsonPropertyName("projectId")]
    public long? ProjectId { get; set; }

    /// <summary>
    ///     Is this version overdue?
    /// </summary>
    [JsonPropertyName("overdue")]
    public bool? Overdue { get; set; }
}