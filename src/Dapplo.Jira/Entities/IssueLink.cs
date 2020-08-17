// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Describes a link between Jira issues
    /// </summary>
    [JsonObject]
    public class IssueLink : ReadOnlyBaseId<string>
    {
        /// <summary>
        /// The comment for the issue link
        /// </summary>
        [JsonProperty("comment")]
        public Comment Comment { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("type")]
        public IssueLinkType IssueLinkType { get; set; }

        /// <summary>
        /// This describes the issue where the issue link if pointing to
        /// </summary>
        [JsonProperty("outwardIssue")]
        public LinkedIssue OutwardIssue { get; set; }

        /// <summary>
        /// This describes the issue which is pointing to this
        /// </summary>
        [JsonProperty("inwardIssue")]
        public LinkedIssue InwardIssue { get; set; }
    }
}