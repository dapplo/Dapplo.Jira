// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Comment information
    ///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/attachment
    /// </summary>
    [JsonObject]
    public class RenderedComment : BaseProperties<long>
    {
        /// <summary>
        ///     Who created the comment
        /// </summary>
        [JsonProperty("author")]
        [ReadOnly(true)]
        public User Author { get; set; }

        /// <summary>
        ///     The text of the comment
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        ///     When was the comment created
        /// </summary>
        [JsonProperty("created")]
        [ReadOnly(true)]
        public string Created { get; set; }

        /// <summary>
        ///     Who updated the comment
        /// </summary>
        [JsonProperty("updateAuthor")]
        [ReadOnly(true)]
        public User UpdateAuthor { get; set; }

        /// <summary>
        ///     When was the comment updated
        /// </summary>
        [JsonProperty("updated")]
        [ReadOnly(true)]
        public string Updated { get; set; }

        /// <summary>
        ///     Is a comment visible
        /// </summary>
        [JsonProperty("visibility")]
        public Visibility Visibility { get; set; }
    }
}
