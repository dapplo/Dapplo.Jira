// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     UpdatedWorklog information
    /// </summary>
    [JsonObject]
    public class UpdatedWorklog
    {
        /// <summary>
        ///     The ID of the updated worklog.
        /// </summary>
        [JsonProperty("worklogId")]
        public long Id { get; set; }

        /// <summary>
        ///     The datetime of the change, as a UNIX timestamp in milliseconds.
        /// </summary>
        [JsonProperty("updatedTime")]
        public long UpdatedTime { get; set; }

        /// <summary>
        ///     The updated worklog properties
        /// </summary>
        [JsonProperty("properties")]
        public IList<EntityProperty> Properties { get; set; }
    }
}
