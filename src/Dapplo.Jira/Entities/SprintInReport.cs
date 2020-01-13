// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Sprint information within a Sprint Report
    /// </summary>
    [JsonObject]
    public class SprintInReport : Sprint
    {
        /// <summary>
        ///     Sequence of this sprint
        /// </summary>
        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        /// <summary>
        ///     Indicates the amount of pages attached to the sprint
        /// </summary>
        [JsonProperty("linkedPagesCount")]
        public int LinkedPagesCount { get; set; }

        /// <summary>
        ///     Indicated if the Sprint can be updated
        /// </summary>
        [JsonProperty("canUpdateSprint")]
        public bool CanUpdateSprint { get; set; }

        /// <summary>
        ///     Links to pages attached to the sprint
        /// </summary>
        [JsonProperty("remoteLinks")]
        public IEnumerable<RemoteLinks> RemoteLinks { get; set; }

        /// <summary>
        ///     Days remaining before the end of the sprint
        /// </summary>
        [JsonProperty("daysRemaining")]
        public int DaysRemaining { get; set; }
    }
}
