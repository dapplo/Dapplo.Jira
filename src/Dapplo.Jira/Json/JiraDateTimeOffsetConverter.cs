// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Dapplo.Jira.Json;

/// <summary>
/// A JsonConverter especially for the Jira datetime format
/// </summary>
public class JiraDateTimeOffsetConverter : JsonConverter<DateTimeOffset?>
{
    private const string Iso8601Format = @"yyyy-MM-dd\THH:mm:ss.fff";

    private readonly string _format;

    /// <summary>
    /// Default constructor with the Iso8601 format
    /// </summary>
    public JiraDateTimeOffsetConverter() : this(Iso8601Format)
    {
    }

    /// <summary>
    /// Constructor which supports a custom format
    /// </summary>
    /// <param name="format"></param>
    public JiraDateTimeOffsetConverter(string format)
    {
        _format = format;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }

        var dateTime = value.Value;
        string sign = dateTime.Offset < TimeSpan.Zero ? "-" : "+";
        var output = $"{dateTime.ToString(_format, CultureInfo.InvariantCulture)}{sign}{Math.Abs(dateTime.Offset.Hours):00}{Math.Abs(dateTime.Offset.Minutes):00}";
        writer.WriteStringValue(output);
    }

    /// <inheritdoc />
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Unexpected token parsing date. Expected string, got {reader.TokenType}.");
        }

        string dateTimeOffsetString = reader.GetString();
        
        if (string.IsNullOrEmpty(dateTimeOffsetString) || dateTimeOffsetString.Equals("none", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (Regex.IsMatch(dateTimeOffsetString, @"\d{4}$"))
        {
            dateTimeOffsetString = dateTimeOffsetString.Insert(dateTimeOffsetString.Length - 2, ":");
        }

        return DateTimeOffset.Parse(dateTimeOffsetString, CultureInfo.InvariantCulture);
    }
}
