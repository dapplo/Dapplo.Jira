// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     User information
    ///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/user
    /// </summary>
    [JsonObject]
    public class User : IUserIdentifier
    {
        /// <summary>
        ///     Use this to specify nobody
        /// </summary>
        /// <returns>A User which can be used in e.g. AssignAsync to remove the assignee</returns>
        public static User Nobody { get; } = new User
        {
            Name = null
        };

        /// <summary>
        ///     Default assignee
        /// </summary>
        /// <returns>A User which can be used in e.g. AssignAsync to assign to the default user</returns>
        public static User Default { get; } = new User
        {
            Name = "-1"
        };

        /// <summary>
        ///     Value for the account ID
        /// </summary>
        [JsonProperty("accountId")]
        [ReadOnly(true)]
        public string AccountId { get; set; }

        /// <summary>
        ///     true if the user is active (license count)
        /// </summary>
        [JsonProperty("active")]
        [ReadOnly(true)]
        public bool Active { get; set; }

        /// <summary>
        ///     Avatar urls (links to 16x16, 24x24, 32x32, 48x48 icons) for this user
        /// </summary>
        [JsonProperty("avatarUrls")]
        [ReadOnly(true)]
        public AvatarUrls Avatars { get; set; }

        /// <summary>
        ///     Display name for the user
        /// </summary>
        [JsonProperty("displayName")]
        [ReadOnly(true)]
        public string DisplayName { get; set; }

        /// <summary>
        ///     Email address of the user
        /// </summary>
        [JsonProperty("emailAddress")]
        [ReadOnly(true)]
        public string EmailAddress { get; set; }

        /// <summary>
        ///     Name of the user
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Link to this item (self)
        /// </summary>
        [JsonProperty("self")]
        [ReadOnly(true)]
        public Uri Self { get; set; }
    }
}
