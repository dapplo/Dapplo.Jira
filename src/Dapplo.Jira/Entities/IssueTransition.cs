﻿// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Describes a possible issue transition
    /// </summary>
    [JsonObject]
    public class IssueTransition : BaseId<string>
    {
        /// <summary>
        /// The name of the issue transition.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Details of the issue status after the transition.
        /// </summary>
        [JsonProperty("statusDetails")]
        public string StatusDetails { get; set; }

        /// <summary>
        /// Whether there is a screen associated with the issue transition.
        /// </summary>
        [JsonProperty("hasScreen")]
        public bool HasScreen { get; set; }

        /// <summary>
        /// Whether the issue transition is global, that is, the transition is applied to issues regardless of their status.
        /// </summary>
        [JsonProperty("isGlobal")]
        public bool IsGlobal { get; set; }

        /// <summary>
        /// Whether this is the initial issue transition for the workflow.
        /// </summary>
        [JsonProperty("isInitial")]
        public bool IsInitial { get; set; }

        /// <summary>
        /// Whether the transition is available to be performed.
        /// </summary>
        [JsonProperty("isAvailable")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Whether the issue has to meet criteria before the issue transition is applied.
        /// </summary>
        [JsonProperty("isConditional")]
        public bool IsConditional { get; set; }
    }
}