// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    /// This defines a remove operation
    /// </summary>
    [JsonObject]
    public class IssueUpdateOperationRemove : IIssueUpdateOperation
    {
        /// <summary>
        ///The values to remove
        /// </summary>
        [JsonProperty("remove")]
        public IDictionary<string, string> Remove { get; set; }
    }
}