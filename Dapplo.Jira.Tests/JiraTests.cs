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

using Xunit;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Dapplo.LogFacade;

namespace Dapplo.Jira.Tests
{
	public class JiraTests
	{
		// Test against a well known JIRA
		private static readonly Uri TestJiraUri = new Uri("https://greenshot.atlassian.net");

		private JiraApi _jiraApi;

		public JiraTests(ITestOutputHelper testOutputHelper)
		{
			XUnitLogger.RegisterLogger(testOutputHelper, LogLevel.Verbose);
		}

		[Fact]
		public async Task TestCreateAndInitializeAsync()
		{
			_jiraApi = await JiraApi.CreateAndInitializeAsync(TestJiraUri);
			Assert.NotNull(_jiraApi);
			Assert.NotNull(_jiraApi.JiraVersion);
			Assert.NotNull(_jiraApi.ServerTitle);
			// This should be changed when the title changes
			Assert.Equal("Greenshot JIRA", _jiraApi.ServerTitle);
			Debug.WriteLine($"Version {_jiraApi.JiraVersion} - Title: {_jiraApi.ServerTitle}");
		}

		[Fact]
		public async Task TestProjectsAsync()
		{
			_jiraApi = await JiraApi.CreateAndInitializeAsync(TestJiraUri);
			var projects = await _jiraApi.ProjectsAsync();

			Assert.NotNull(projects);
			Assert.NotNull(projects.Count > 0);

			foreach (var project in projects)
			{
				var avatar = await _jiraApi.AvatarAsync<Bitmap>(project.Avatar, AvatarSizes.ExtraLarge);
				Assert.True(avatar.Width == 48);

				var projectDetails = await _jiraApi.ProjectAsync(project.Key);
				Assert.NotNull(projectDetails);
			}
		}

		[Fact]
		public async Task TestSearch()
		{
			_jiraApi = await JiraApi.CreateAndInitializeAsync(TestJiraUri);
			var searchResult = await _jiraApi.SearchAsync("text ~ \"robin\"");

			Assert.NotNull(searchResult);
			Assert.NotNull(searchResult.Issues.Count > 0);

			foreach (var issue in searchResult.Issues)
			{
				Assert.NotNull(issue.Fields.Project);
			}
		}
	}
}
