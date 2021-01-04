// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    /// This object is used as follows:
    /// In the issueLink resource it defines and reports on the type of link between the issues.Find a list of issue link types with Get issue link types.
    /// In the issueLinkType resource it defines and reports on issue link types.
    /// </summary>
    [JsonObject]
    public class IssueLinkType : BaseProperties<string>
    {
        /// <summary>
        /// Name of issue link type
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Inward relation link name
        /// </summary>
        [JsonProperty("inward")]
        public string Inward { get; set; }

        /// <summary>
        /// Outward relation link name
        /// </summary>
        [JsonProperty("outward")]
        public string Outward { get; set; }
    }
}
