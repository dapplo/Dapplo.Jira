// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Base id, used in pretty much every entity
    ///     This is used where the ID is only de-serialized, and not sent when serializing
    /// </summary>
    [JsonObject]
    public class ReadOnlyBaseId<TId> : IComparable where TId : IComparable
    {
        /// <summary>
        ///     Id of this entity
        /// </summary>
        [JsonProperty("id")]
        [ReadOnly(true)]
        public TId Id { get; set; }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj is not ReadOnlyBaseId<TId> otherBaseId)
            {
                return 1;   // All instances are greater than null
            }
            return Id.CompareTo(otherBaseId.Id);
        }
    }
}
