// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    [JsonObject]
    internal class JiraSession
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("value")] public string Value { get; set; }

        public override string ToString()
        {
            return $"{Name ?? string.Empty}={Value ?? string.Empty}";
        }
    }
}
