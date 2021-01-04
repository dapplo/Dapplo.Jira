// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    /// Part of the changelog
    /// </summary>
    [JsonObject]
    public class History : BaseProperties<long>
    {
        /// <summary>
        ///     Who created the comment
        /// </summary>
        [JsonProperty("author")]
        [ReadOnly(true)]
        public User Author { get; set; }

        /// <summary>
        ///     When was the comment created
        /// </summary>
        [JsonProperty("created")]
        [ReadOnly(true)]
        public DateTimeOffset? Created { get; set; }

        /// <summary>
        ///     list of fields that have been changed during this operation
        /// </summary>
        /// <value>
        ///     The items.
        /// </value>
        [JsonProperty("items")]
        [ReadOnly(true)]
        public IList<HistoryItem> Items { get; set; }
    }
}
