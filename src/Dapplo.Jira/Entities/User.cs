// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     User information
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/user
/// </summary>
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
    [JsonPropertyName("accountId")]
    [ReadOnly(true)]
    public string AccountId { get; set; }

    /// <summary>
    ///     true if the user is active (license count)
    /// </summary>
    [JsonPropertyName("active")]
    [ReadOnly(true)]
    public bool Active { get; set; }

    /// <summary>
    ///     Avatar urls (links to 16x16, 24x24, 32x32, 48x48 icons) for this user
    /// </summary>
    [JsonPropertyName("avatarUrls")]
    [ReadOnly(true)]
    public AvatarUrls Avatars { get; set; }

    /// <summary>
    ///     Display name for the user
    /// </summary>
    [JsonPropertyName("displayName")]
    [ReadOnly(true)]
    public string DisplayName { get; set; }

    /// <summary>
    ///     Email address of the user
    /// </summary>
    [JsonPropertyName("emailAddress")]
    [ReadOnly(true)]
    public string EmailAddress { get; set; }

    /// <summary>
    ///     Name of the user
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Link to this item (self)
    /// </summary>
    [JsonPropertyName("self")]
    [ReadOnly(true)]
    public Uri Self { get; set; }
}