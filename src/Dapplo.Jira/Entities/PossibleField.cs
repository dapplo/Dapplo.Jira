// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Possible field information
    /// </summary>
    [JsonObject]
    public class PossibleField
    {
        /// <summary>
        ///     Allowed values
        /// </summary>
        [JsonProperty("allowedValues")]
        public IList<AllowedValue> AllowedValues { get; set; }

        /// <summary>
        ///     TODO: Describe
        /// </summary>
        [JsonProperty("autoCompleteUrl")]
        public Uri AutoCompleteUrl { get; set; }

        /// <summary>
        ///     Name of the field
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Possible operations
        /// </summary>
        [JsonProperty("operations")]
        public IList<string> Operations { get; set; }

        /// <summary>
        ///     The summary of the time spend on this issue
        /// </summary>
        [JsonProperty("required")]
        public bool Required { get; set; }
    }
}
