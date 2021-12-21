// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dapplo.HttpExtensions.Support;

namespace Dapplo.Jira.Entities;

/// <summary>
///     The attachment needs to be uploaded as a multi-part request
/// </summary>
[HttpRequest(MultiPart = true)]
public class AttachmentContainer<T>
{
    /// <summary>
    ///     The actual content for the attachment
    /// </summary>
    [HttpPart(HttpParts.RequestContent)]
    public T Content { get; set; }

    /// <summary>
    ///     The name of the content, this is always "file"
    /// </summary>
    [HttpPart(HttpParts.RequestMultipartName)]
    public string ContentName { get; } = "file";

    /// <summary>
    ///     The (mime) type for the content
    /// </summary>
    [HttpPart(HttpParts.RequestContentType)]
    public string ContentType { get; set; } = "text/plain";


    /// <summary>
    ///     Filename for the attachment
    /// </summary>
    [HttpPart(HttpParts.RequestMultipartFilename)]
    public string FileName { get; set; }
}