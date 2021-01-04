// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Visibility information
    /// </summary>
    [JsonObject]
    public class Visibility
    {
        /// <summary>
        ///     Type for the visibility
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     Value of the visibility
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Factory for a role visibility
        /// </summary>
        /// <param name="role">string with the name of the role</param>
        /// <returns>Visibility</returns>
        public static Visibility ForRole(string role)
        {
            return new Visibility
            {
                Type = "role",
                Value = role
            };
        }

        /// <summary>
        /// Factory for a group visibility
        /// </summary>
        /// <param name="group">string with the name of the group</param>
        /// <returns>Visibility</returns>
        public static Visibility ForGroup(string group)
        {
            return new Visibility
            {
                Type = "group",
                Value = group
            };
        }
    }
}
