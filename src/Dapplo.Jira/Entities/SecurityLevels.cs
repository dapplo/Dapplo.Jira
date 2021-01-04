// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Security levels
    /// </summary>
    [JsonObject]
    public class SecurityLevels
    {
        /// <summary>
        ///     The actual list of security levels
        /// </summary>
        [JsonProperty("levels")]
        public IList<SecurityLevel> Levels { get; set; }
    }
}
