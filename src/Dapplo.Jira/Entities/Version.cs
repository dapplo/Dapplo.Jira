// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Version information
/// </summary>
[JsonObject]
public class Version : BaseProperties<string>
{
    /// <summary>
    ///     Is this an archived version?
    /// </summary>
    [JsonProperty("archived")]
    public bool Archived { get; set; }

    /// <summary>
    ///     Description of the version
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Name of the version
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Is this version released?
    /// </summary>
    [JsonProperty("released")]
    public bool Released { get; set; }

    /// <summary>
    ///     Release date of the version
    /// </summary>
    [JsonProperty("releaseDate")]
    public DateTimeOffset? ReleaseDate { get; set; }

    /// <summary>
    ///     Start date of the version
    /// </summary>
    [JsonProperty("startDate")]
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    ///     Project ID this version belongs to
    /// </summary>
    [JsonProperty("projectId")]
    public long? ProjectId { get; set; }

    /// <summary>
    ///     Is this version overdue?
    /// </summary>
    [JsonProperty("overdue")]
    public bool? Overdue { get; set; }
}