// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Security level information
    /// </summary>
    [JsonObject]
    public class SecurityLevel : BaseProperties<long>
    {
        /// <summary>
        ///     Description for the security level
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Name of the security level
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}