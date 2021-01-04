// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Base fields, used in pretty much every entity
    /// </summary>
    [JsonObject]
    public class BaseProperties<TId> : ReadOnlyBaseId<TId>
    {
        /// <summary>
        ///     Link to itself
        /// </summary>
        [JsonProperty("self")]
        [ReadOnly(true)]
        public Uri Self { get; set; }
    }
}
