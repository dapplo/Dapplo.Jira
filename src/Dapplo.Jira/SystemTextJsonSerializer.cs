// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Json;

namespace Dapplo.Jira;

/// <summary>
/// JSON serializer using System.Text.Json
/// </summary>
public class SystemTextJsonSerializer : IJsonSerializer
{
    private static readonly JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = 
        {
            new JiraDateTimeOffsetConverter(),
            new JsonStringEnumConverter()
        }
    };

    /// <inheritdoc />
    public string MimeType => "application/json";

    /// <inheritdoc />
    public bool CanSerializeTo(Type targetType) => true;

    /// <inheritdoc />
    public bool CanDeserializeFrom(Type currentType) => true;

    /// <inheritdoc />
    public T Deserialize<T>(Stream jsonStream)
    {
        return JsonSerializer.Deserialize<T>(jsonStream, DefaultOptions);
    }

    /// <inheritdoc />
    public object Deserialize(Type targetType, Stream jsonStream)
    {
        return JsonSerializer.Deserialize(jsonStream, targetType, DefaultOptions);
    }

    /// <inheritdoc />
    public object Deserialize(Type targetType, string jsonString)
    {
        return JsonSerializer.Deserialize(jsonString, targetType, DefaultOptions);
    }

    /// <inheritdoc />
    public void Serialize(Stream outputStream, object obj)
    {
        using var writer = new Utf8JsonWriter(outputStream);
        JsonSerializer.Serialize(writer, obj, obj.GetType(), DefaultOptions);
    }

    /// <inheritdoc />
    public string Serialize(object obj)
    {
        return JsonSerializer.Serialize(obj, obj.GetType(), DefaultOptions);
    }

    /// <inheritdoc />
    public string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, DefaultOptions);
    }
}
