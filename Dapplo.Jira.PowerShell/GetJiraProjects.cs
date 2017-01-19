//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Jira
// 
//  Dapplo.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#if NET45 || NET46

#region using

using System.Management.Automation;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;
using Dapplo.Jira.PowerShell.Support;

#endregion

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
			var projects = await JiraApi.Project.GetAllAsync();
			foreach (var projectDigest in projects)
			{
				WriteObject(projectDigest);
			}
		}
	}
}

#endif