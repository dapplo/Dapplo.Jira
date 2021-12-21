// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Attachment information
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/attachment
/// </summary>
[JsonObject]
public class RenderedAttachment : BaseProperties<long>
{
    /// <summary>
    ///     Who created the attachment
    /// </summary>
    [JsonProperty("author")]
    public User Author { get; set; }

    /// <summary>
    ///     Url which can be used to download the attachment
    /// </summary>
    [JsonProperty("content")]
    public Uri ContentUri { get; set; }

    /// <summary>
    ///     When was the attachment created
    /// </summary>
    [JsonProperty("created")]
    public string Created { get; set; }

    /// <summary>
    ///     Filename of the attachment
    /// </summary>
    [JsonProperty("filename")]
    public string Filename { get; set; }

    /// <summary>
    ///     Mimetype for the attachment
    /// </summary>
    [JsonProperty("mimeType")]
    public string MimeType { get; set; }

    /// <summary>
    ///     Size (in bytes) of the attachment
    /// </summary>
    [JsonProperty("size")]
    public string Size { get; set; }

    /// <summary>
    ///     An URL to the thumbnail for this attachment
    /// </summary>
    [JsonProperty("thumbnail")]
    public Uri ThumbnailUri { get; set; }
}