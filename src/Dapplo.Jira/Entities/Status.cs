// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Status information
    /// </summary>
    [JsonObject]
    public class Status : BaseProperties<string>
    {
        /// <summary>
        ///     Category for this status
        /// </summary>
        [JsonProperty("statusCategory")]
        public StatusCategory Category { get; set; }

        /// <summary>
        ///     Description for this status
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Url to the icon for this status
        /// </summary>
        [JsonProperty("iconUrl")]
        public Uri IconUri { get; set; }

        /// <summary>
        ///     Name of the status
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
