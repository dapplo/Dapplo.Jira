// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Management.Automation;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;
using Dapplo.Jira.PowerShell.Support;

namespace Dapplo.Jira.PowerShell;

/// <summary>
///     A Cmdlet which processes the information of a Jira issue
/// </summary>
[Cmdlet(VerbsCommon.Get, "JiraIssue")]
[OutputType(typeof(IssueFields))]
public class GetJiraIssue : JiraAsyncCmdlet
{
    /// <summary>
    ///     Key for the issue that needs to be retrieved
    /// </summary>
    [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true)]
    public string IssueKey { get; set; }

    /// <summary>
    ///     Override ProcessRecordAsync to get the issue data and output the object
    /// </summary>
    /// <returns></returns>
    protected override async Task ProcessRecordAsync()
    {
        var issue = await this.JiraApi.Issue.GetAsync(IssueKey).ConfigureAwait(false);
        WriteObject(issue.Fields);
    }
}