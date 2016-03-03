/*
	Dapplo - building blocks for desktop applications
	Copyright (C) 2015-2016 Dapplo

	For more information see: http://dapplo.net/
	Dapplo repositories are hosted on GitHub: https://github.com/dapplo

	This file is part of Dapplo.Jira

	Dapplo.Jira is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	Dapplo.Jira is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/>.
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace Dapplo.Jira.Tests
{
	[TestClass]
	public class JiraTests
	{
		private static readonly Uri TestJiraUri = new Uri("https://greenshot.atlassian.net");
		[TestMethod]
		public async Task TestCreateAndInitializeAsync()
		{
			// Test against a well known JIRA
			var jiraApi = await JiraApi.CreateAndInitializeAsync(TestJiraUri);
			Assert.IsNotNull(jiraApi);
			Assert.IsNotNull(jiraApi.JiraVersion);
			Assert.IsNotNull(jiraApi.ServerTitle);
			// This should be changed when the title changes
			Assert.AreEqual("Greenshot JIRA", jiraApi.ServerTitle);
			Debug.WriteLine($"Version {jiraApi.JiraVersion} - Title: {jiraApi.ServerTitle}");
		}

		[TestMethod]
		public async Task TestProjectsAsync()
		{
			// Test against a well known JIRA
			var jiraApi = await JiraApi.CreateAndInitializeAsync(TestJiraUri);

			var projects = await jiraApi.ProjectsAsync();

			Assert.IsNotNull(projects);
			Assert.IsNotNull(projects.Count > 0);

			foreach (var project in projects)
			{
				var avatar = await jiraApi.AvatarAsync<Bitmap>(project.Avatar);
				Assert.IsTrue(avatar.Width == 48);

				var projectDetails = await jiraApi.ProjectAsync(project.Key);
				Assert.IsNotNull(projectDetails);
			}
		}
	}
}
