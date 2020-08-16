// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.HttpExtensions.ContentConverter;
using Dapplo.HttpExtensions.Extensions;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Query;
using Dapplo.Log;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Jira.Tests
{
	public class IssueTests : TestBase
	{
		public IssueTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
		{
		}

		[Fact]
		public async Task Test_EditIssue()
		{
			var updateIssue = new IssueEdit
			{
				Fields = new IssueFields
				{
					Description = "Test run at " + DateTime.Now.ToLocalTime()
				}
			};
			await Client.Issue.EditAsync(TestIssueKey, updateIssue);
		}

        [Fact]
        public async Task Test_GetIssueLinkTypes()
        {
            var issueLinkTypes = await Client.Issue.GetIssueLinkTypesAsync();
			Assert.NotEmpty(issueLinkTypes);
        }

        [Fact]
        public async Task Test_GetIssueTransitions()
        {
            var issueTransitions = await Client.Issue.GetIssueTransitionsAsync(TestIssueKey);
            Assert.NotEmpty(issueTransitions);
        }

		[Fact]
		public async Task Test_ChangeIssueLinks()
		{
			var issue = await Client.Issue.GetAsync(TestIssueKey);

            var issueLinkCount = issue.Fields.IssueLinks.Count;

			var issueLinkTypes = await Client.Issue.GetIssueLinkTypesAsync();


            var issueLink = new IssueLink()
            {
				InwardIssue = new LinkedIssue { Key = TestIssueKey },

				IssueLinkType = issueLinkTypes[0],
				OutwardIssue = new LinkedIssue{ Key = TestIssueKey2 }
            };

            await Client.Issue.CreateIssueLinkAsync(issueLink);
            issue = await Client.Issue.GetAsync(TestIssueKey);
			Assert.Equal(issueLinkCount+1, issue.Fields.IssueLinks.Count);
            foreach (var issueLinkToDelete in issue.Fields.IssueLinks)
            {
                await Client.Issue.DeleteIssueLinkAsync(issueLinkToDelete.Id);
            }
            issue = await Client.Issue.GetAsync(TestIssueKey);
			Assert.Empty(issue.Fields.IssueLinks);
		}

        [Fact]
        public async Task Test_IssueTransition()
        {
            var issueInitial = await Client.Issue.GetAsync(TestIssueKey);
            Assert.Equal("To Do", issueInitial.Fields.Status.Name);
			var possibleTransitions = await Client.Issue.GetIssueTransitionsAsync(TestIssueKey);
            Assert.NotEmpty(possibleTransitions);

			// Set the issue to the state "In Progress"
            var transitionForward = possibleTransitions.First(t => t.Name == "In Progress");
            await Client.Issue.TransitionAsync(TestIssueKey, transitionForward);
			var issueAfter = await Client.Issue.GetAsync(TestIssueKey);
			Assert.Equal("In Progress", issueAfter.Fields.Status.Name);

			var transitionBack = possibleTransitions.First(t => t.Name == issueInitial.Fields.Status.Name);
            await Client.Issue.TransitionAsync(TestIssueKey, transitionBack);
            issueInitial = await Client.Issue.GetAsync(TestIssueKey);
            Assert.Equal("To Do", issueInitial.Fields.Status.Name);
		}

		[Fact]
		public async Task Test_CreateIssue()
		{
			var meMyselfAndI = await Client.User.GetMyselfAsync();
			Assert.NotNull(meMyselfAndI);

			var issueTypes = await Client.Issue.GetIssueTypesAsync();
			var projects = await Client.Project.GetAllAsync();

			var bugIssueType = issueTypes.First(type => type.Name == "Bug");
			var projectForIssue = projects.First(digest => digest.Key == "DIT");

			var issueToCreate = new Issue
			{
				Fields = new IssueFields
				{
					Project = new Project {Key = projectForIssue.Key},
					IssueType = bugIssueType,
					Summary = "Some summary, this is a test",
					Description = "Some description, this is a test"
				}
			};

			var createdIssue = await Client.Issue.CreateAsync(issueToCreate);
			Assert.NotNull(createdIssue);
			Assert.NotNull(createdIssue.Key);
			// Remove again
			await Client.Issue.DeleteAsync(createdIssue.Key);
		}

        //[Fact]
		public async Task Test_CreateIssueWithCustomFields()
		{
			var meMyselfAndI = await Client.User.GetMyselfAsync();
			Assert.NotNull(meMyselfAndI);

			var issueTypes = await Client.Issue.GetIssueTypesAsync();
			var projects = await Client.Project.GetAllAsync();

			var bugIssueType = issueTypes.First(type => type.Name == "Bug");
			var projectForIssue = projects.First(digest => digest.Key == "DIT");

			var cfTextField = "customfield_10001";
			var cfLabelField = "customfield_10002";

            var issueToCreate = new Issue
			{
				Fields = new IssueFields
				{
					Project = new Project {Key = projectForIssue.Key},
					IssueType = bugIssueType,
					Summary = "Some summary, this is a test",
					Description = "Some description, this is a test",
					CustomFields =
					{
						{ cfTextField, "plain text" },
						{ cfLabelField, new [] {"label1 label2"} },
					}
				}
			};

			var createdIssue = await Client.Issue.CreateAsync(issueToCreate);
			Assert.NotNull(createdIssue);
			Assert.NotNull(createdIssue.Key);
			// Remove again
			await Client.Issue.DeleteAsync(createdIssue.Key);
		}

        [Fact]
		public async Task Test_GetIssueTypes()
		{
			var issueTypes = await Client.Issue.GetIssueTypesAsync();
			Assert.NotNull(issueTypes);
		}

		[Fact]
		public async Task Test_Assign()
		{
			var issueBeforeChanges = await Client.Issue.GetAsync(TestIssueKey);

			// assign to nobody
			await Client.Issue.AssignAsync(TestIssueKey, User.Nobody);

			// check
			var issueAssignedToNobody = await Client.Issue.GetAsync(TestIssueKey);
			Assert.Null(issueAssignedToNobody.Fields.Assignee);

			// Assign back to the initial user
			await Client.Issue.AssignAsync(TestIssueKey, issueBeforeChanges.Fields.Assignee);

			// check
			var issueAssignedToMe = await Client.Issue.GetAsync(TestIssueKey);
			Assert.Equal(issueAssignedToMe.Fields.Assignee, issueBeforeChanges.Fields.Assignee);
		}

		[Fact]
		public async Task Test_GetIssue()
		{
			var issue = await Client.Issue.GetAsync(TestIssueKey);
			Assert.NotNull(issue);
			Assert.NotNull(issue.Fields.IssueType);
			Assert.NotNull(issue.Fields.Comments.Elements);
			Assert.True(issue.Fields.TimeTracking.TimeSpentSeconds > 0);
			Assert.True(issue.Fields.CustomFields.Count > 0);
			Assert.True(issue.Fields.Comments.Elements.Count > 0);
		}

		[Fact]
		public async Task Test_GetPossibleTransitions()
		{
			var defaultJsonHttpContentConverterConfiguration = new DefaultJsonHttpContentConverterConfiguration
			{
				LogThreshold = 0
			};
			HttpBehaviour.Current.SetConfig(defaultJsonHttpContentConverterConfiguration);
			JiraConfig.ExpandGetTransitions = new[] {"transitions.fields"};
			var transitions = await Client.Issue.GetPossibleTransitionsAsync(TestIssueKey);
			Assert.NotNull(transitions);
			Assert.True(transitions.Count > 0);
			Assert.NotNull(transitions[0].PossibleFields);
		}

		[Fact]
		public async Task Test_Search()
		{
			var searchResult = await Client.Issue.SearchAsync(Where.Text.Contains("robin"));

			Assert.NotNull(searchResult);
			Assert.True(searchResult.Issues.Count > 0);

			foreach (var issue in searchResult.Issues)
			{
				Assert.NotNull(issue.Fields.Project);
				Assert.Null(issue.Changelog); //Should be null as no changelog was requested in the request
			}
		}

		[Fact]
		public async Task Test_Search_Paging()
		{
			// Create initial search
			string[] fields = {"summary", "status", "assignee", "key", "project", "summary"};
			var searchResult = await Client.Issue.SearchAsync(Where.Text.Contains("DPI"), fields: fields);
			// Loop over all results
			while (searchResult.Count > 0)
			{
				Assert.NotNull(searchResult);
				Assert.True(searchResult.Issues.Count > 0);
				Log.Info().WriteLine("Got {0} of {1} results, starting at index {2}, isLast: {3}", searchResult.Count, searchResult.Total, searchResult.StartAt, searchResult.IsLastPage);
				foreach (var issue in searchResult)
				{
					Log.Info().WriteLine("Found issue {0} - {1}", issue.Key, issue.Fields.Summary);
				}
				if (searchResult.IsLastPage)
				{
					break;
				}
				// Continue the search, by reusing the SearchParameter and taking the next page
				searchResult = await Client.Issue.SearchAsync(searchResult.SearchParameter, searchResult.NextPage);
			}
		}

		[Fact]
		public async Task Test_SearchWithChangelog()
		{
			var searchResult = await Client.Issue.SearchAsync(Where.Text.Contains("robin"), expand: new[] {"changelog"});

			Assert.NotNull(searchResult);
			Assert.True(searchResult.Issues.Count > 0);

			foreach (var issue in searchResult.Issues)
			{
				Assert.NotNull(issue.Fields.Project);
				Assert.NotNull(issue.Changelog);
			}
		}
	}
}