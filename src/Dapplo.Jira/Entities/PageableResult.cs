// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Container for pageable information in a result
    /// </summary>
    [JsonObject]
    public class PageableResult : Page
    {
        /// <summary>
        ///     The total results
        /// </summary>
        [JsonProperty("total")]
        public int? Total { get; set; }

        /// <summary>
        ///     Specifies if there are more results
        /// </summary>
        [JsonProperty("isLast")]
        public bool IsLastPage { get; set; }
    }
}
