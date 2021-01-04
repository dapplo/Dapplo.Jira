// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Epic information
    /// </summary>
    [JsonObject]
    public class Epic : BaseProperties<long>
    {
        /// <summary>
        ///     Name of the Epic
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Epic summary
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; }

        /// <summary>
        ///     Is the epic done?
        /// </summary>
        [JsonProperty("done")]
        public bool IsDone { get; set; }

        /// <summary>
        ///     Color of the Epic
        /// </summary>
        [JsonProperty("color")]
        public EpicColor Color { get; set; }
    }
}
