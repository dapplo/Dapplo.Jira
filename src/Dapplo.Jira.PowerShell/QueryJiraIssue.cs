// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Management.Automation;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;
using Dapplo.Jira.PowerShell.Support;

namespace Dapplo.Jira.PowerShell;

/// <summary>
///     A Cmdlet which queries a jira system for issues
/// </summary>
[Cmdlet(VerbsCommon.Get, "JiraQueryIssues")]
[OutputType(typeof(IssueFieldsV2))]
public class QueryJiraIssue : JiraAsyncCmdlet
{
    /// <summary>
    ///     Key for the issue that needs to be retrieved
    /// </summary>
    [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true)]
    public string Query { get; set; }

    /// <summary>
    ///     Override ProcessRecordAsync to get the issue data and output the object
    /// </summary>
    /// <returns></returns>
    protected override async Task ProcessRecordAsync()
    {
        var issues = await this.JiraApi.Issue.SearchAsync(Query).ConfigureAwait(false);
        WriteObject(issues, true);
    }
}