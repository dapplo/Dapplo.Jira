// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;
using Dapplo.Jira.Json;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Worklog information
    /// </summary>
    [JsonObject]
    public class Worklog : BaseProperties<string>
    {
        /// <summary>
        /// Default constructor for a work log
        /// </summary>
        public Worklog()
        {
        }

        /// <summary>
        /// Default constructor with a TimeSpan for the TimeSpentSeconds
        /// </summary>
        /// <param name="timeSpent">TimeSpan</param>
        public Worklog(TimeSpan timeSpent)
        {
            TimeSpentSeconds = (int)timeSpent.TotalSeconds;
        }

        /// <summary>
        ///     Author of this worklog
        /// </summary>
        [JsonProperty("author")]
        public User Author { get; set; }

        /// <summary>
        ///     Comment for this worklog
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }

        /// <summary>
        ///     When was the worklog created
        /// </summary>
        [JsonProperty("created")]
        [ReadOnly(true)]
        public DateTimeOffset? Created { get; set; }

        /// <summary>
        ///     When was the worklog started
        /// </summary>
        [JsonProperty("started")]
        [JsonConverter(typeof(CustomDateTimeOffsetConverter))]
        public DateTimeOffset? Started { get; set; }

        /// <summary>
        ///     Time spent in this worklog, this is a number and qualifier (h = hour, d = day etc)
        /// </summary>
        [JsonProperty("timeSpent")]
        public string TimeSpent { get; set; }

        /// <summary>
        ///     Time spent in this worklog, in seconds
        /// </summary>
        [JsonProperty("timeSpentSeconds")]
        public long? TimeSpentSeconds { get; set; }

        /// <summary>
        ///     Who updated this worklog, this cannot be updated
        /// </summary>
        [JsonProperty("updateAuthor")]
        [ReadOnly(true)]
        public User UpdateAuthor { get; set; }

        /// <summary>
        ///     When was the worklog updated, this cannot be updated
        /// </summary>
        [JsonProperty("updated")]
        [ReadOnly(true)]
        public DateTimeOffset? Updated { get; set; }

        /// <summary>
        ///     Visibility
        /// </summary>
        [JsonProperty("visibility")]
        public Visibility Visibility { get; set; }
    }
}
