// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    [JsonObject]
    public class IssueLinkAdd
    {
        /// <summary>
        /// Describes type of link
        /// </summary>
        [JsonProperty("type")]
        public IssueLinkType Type { get; set; }

        /// <summary>
        /// The outward issue to link to
        /// </summary>
        [JsonProperty("outwardIssue")]
        public LinkedIssue OutwardIssue { get; set; }
    }
}