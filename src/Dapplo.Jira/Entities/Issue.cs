// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Issue V3 information
///     See: https://docs.atlassian.com/jira/REST/latest/#api/3/issue
/// </summary>
[JsonObject]
public class Issue : IssueWithFields<IssueFields>
{
    /// <summary>
    /// Converts an IssueV3 instance to an IssueV2 instance, mapping standard and custom fields.
    /// </summary>
    /// <remarks>This conversion copies the relevant fields from the IssueV3 instance to the IssueV2 instance,
    /// including custom fields. Ensure that the IssueV3 instance is not null before calling this operator to avoid
    /// unexpected behavior.</remarks>
    /// <param name="issueV3">The IssueV3 instance to convert. If null, the method returns null.</param>
    public static explicit operator IssueV2(Issue issueV3)
    {
        if (issueV3 == null)
        {
            return null;
        }

        var issueV2 = new IssueV2
        {
            Id = issueV3.Id,
            Key = issueV3.Key,
            Self = issueV3.Self,
            Fields = new IssueFieldsV2
            {
                Summary = issueV3.Fields.Summary,
                Description = (string)issueV3.Fields.Description,
                IssueType = issueV3.Fields.IssueType,
                Project = issueV3.Fields.Project,
                Status = issueV3.Fields.Status,
                Priority = issueV3.Fields.Priority,
                Assignee = issueV3.Fields.Assignee,
                Reporter = issueV3.Fields.Reporter,
                Created = issueV3.Fields.Created,
                Updated = issueV3.Fields.Updated
            }
        };
        // Copy custom fields
        var customFields = issueV3.Fields.CustomFields;
        foreach (var customField in customFields)
        {
            issueV2.Fields.CustomFields[customField.Key] = customField.Value;
        }
        return issueV2;
    }
}
