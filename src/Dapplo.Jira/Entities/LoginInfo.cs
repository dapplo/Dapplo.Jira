// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Jira login info
    /// </summary>
    [JsonObject]
    public class LoginInfo
    {
        /// <summary>
        ///     Failed login count
        /// </summary>
        [JsonProperty("failedLoginCount")]
        public int? FailedLoginCount { get; set; }

        /// <summary>
        ///     Last failed login time
        /// </summary>
        [JsonProperty("lastFailedLoginTime")]
        public string LastFailedLoginTime { get; set; }

        /// <summary>
        ///     Login count
        /// </summary>
        [JsonProperty("loginCount")]
        public int? LoginCount { get; set; }

        /// <summary>
        ///     Previous login time
        /// </summary>
        [JsonProperty("previousLoginTime")]
        public string PreviousLoginTime { get; set; }
    }
}
