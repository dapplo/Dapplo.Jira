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
    public static JsonSerializerOptions Default { get; } = CreateDefaultOptions();

    private static JsonSerializerOptions CreateDefaultOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = 
            {
                new JiraDateTimeOffsetConverter(),
                new JsonStringEnumConverter()
            }
        };

#if NET6_0_OR_GREATER
        // Use source generation for better performance on .NET 6+
        options.TypeInfoResolver = JiraJsonContext.Default;
#endif

        return options;
    }
}
