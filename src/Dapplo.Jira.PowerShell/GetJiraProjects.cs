// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Management.Automation;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;
using Dapplo.Jira.PowerShell.Support;

namespace Dapplo.Jira.PowerShell
{
    /// <summary>
    ///     A Cmdlet which outputs the projects in the specified jira
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "JiraProjects")]
    [OutputType(typeof(ProjectDigest))]
    public class GetJiraProjects : JiraAsyncCmdlet
    {
        /// <summary>
        ///     Process the Projects output
        /// </summary>
        protected override async Task ProcessRecordAsync()
        {
            var projects = await this.JiraApi.Project.GetAllAsync().ConfigureAwait(false);
            foreach (var projectDigest in projects)
            {
                WriteObject(projectDigest);
            }
        }
    }
}
