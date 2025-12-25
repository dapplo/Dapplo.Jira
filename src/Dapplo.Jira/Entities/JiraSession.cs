// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

internal class JiraSession
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("value")] public string Value { get; set; }

    public override string ToString()
    {
        return $"{Name ?? string.Empty}={Value ?? string.Empty}";
    }
}