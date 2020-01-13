// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Represents Values in numeric and string modes
    /// </summary>
    [JsonObject]
    public class ValueField
    {
        /// <summary>
        ///     The numeric value
        /// </summary>
        [JsonProperty("value")]
        public long Value { get; set; }

        /// <summary>
        ///     The string value
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
