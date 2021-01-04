// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    /// This defines a edit operation
    /// </summary>
    [JsonObject]
    public class IssueUpdateOperationEdit : IIssueUpdateOperation
    {
        /// <summary>
        /// The values to edit
        /// </summary>
        [JsonProperty("edit")]
        public IDictionary<string, string> Edit { get; set; }
    }
}
