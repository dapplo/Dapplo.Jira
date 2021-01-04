// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dapplo.Jira.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Board information
    /// </summary>
    [JsonObject]
    public class Board : BaseProperties<long>
    {
        /// <summary>
        ///     Name of the Board
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Board type
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BoardTypes Type { get; set; }

        /// <summary>
        ///     Filter for the board, used when creating
        /// </summary>
        [JsonProperty("filterId")]
        public long FilterId { get; set; }
    }
}
