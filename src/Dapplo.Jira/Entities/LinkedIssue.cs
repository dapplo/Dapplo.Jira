// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    /// The ID or key of a linked issue.
    /// </summary>
    [JsonObject]
    public class LinkedIssue : BaseId<string>
    {
        /// <summary>
        /// Issue key of linked issue
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

    }
}