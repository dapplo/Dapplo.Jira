// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Priority information
    /// </summary>
    [JsonObject]
    public class Priority : BaseProperties<int>
    {
        /// <summary>
        ///     Description of the priority
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Url to the icon for this priority
        /// </summary>
        [JsonProperty("iconUrl")]
        public Uri IconUrl { get; set; }

        /// <summary>
        ///     Name of the priority
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Status color
        /// </summary>
        [JsonProperty("statusColor")]
        public string StatusColor { get; set; }
    }
}
