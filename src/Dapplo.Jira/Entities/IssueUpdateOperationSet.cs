// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    /// This defines a set operation
    /// </summary>
    [JsonObject]
    public class IssueUpdateOperationSet : IIssueUpdateOperation
    {
        /// <summary>
        /// The values to set
        /// </summary>
        [JsonProperty("set")]
        public IDictionary<string, string> Set { get; set; }
    }
}