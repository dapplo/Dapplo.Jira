// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Issue information
///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/issue
/// </summary>
public class Issue : IssueWithFields<IssueFields>
{
}