// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json;
using System.Text.Json.Serialization;
using Dapplo.Jira.Json;

namespace Dapplo.Jira;

/// <summary>
/// Shared JSON serializer options for System.Text.Json
/// </summary>
public static class JiraJsonSerializerOptions
{
    /// <summary>
    /// Default options used across the library
    /// </summary>
    public static JsonSerializerOptions Default { get; } = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = 
        {
            new JiraDateTimeOffsetConverter(),
            new JsonStringEnumConverter()
        }
    };
}
