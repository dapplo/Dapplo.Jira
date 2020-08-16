// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    /// Container to retrieve every possible IssueTransition for an issue
    /// </summary>
    [JsonObject]
    public class IssueTransitions
    {
        /// <summary>
        /// The possible issue transitions
        /// </summary>
        [JsonProperty("transitions")]
        public IList<IssueTransition> Values { get; set; }
    }
}