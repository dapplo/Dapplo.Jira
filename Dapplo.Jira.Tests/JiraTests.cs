//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2016 Dapplo
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

#region using

using Dapplo.Jira.Entities;
using Dapplo.Jira.Tests.Logger;
using Dapplo.LogFacade;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

#endregion

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
		public async Task TestGetFavoriteFiltersAsync()
		{
			_jiraApi = new JiraApi(TestJiraUri);
			var filter = await _jiraApi.GetFavoriteFiltersAsync();
			Assert.NotNull(filter);
		}

		[Fact]
		public async Task TestGetIssueAsync()
		{
			_jiraApi = new JiraApi(TestJiraUri);
			var issue = await _jiraApi.GetIssueAsync("BUG-1845");
			Assert.NotNull(issue);
			Assert.NotNull(issue.Fields.Comments.Elements);
			Assert.True(issue.Fields.Comments.Elements.Count > 0);
		}

		[Fact]
		public async Task TestGetProjectsAsync()
		{
			_jiraApi = new JiraApi(TestJiraUri);
			var projects = await _jiraApi.GetProjectsAsync();

			Assert.NotNull(projects);
			Assert.NotNull(projects.Count > 0);

			foreach (var project in projects)
			{
				var avatar = await _jiraApi.GetAvatarAsync<Bitmap>(project.Avatar, AvatarSizes.Medium);
				Assert.True(avatar.Width == 24);

				var projectDetails = await _jiraApi.GetProjectAsync(project.Key);
				Assert.NotNull(projectDetails);
			}
		}

		[Fact]
		public async Task TestGetServerInfoAsync()
		{
			_jiraApi = new JiraApi(TestJiraUri);
			Assert.NotNull(_jiraApi);
			var serverInfo = await _jiraApi.GetServerInfoAsync();
			Assert.NotNull(serverInfo.Version);
			Assert.NotNull(serverInfo.ServerTitle);
			// This should be changed when the title changes
			Assert.Equal("Greenshot JIRA", serverInfo.ServerTitle);
			Debug.WriteLine($"Version {serverInfo.Version} - Title: {serverInfo.ServerTitle}");
		}

		[Fact]
		public async Task TestSearch()
		{
			_jiraApi = new JiraApi(TestJiraUri);
			var searchResult = await _jiraApi.SearchAsync("text ~ \"robin\"");

			Assert.NotNull(searchResult);
			Assert.NotNull(searchResult.Issues.Count > 0);

			foreach (var issue in searchResult.Issues)
			{
				Assert.NotNull(issue.Fields.Project);
			}
		}

		//[Fact]
		public async Task TestAttach()
		{
			_jiraApi = new JiraApi(TestJiraUri);
			_jiraApi.SetBasicAuthentication("username", "password");
			var attachments = await _jiraApi.AttachAsync("key", "Testing 1 2 3", "test.txt");
			Assert.NotNull(attachments);
			Assert.True(attachments.Count > 0);
			Assert.Equal("text/plain", attachments[0].MimeType);
		}

		//[Fact]
		public async Task TestGetProjectAsync()
		{
			_jiraApi = new JiraApi(TestJiraUri);
			var project = await _jiraApi.GetProjectAsync("BATCH");

			Assert.NotNull(project);
			Assert.NotNull(project.Roles.Count > 0);
		}
	}
}