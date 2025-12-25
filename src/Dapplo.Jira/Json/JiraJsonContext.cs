// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if NET6_0_OR_GREATER

using System.Text.Json.Serialization;
using Dapplo.Jira.Entities;

namespace Dapplo.Jira.Json;

/// <summary>
/// JSON source generation context for Jira entities.
/// This provides AOT-friendly, high-performance serialization for .NET 6+
/// </summary>
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    GenerationMode = JsonSourceGenerationMode.Metadata)]
// Main entities
[JsonSerializable(typeof(Issue))]
[JsonSerializable(typeof(AgileIssue))]
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(Project))]
[JsonSerializable(typeof(Board))]
[JsonSerializable(typeof(Sprint))]
[JsonSerializable(typeof(Worklog))]
[JsonSerializable(typeof(Comment))]
[JsonSerializable(typeof(Attachment))]
[JsonSerializable(typeof(Filter))]
// Collections and results
[JsonSerializable(typeof(IList<Issue>))]
[JsonSerializable(typeof(IList<User>))]
[JsonSerializable(typeof(IList<Project>))]
[JsonSerializable(typeof(IList<ProjectDigest>))]
[JsonSerializable(typeof(IList<Board>))]
[JsonSerializable(typeof(IList<Sprint>))]
[JsonSerializable(typeof(SearchIssuesResult))]
[JsonSerializable(typeof(Transitions))]
// Supporting entities
[JsonSerializable(typeof(IssueFields))]
[JsonSerializable(typeof(AgileIssueFields))]
[JsonSerializable(typeof(ServerInfo))]
[JsonSerializable(typeof(Configuration))]
[JsonSerializable(typeof(SessionResponse))]
[JsonSerializable(typeof(LoginInfo))]
[JsonSerializable(typeof(Error))]
[JsonSerializable(typeof(Version))]
[JsonSerializable(typeof(Component))]
[JsonSerializable(typeof(Status))]
[JsonSerializable(typeof(Priority))]
[JsonSerializable(typeof(IssueType))]
[JsonSerializable(typeof(Resolution))]
[JsonSerializable(typeof(Transition))]
[JsonSerializable(typeof(ProjectDigest))]
[JsonSerializable(typeof(BoardConfiguration))]
[JsonSerializable(typeof(SprintReport))]
public partial class JiraJsonContext : JsonSerializerContext
{
}

#endif
