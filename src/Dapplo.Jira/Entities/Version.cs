// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Version information
    /// </summary>
    [JsonObject]
    public class Version : BaseProperties<string>
    {
        /// <summary>
        ///     Is this an achived version?
        /// </summary>
        [JsonProperty("archived")]
        public bool Archived { get; set; }

        /// <summary>
        ///     Name of the version
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Is this version released?
        /// </summary>
        [JsonProperty("released")]
        public bool Released { get; set; }
    }
}
