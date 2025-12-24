// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Net;

namespace Dapplo.Jira.Internal;

/// <summary>
/// Wrapper for HTTP response with status code
/// </summary>
public class HttpResponse
{
    /// <summary>
    /// HTTP status code
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }
}

/// <summary>
/// Wrapper for HTTP response with typed response
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>
public class HttpResponse<TResponse> : HttpResponse where TResponse : class
{
    /// <summary>
    /// The response object
    /// </summary>
    public TResponse Response { get; set; }
}

/// <summary>
/// Wrapper for HTTP response with typed response and error
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>
/// <typeparam name="TError">Error type</typeparam>
public class HttpResponse<TResponse, TError> : HttpResponse where TResponse : class
{
    /// <summary>
    /// The response object
    /// </summary>
    public TResponse Response { get; set; }

    /// <summary>
    /// The error response object
    /// </summary>
    public TError ErrorResponse { get; set; }

    /// <summary>
    /// Indicates if there was an error
    /// </summary>
    public bool HasError { get; set; }
}

/// <summary>
/// Wrapper for HTTP response with error only
/// </summary>
/// <typeparam name="TError">Error type</typeparam>
public class HttpResponseWithError<TError> : HttpResponse
{
    /// <summary>
    /// The error response object
    /// </summary>
    public TError ErrorResponse { get; set; }
}
