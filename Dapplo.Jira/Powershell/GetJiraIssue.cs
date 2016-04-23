//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.PowerShell.Jira
// 
//  Dapplo.PowerShell.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.PowerShell.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.PowerShell.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System.Management.Automation;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Powershell.Support;

#endregion

namespace Dapplo.Jira.Powershell
{
	/// <summary>
	/// A Cmdlet which processes the information of a Jira issue
	/// </summary>
	[Cmdlet(VerbsCommon.Get, "JiraIssue")]
	[OutputType(typeof (Fields))]
	public class GetJiraIssue : JiraAsyncCmdlet
	{
		/// <summary>
		/// Key for the issue that needs to be retrieved
		/// </summary>
		[Parameter(ValueFromPipeline = true, Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true)]
		public string IssueKey { get; set; }

		/// <summary>
		/// Override ProcessRecordAsync to get the issue data and output the object
		/// </summary>
		/// <returns></returns>
		protected override async Task ProcessRecordAsync()
		{
			var issue = await JiraApi.GetIssueAsync(IssueKey);
			WriteObject(issue.Fields);
		}
	}
}