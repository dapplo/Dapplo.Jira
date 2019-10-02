// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2019 Dapplo
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

using System;
using System.Management.Automation;
using System.Threading.Tasks;

namespace Dapplo.Jira.PowerShell.Support
{
	/// <summary>
	///     This is the base class for all (most?) Jira CmdLets
	///     It will create the JiraApi instance, so the derriving class only needs to implement the logic
	/// </summary>
	public class JiraAsyncCmdlet : AsyncCmdlet
	{
		/// <summary>
		///     The Jira API which should be used to get information
		/// </summary>
		protected IJiraClient JiraApi;

		/// <summary>
		///     Url to the Jira system
		/// </summary>
		[Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
		public Uri JiraUri { get; set; }

		/// <summary>
		///     Password for the user
		/// </summary>
		[Parameter(ValueFromPipelineByPropertyName = true)]
		public string Password { get; set; }

		/// <summary>
		///     User for the Jira connection
		/// </summary>
		[Parameter(ValueFromPipelineByPropertyName = true)]
		public string Username { get; set; }

		/// <summary>
		///     Override the BeginProcessingAsync to connect to our jira
		/// </summary>
		protected override Task BeginProcessingAsync()
		{
			JiraApi = JiraClient.Create(JiraUri);
			if (Username != null)
			{
				JiraApi.SetBasicAuthentication(Username, Password);
			}
			return Task.FromResult(true);
		}
	}
}