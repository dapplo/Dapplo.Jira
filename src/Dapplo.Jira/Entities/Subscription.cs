// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Subscription information
    /// </summary>
    [JsonObject]
    public class Subscription : ReadOnlyBaseId<long>
    {
        /// <summary>
        ///     The user which subscribed
        /// </summary>
        [JsonProperty("user")]
        public User Subscriber { get; set; }
    }
}
