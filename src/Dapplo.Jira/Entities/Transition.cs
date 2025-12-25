// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Transition information
/// </summary>
public class Transition : BaseId<long>
{
    /// <summary>
    ///     Name for this transition
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Possible fields for the transition
    /// </summary>
    [JsonPropertyName("fields")]
    public IDictionary<string, PossibleField> PossibleFields { get; set; }

    /// <summary>
    ///     To status for the transition
    /// </summary>
    [JsonPropertyName("to")]
    public Status To { get; set; }


    /// <summary>
    /// Details of the issue status after the transition.
    /// </summary>
    [JsonPropertyName("statusDetails")]
    public string StatusDetails { get; set; }

    /// <summary>
    /// Whether there is a screen associated with the issue transition.
    /// </summary>
    [JsonPropertyName("hasScreen")]
    public bool HasScreen { get; set; }

    /// <summary>
    /// Whether the issue transition is global, that is, the transition is applied to issues regardless of their status.
    /// </summary>
    [JsonPropertyName("isGlobal")]
    public bool IsGlobal { get; set; }

    /// <summary>
    /// Whether this is the initial issue transition for the workflow.
    /// </summary>
    [JsonPropertyName("isInitial")]
    public bool IsInitial { get; set; }

    /// <summary>
    /// Whether the transition is available to be performed.
    /// </summary>
    [JsonPropertyName("isAvailable")]
    public bool IsAvailable { get; set; }

    /// <summary>
    /// Whether the issue has to meet criteria before the issue transition is applied.
    /// </summary>
    [JsonPropertyName("isConditional")]
    public bool IsConditional { get; set; }
}