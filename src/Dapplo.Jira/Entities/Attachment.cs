// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Attachment information
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/attachment
/// </summary>
public class Attachment : BaseProperties<long>
{
    /// <summary>
    ///     Who created the attachment
    /// </summary>
    [JsonPropertyName("author")]
    public User Author { get; set; }

    /// <summary>
    ///     Url which can be used to download the attachment
    /// </summary>
    [JsonPropertyName("content")]
    public Uri ContentUri { get; set; }

    /// <summary>
    ///     When was the attachment created
    /// </summary>
    [JsonPropertyName("created")]
    public DateTimeOffset? Created { get; set; }

    /// <summary>
    ///     Filename of the attachment
    /// </summary>
    [JsonPropertyName("filename")]
    public string Filename { get; set; }

    /// <summary>
    ///     Mimetype for the attachment
    /// </summary>
    [JsonPropertyName("mimeType")]
    public string MimeType { get; set; }

    /// <summary>
    ///     Size (in bytes) of the attachment
    /// </summary>
    [JsonPropertyName("size")]
    public long? Size { get; set; }

    /// <summary>
    ///     An URL to the thumbnail for this attachment
    /// </summary>
    [JsonPropertyName("thumbnail")]
    public Uri ThumbnailUri { get; set; }
}