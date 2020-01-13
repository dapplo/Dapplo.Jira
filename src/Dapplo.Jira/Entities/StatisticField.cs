// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Statistic Field information
    /// </summary>
    [JsonObject]
    public class StatisticField
    {
        /// <summary>
        ///     The Statistic Field Id
        /// </summary>
        [JsonProperty("statFieldId")]
        public string StatFieldId { get; set; }

        /// <summary>
        ///     The Statistic Field value
        /// </summary>
        [JsonProperty("statFieldValue")]
        public ValueField StatFieldValue { get; set; }
    }
}
