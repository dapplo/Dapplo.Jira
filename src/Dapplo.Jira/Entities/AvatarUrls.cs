﻿// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dapplo.Jira.Enums;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Avatar information, has 16x16,24x24,32x32,48x48 Urls
    ///     See: <a href="https://docs.atlassian.com/jira/REST/latest/#api/2/project">Jira project</a>
    /// </summary>
    [JsonObject]
    public class AvatarUrls
    {
        /// <summary>
        ///     Url to the 48x48 Avatar
        /// </summary>
        [JsonProperty("48x48")]
        public Uri ExtraLarge { get; set; }

        /// <summary>
        ///     Url to the 32x32 Avatar
        /// </summary>
        [JsonProperty("32x32")]
        public Uri Large { get; set; }

        /// <summary>
        ///     Url to the 24x24 Avatar
        /// </summary>
        [JsonProperty("24x24")]
        public Uri Medium { get; set; }

        /// <summary>
        ///     Url to the 16x16 Avatar
        /// </summary>
        [JsonProperty("16x16")]
        public Uri Small { get; set; }

        /// <summary>
        ///     Helper method to get the Uri for a certain avatar size
        /// </summary>
        /// <param name="avatarSize"></param>
        /// <returns>Uri</returns>
        /// <exception cref="ArgumentException">when an unknown avatar size is requested</exception>
        public Uri GetUri(AvatarSizes avatarSize)
        {
            switch (avatarSize)
            {
                case AvatarSizes.Small:
                    return Small;
                case AvatarSizes.Medium:
                    return Medium;
                case AvatarSizes.Large:
                    return Large;
                case AvatarSizes.ExtraLarge:
                    return ExtraLarge;
                default:
                    throw new ArgumentException($"Unknown avatar size: {avatarSize}", nameof(avatarSize));
            }
        }
    }
}
