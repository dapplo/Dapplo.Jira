// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Represents a property of an entity
    /// </summary>
    [JsonObject]
    public class EntityProperty
    {
        /// <summary>
        ///     The property key
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        ///     The property value
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
