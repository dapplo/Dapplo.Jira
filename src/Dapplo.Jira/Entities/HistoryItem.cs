#region Dapplo 2017-2018 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2018 Dapplo
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

using System.ComponentModel;
using Dapplo.Jira.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     One change on one specific field of one item
    /// </summary>
    [JsonObject]
    public class HistoryItem
    {
        /// <summary>
        ///     Gets or sets the field.
        /// </summary>
        /// <value>
        ///     The field.
        /// </value>
        [JsonProperty("field")]
        [ReadOnly(true)]
        public string Field { get; set; }

        /// <summary>
        ///     Gets or sets the type of the field.
        /// </summary>
        /// <value>
        ///     The type of the field.
        /// </value>
        [JsonProperty("fieldtype")]
        [ReadOnly(true)]
        [JsonConverter(typeof(StringEnumConverter))]
        public FieldType FieldType { get; set; }

        /// <summary>
        ///     The from value value of JIRA. Could be a number, string, ... depending of the type
        /// </summary>
        /// <value>
        ///     From.
        /// </value>
        [JsonProperty("from")]
        [ReadOnly(true)]
        public string From { get; set; }

        /// <summary>
        ///     A string representation of the from value of JIRA
        /// </summary>
        /// <value>
        ///     From string.
        /// </value>
        [JsonProperty("fromString")]
        [ReadOnly(true)]
        public string FromAsString { get; set; }

        /// <summary>
        ///     The to value value of JIRA. Could be a number, string, ... depending of the type
        /// </summary>
        /// <value>
        ///     From.
        /// </value>
        [JsonProperty("to")]
        [ReadOnly(true)]
        public string To { get; set; }

        /// <summary>
        ///     A string representation of the from value of JIRA
        /// </summary>
        /// <value>
        ///     From string.
        /// </value>
        [JsonProperty("toString")]
        [ReadOnly(true)]
        public string ToAsString { get; set; }
    }
}