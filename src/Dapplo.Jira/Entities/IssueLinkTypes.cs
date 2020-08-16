// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    /// Container to retrieve every possible IssueLinkType
    /// </summary>
    [JsonObject]
    public class IssueLinkTypes
    {
        /// <summary>
        /// Possible issue link types
        /// </summary>
        [JsonProperty("issueLinkTypes")]
        public IList<IssueLinkType> Values { get; set; }
    }
}