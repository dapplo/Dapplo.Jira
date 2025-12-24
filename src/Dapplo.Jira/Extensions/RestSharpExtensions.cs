// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using RestSharp;
using System.Text.Json;

namespace Dapplo.Jira.Extensions;

/// <summary>
/// Extension methods for RestSharp RestClient and RestRequest
/// </summary>
public static class RestSharpExtensions
{
    private static RestClient _restClient;
    private static readonly JsonSerializerOptions _jsonOptions = CreateJsonOptions();

    private static JsonSerializerOptions CreateJsonOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            Converters = { new Json.JiraDateTimeOffsetConverter() }
        };
        return options;
    }

    /// <summary>
    /// Set the RestClient instance to use
    /// </summary>
    public static void SetRestClient(RestClient client)
    {
        _restClient = client;
    }

    /// <summary>
    /// Get a shared RestClient instance
    /// </summary>
    public static RestClient GetRestClient()
    {
        return _restClient ?? throw new InvalidOperationException("RestClient not initialized");
    }

    /// <summary>
    /// POST request with response
    /// </summary>
    public static async Task<TResponse> PostAsync<TResponse>(this Uri uri, object body, CancellationToken cancellationToken = default)
    {
        var request = new RestRequest(uri.PathAndQuery, Method.Post);
        if (body != null)
        {
            request.AddJsonBody(body, ContentType.Json);
        }

        var response = await GetRestClient().ExecuteAsync(request, cancellationToken);
        return DeserializeResponse<TResponse>(response);
    }

    /// <summary>
    /// GET request with response
    /// </summary>
    public static async Task<TResponse> GetAsAsync<TResponse>(this Uri uri, CancellationToken cancellationToken = default)
    {
        var request = new RestRequest(uri.PathAndQuery, Method.Get);
        var response = await GetRestClient().ExecuteAsync(request, cancellationToken);
        return DeserializeResponse<TResponse>(response);
    }

    /// <summary>
    /// PUT request with response
    /// </summary>
    public static async Task<TResponse> PutAsync<TResponse>(this Uri uri, object body, CancellationToken cancellationToken = default)
    {
        var request = new RestRequest(uri.PathAndQuery, Method.Put);
        if (body != null)
        {
            request.AddJsonBody(body, ContentType.Json);
        }

        var response = await GetRestClient().ExecuteAsync(request, cancellationToken);
        return DeserializeResponse<TResponse>(response);
    }

    /// <summary>
    /// DELETE request with response
    /// </summary>
    public static async Task<TResponse> DeleteAsync<TResponse>(this Uri uri, CancellationToken cancellationToken = default)
    {
        var request = new RestRequest(uri.PathAndQuery, Method.Delete);
        var response = await GetRestClient().ExecuteAsync(request, cancellationToken);
        return DeserializeResponse<TResponse>(response);
    }

    /// <summary>
    /// PATCH request with response
    /// </summary>
    public static async Task<TResponse> PatchAsync<TResponse>(this Uri uri, object body, CancellationToken cancellationToken = default)
    {
        var request = new RestRequest(uri.PathAndQuery, Method.Patch);
        if (body != null)
        {
            request.AddJsonBody(body, ContentType.Json);
        }

        var response = await GetRestClient().ExecuteAsync(request, cancellationToken);
        return DeserializeResponse<TResponse>(response);
    }

    private static TResponse DeserializeResponse<TResponse>(RestResponse response)
    {
        // Handle HttpResponse wrapper types
        var responseType = typeof(TResponse);
        
        if (responseType.IsGenericType)
        {
            var genericType = responseType.GetGenericTypeDefinition();
            
            // Check if it's an HttpResponse<T, TError> type
            if (genericType.Name.StartsWith("HttpResponse"))
            {
                var args = responseType.GetGenericArguments();
                var httpResponseType = responseType;
                
                // Create instance using reflection
                var instance = Activator.CreateInstance(httpResponseType);
                var statusCodeProp = httpResponseType.GetProperty("StatusCode");
                var responseProp = httpResponseType.GetProperty("Response");
                var errorResponseProp = httpResponseType.GetProperty("ErrorResponse");
                var hasErrorProp = httpResponseType.GetProperty("HasError");
                
                statusCodeProp?.SetValue(instance, response.StatusCode);
                
                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                {
                    var successType = args[0];
                    var deserializedResponse = JsonSerializer.Deserialize(response.Content, successType, _jsonOptions);
                    responseProp?.SetValue(instance, deserializedResponse);
                    hasErrorProp?.SetValue(instance, false);
                }
                else if (args.Length > 1 && !string.IsNullOrEmpty(response.Content))
                {
                    try
                    {
                        var errorType = args[1];
                        var deserializedError = JsonSerializer.Deserialize(response.Content, errorType, _jsonOptions);
                        errorResponseProp?.SetValue(instance, deserializedError);
                        hasErrorProp?.SetValue(instance, true);
                    }
                    catch
                    {
                        hasErrorProp?.SetValue(instance, true);
                    }
                }
                else
                {
                    hasErrorProp?.SetValue(instance, !response.IsSuccessful);
                }
                
                return (TResponse)instance;
            }
        }
        
        // Direct deserialization for non-wrapper types
        if (string.IsNullOrEmpty(response.Content))
        {
            return default;
        }
        
        return JsonSerializer.Deserialize<TResponse>(response.Content, _jsonOptions);
    }
}
