#region Dapplo 2017-2018 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2018 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Jira
// 
// Dapplo.Jira is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Jira is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#if NET451 || NET461

#region Usings

using System.Management.Automation;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;
using Dapplo.Jira.PowerShell.Support;

#endregion

namespace Dapplo.Jira.PowerShell
{
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
			var issue = await JiraApi.Issue.GetAsync(IssueKey).ConfigureAwait(false);
			WriteObject(issue.Fields);
		}
	}
}

#endif