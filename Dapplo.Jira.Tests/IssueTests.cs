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

#region using

using System.Linq;
using System.Threading.Tasks;
using Dapplo.HttpExtensions.ContentConverter;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Query;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Jira.Tests
{
	public class IssueTests : TestBase
	{
		public IssueTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
		{
		}

		[Fact]
		public async Task TestGetIssue()
		{
			var issue = await Client.Issue.GetAsync("BUG-2104");
			Assert.NotNull(issue);
			Assert.NotNull(issue.Fields.IssueType);
			Assert.NotNull(issue.Fields.Comments.Elements);
			Assert.True(issue.Fields.CustomFields.Count > 0);
			Assert.True(issue.Fields.Comments.Elements.Count > 0);
		}

		[Fact]
		public async Task TestGetPossibleTransitions()
		{
			DefaultJsonHttpContentConverter.Instance.Value.LogThreshold = 0;
			JiraConfig.ExpandGetTransitions = new[] {"transitions.fields"};
			var transitions = await Client.Issue.GetPossibleTransitionsAsync("BUG-1845");
			Assert.NotNull(transitions);
			Assert.True(transitions.Count > 0);
			Assert.NotNull(transitions[0].PossibleFields);
		}

		[Fact]
		public async Task TestSearch()
		{
			var searchResult = await Client.Issue.SearchAsync(Where.Text.Contains("robin"));

			Assert.NotNull(searchResult);
			Assert.NotNull(searchResult.Issues.Count > 0);

			foreach (var issue in searchResult.Issues)
			{
				Assert.NotNull(issue.Fields.Project);
			}
		}

		[Fact]
		public async Task TestAssign()
		{
			const string issueKey = "FEATURE-746";
			var issueBeforeChanges = await Client.Issue.GetAsync(issueKey);

			// assign to nobody
			await Client.Issue.AssignAsync(issueKey, User.Nobody);

			// check
			var issueAssignedToNobody = await Client.Issue.GetAsync(issueKey);
			Assert.Null(issueAssignedToNobody.Fields.Assignee);

			// Assign back to the initial user
			await Client.Issue.AssignAsync(issueKey, issueBeforeChanges.Fields.Assignee);

			// check
			var issueAssignedToMe = await Client.Issue.GetAsync(issueKey);
			Assert.True(issueAssignedToMe.Fields.Assignee.Name == issueBeforeChanges.Fields.Assignee.Name);
		}

		[Fact]
		public async Task GetIssueTypes()
		{
			var issueTypes = await Client.Issue.GetIssueTypesAsync();
			Assert.NotNull(issueTypes);
		}

		//[Fact]
		public async Task DeleteIssue()
		{
			// Remove again
			await Client.Issue.DeleteAsync("BUG-2118");
		}

		//[Fact]
		public async Task CreateIssue()
		{
			var issueTypes = await Client.Issue.GetIssueTypesAsync();
			var projects = await Client.Project.GetAllAsync();

			var bugIssueType = issueTypes.First(type => type.Name == "Bug");
			var projectForIssue = projects.First(digest => digest.Key == "BUG");
			var issueToCreate = new Issue
			{
				Fields = new IssueFields
				{
					IssueType = bugIssueType,
					Summary = "Some summary, this is a test",
					Description = "Some description, this is a test",
					Project = new Project
					{
						Id = projectForIssue.Id
					}
				}
			};
			var createdIssue = await Client.Issue.CreateAsync(issueToCreate);
			Assert.NotNull(createdIssue);
			Assert.NotNull(createdIssue.Key);
			// Remove again
			await Client.Issue.DeleteAsync(createdIssue.Key);
		}
	}
}