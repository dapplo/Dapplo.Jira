// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Newtonsoft.Json;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Converters;

namespace Dapplo.Jira.Json
{
    /// <summary>
    /// A JsonConverter especially for the Jira datetime format
    /// </summary>
    public class CustomDateTimeOffsetConverter : DateTimeConverterBase
    {
        private const string Iso8601Format = @"yyyy-MM-dd\THH:mm:ss.fff";

        private readonly string _format;

        /// <summary>
        /// Default constructor with the Iso8601 format
        /// </summary>
        public CustomDateTimeOffsetConverter() : this(Iso8601Format)
        {
        }

        /// <summary>
        /// Constructor which supports a custom format
        /// </summary>
        /// <param name="format"></param>
        public CustomDateTimeOffsetConverter(string format)
        {
            _format = format;
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                return;
            }
            var dateTime = (DateTimeOffset)value;
            string sign = dateTime.Offset < TimeSpan.Zero ? "-" : "+";
            var output = $"{dateTime.ToString(_format, CultureInfo.InvariantCulture)}{sign}{Math.Abs(dateTime.Offset.Hours):00}{Math.Abs(dateTime.Offset.Minutes):00}";
            writer.WriteValue(output);
            writer.Flush();
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }
            string dateTimeOffsetString = (string)reader.Value;
            if (reader.TokenType != JsonToken.String)
            {
                throw new Exception($"Unexpected token parsing date. Expected string, got {reader.TokenType}.");
            }
            if (dateTimeOffsetString.ToLowerInvariant() == "none")
            {
                return null;
            }
            if (Regex.IsMatch(dateTimeOffsetString, @"\d{4}$"))
            {
                dateTimeOffsetString = dateTimeOffsetString.Insert(dateTimeOffsetString.Length - 2, ":");

            }
            return DateTimeOffset.Parse(dateTimeOffsetString, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTimeOffset);
        }
    }
}
