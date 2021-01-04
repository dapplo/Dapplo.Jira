// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Container for the Watches information.
    ///     To get the details, issue/<issue-key />/watchers needs to be called, expand doesn't seem to work!
    /// </summary>
    [JsonObject]
    public class Watches
    {
        /// <summary>
        ///     Is "current user" watching?
        /// </summary>
        [JsonProperty("isWatching")]
        public bool IsWatching { get; set; }

        /// <summary>
        ///     Link to the watch info itself
        /// </summary>
        [JsonProperty("self")]
        public Uri Self { get; set; }

        /// <summary>
        ///     Amount of users watching
        /// </summary>
        [JsonProperty("watchCount")]
        public int? WatchCount { get; set; }

        /// <summary>
        ///     The list of users who are watching
        /// </summary>
        [JsonProperty("watchers")]
        public IList<User> Watchers { get; set; }
    }
}
