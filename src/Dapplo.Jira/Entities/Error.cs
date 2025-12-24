// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Container for the Error
/// </summary>
public class Error
{
    /// <summary>
    ///     The HTTP status code of the error
    /// </summary>
    [JsonPropertyName("status-code")]
    public int StatusCode { get; set; }

    /// <summary>
    ///     The list of error messages
    /// </summary>
    [JsonPropertyName("errorMessages")]
    public IList<string> ErrorMessages { get; set; }

    /// <summary>
    ///     The message
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; }

    /// <summary>
    /// A list of errors
    /// </summary>
    [JsonPropertyName("errors")]
    public IDictionary<string, string> Errors { get; set; }
}