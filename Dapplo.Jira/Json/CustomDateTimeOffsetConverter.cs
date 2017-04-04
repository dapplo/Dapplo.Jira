#region Dapplo 2017 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Jira
// 
// Dapplo.Jira is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Jira is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

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
