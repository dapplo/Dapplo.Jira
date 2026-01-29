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

namespace Dapplo.Jira.Tests;

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
            Fields = new IssueFieldsV2
            {
                Description = "Test run at " + DateTime.Now.ToLocalTime()
            }
        };
        await Client.Issue.EditAsync(TestIssueKey, updateIssue, cancellationToken: TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Test_GetIssueLinkTypes()
    {
        var issueLinkTypes = await Client.Issue.GetIssueLinkTypesAsync(TestContext.Current.CancellationToken);
        Assert.NotEmpty(issueLinkTypes);
    }

    [Fact]
    public async Task Test_GetIssueTransitions()
    {
        var issueTransitions = await Client.Issue.GetTransitionsAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotEmpty(issueTransitions);
    }

    [Fact]
    public async Task Test_ChangeIssueLinks()
    {
        var issue = await Client.Issue.GetAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);

        var issueLinkCount = issue.Fields.IssueLinks.Count;

        var issueLinkTypes = await Client.Issue.GetIssueLinkTypesAsync(TestContext.Current.CancellationToken);


        var issueLink = new IssueLink()
        {
            InwardIssue = new LinkedIssue
            {
                Key = TestIssueKey
            },
            IssueLinkType = issueLinkTypes[0],
            OutwardIssue = new LinkedIssue
            {
                Key = TestIssueKey2
            }
        };

        await Client.Issue.CreateIssueLinkAsync(issueLink, cancellationToken: TestContext.Current.CancellationToken);
        issue = await Client.Issue.GetAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.Equal(issueLinkCount + 1, issue.Fields.IssueLinks.Count);
        foreach (var issueLinkToDelete in issue.Fields.IssueLinks)
        {
            await Client.Issue.DeleteIssueLinkAsync(issueLinkToDelete.Id, cancellationToken: TestContext.Current.CancellationToken);
        }

        issue = await Client.Issue.GetAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.Empty(issue.Fields.IssueLinks);
    }

    [Fact]
    public async Task Test_IssueTransition()
    {
        var issueInitial = await Client.Issue.GetAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.Equal("To Do", issueInitial.Fields.Status.Name);
        var possibleTransitions = await Client.Issue.GetTransitionsAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotEmpty(possibleTransitions);

        // Set the issue to the state "In Progress"
        var transitionForward = possibleTransitions.First(t => t.Name == "In Progress");
        await Client.Issue.TransitionAsync(TestIssueKey, transitionForward, cancellationToken: TestContext.Current.CancellationToken);
        var issueAfter = await Client.Issue.GetAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.Equal("In Progress", issueAfter.Fields.Status.Name);

        var transitionBack = possibleTransitions.First(t => t.Name == issueInitial.Fields.Status.Name);
        await Client.Issue.TransitionAsync(TestIssueKey, transitionBack, cancellationToken: TestContext.Current.CancellationToken);
        issueInitial = await Client.Issue.GetAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.Equal("To Do", issueInitial.Fields.Status.Name);
    }

    [Fact]
    public async Task Test_CreateIssue()
    {
        var meMyselfAndI = await Client.User.GetMyselfAsync(TestContext.Current.CancellationToken);
        Assert.NotNull(meMyselfAndI);

        var issueTypes = await Client.Issue.GetIssueTypesAsync(TestContext.Current.CancellationToken);
        var projects = await Client.Project.GetAllAsync(null, TestContext.Current.CancellationToken);

        var bugIssueType = issueTypes.First(type => type.Name == "Bug");
        var projectForIssue = projects.First(digest => digest.Key == TestProjectKey);

        var issueToCreate = new Issue
        {
            Fields = new IssueFields
            {
                Project = new Project
                {
                    Key = projectForIssue.Key
                },
                IssueType = bugIssueType,
                Summary = "Some summary, this is a test",
                Description = AdfDocument.FromText("Some description, this is a test")
            }
        };

        var createdIssue = await Client.Issue.CreateAsync(issueToCreate, TestContext.Current.CancellationToken);
        Assert.NotNull(createdIssue);
        Assert.NotNull(createdIssue.Key);
        // Remove again
        await Client.Issue.DeleteAsync(createdIssue.Key, cancellationToken: TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Test_Create_and_Retrieve_IssueWithCustomFields()
    {
        var meMyselfAndI = await Client.User.GetMyselfAsync(TestContext.Current.CancellationToken);
        Assert.NotNull(meMyselfAndI);

        var issueTypes = await Client.Issue.GetIssueTypesAsync(TestContext.Current.CancellationToken);
        var projects = await Client.Project.GetAllAsync(cancellationToken: TestContext.Current.CancellationToken);

        var bugIssueType = issueTypes.First(type => type.Name == "Bug");
        var projectForIssue = projects.First(digest => digest.Key == TestProjectKey);

        var cfTextField = "customfield_10001"; // Make sure this custom field is created with: Configure 'Text Field (single line)' Field
        var cfTextFieldValue = "plain text";
        var cfLabelField = "customfield_10002"; // Make sure this custom field is created with: Configure 'Labels' Field
        var cfLabelFieldValue = new[]
        {
            "label1",
            "label2"
        };

        // Translate custom field names to ids

        var fields = await Client.Server.GetFieldsAsync(TestContext.Current.CancellationToken);
        var cfTextFieldId = fields.First(f => f.Name == cfTextField).Id;
        var cfLabelFieldId = fields.First(f => f.Name == cfLabelField).Id;

        var issueToCreate = new Issue
            {
                Fields = new IssueFields
                {
                    Project = new Project
                    {
                        Key = projectForIssue.Key
                    },
                    IssueType = bugIssueType,
                    Summary = "Some summary, this is a test",
                    Description = AdfDocument.FromText("Some description, this is a test")
                }
            }
            .AddCustomField(cfTextFieldId, cfTextFieldValue)
            .AddCustomField(cfLabelFieldId, cfLabelFieldValue);

        var createdIssue = await Client.Issue.CreateAsync(issueToCreate, TestContext.Current.CancellationToken);
        Assert.NotNull(createdIssue);
        Assert.NotNull(createdIssue.Key);

        try
        {
            var testIssue = await Client.Issue.GetAsync(createdIssue.Key, cancellationToken: TestContext.Current.CancellationToken);
            Assert.Equal(cfTextFieldValue, testIssue.GetCustomField(cfTextFieldId));
            Assert.Equal(cfLabelFieldValue, testIssue.GetCustomField<string[]>(cfLabelFieldId));
        }
        finally
        {
            // Remove again
            await Client.Issue.DeleteAsync(createdIssue.Key, cancellationToken: TestContext.Current.CancellationToken);
        }

    }

    [Fact]
    public async Task Test_GetIssueTypes()
    {
        var issueTypes = await Client.Issue.GetIssueTypesAsync(TestContext.Current.CancellationToken);
        Assert.NotNull(issueTypes);
    }

    [Fact]
    public async Task Test_Assign()
    {
        var issueBeforeChanges = await Client.Issue.GetAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);

        // assign to nobody
        await Client.Issue.AssignAsync(TestIssueKey, User.Nobody, cancellationToken: TestContext.Current.CancellationToken);

        // check
        var issueAssignedToNobody = await Client.Issue.GetAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.Null(issueAssignedToNobody.Fields.Assignee);

        // Assign back to the initial user
        await Client.Issue.AssignAsync(TestIssueKey, issueBeforeChanges.Fields.Assignee, cancellationToken: TestContext.Current.CancellationToken);

        // check
        var issueAssignedToMe = await Client.Issue.GetAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.Equal(issueAssignedToMe.Fields.Assignee, issueBeforeChanges.Fields.Assignee);
    }

    [Fact]
    public async Task Test_GetWatchers()
    {
        var watchers = await Client.Issue.GetWatchersAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(watchers);
        Assert.NotNull(watchers.Self);
        Assert.True(watchers.WatchCount >= 0);
    }

    [Fact]
    public async Task Test_AddAndRemoveWatcher()
    {
        var meMyselfAndI = await Client.User.GetMyselfAsync(TestContext.Current.CancellationToken);
        Assert.NotNull(meMyselfAndI);

        var watchersBefore = await Client.Issue.GetWatchersAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        var initialWatchCount = watchersBefore.WatchCount ?? 0;

        // Determine the identifier to use (accountId for Cloud, username for Server)
        var userIdentifier = !string.IsNullOrEmpty(meMyselfAndI.AccountId) ? meMyselfAndI.AccountId : meMyselfAndI.Name;

        // Add watcher
        await Client.Issue.AddWatcherAsync(TestIssueKey, userIdentifier, cancellationToken: TestContext.Current.CancellationToken);

        // Verify watcher was added
        var watchersAfterAdd = await Client.Issue.GetWatchersAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.True(watchersAfterAdd.WatchCount >= initialWatchCount);

        // Remove watcher
        await Client.Issue.RemoveWatcherAsync(TestIssueKey, userIdentifier, cancellationToken: TestContext.Current.CancellationToken);

        // Verify watcher was removed
        var watchersAfterRemove = await Client.Issue.GetWatchersAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.Equal(initialWatchCount, watchersAfterRemove.WatchCount ?? 0);
    }


    [Fact]
    public async Task Test_GetIssue()
    {
        JiraConfig.ExpandGetIssue = new[]
        {
            "renderedFields"
        };
        var issue = await Client.Issue.GetAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(issue);
        Assert.NotNull(issue.Fields.IssueType);
        Assert.NotNull(issue.Fields.Comments.Elements);
        Assert.True(issue.Fields.TimeTracking.TimeSpentSeconds > 0);
        Assert.True(issue.Fields.CustomFields.Count > 0);
        Assert.True(issue.Fields.Comments.Elements.Count > 0);
    }

    [Fact]
    public async Task Test_GetIssue_Parent()
    {
        var issue = await Client.Issue.GetAsync(TestSubTaskIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(issue);
        Assert.NotNull(issue.Fields.Parent);
    }

    [Fact]
    public async Task Test_GetPossibleTransitions()
    {
        var defaultJsonHttpContentConverterConfiguration = new DefaultJsonHttpContentConverterConfiguration
        {
            LogThreshold = 0
        };
        HttpBehaviour.Current.SetConfig(defaultJsonHttpContentConverterConfiguration);
        JiraConfig.ExpandGetTransitions = new[]
        {
            "transitions.fields"
        };
        var transitions = await Client.Issue.GetTransitionsAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(transitions);
        Assert.True(transitions.Count > 0);
        Assert.NotNull(transitions[0].PossibleFields);
    }

    [Fact]
    public async Task Test_SearchSnippet()
    {
        // begin-snippet: SearchExample
        // Preferably use a "bot" user for maintenance
        var username = Environment.GetEnvironmentVariable("jira_test_username");
        var password = Environment.GetEnvironmentVariable("jira_test_password");
        var client = JiraClient
            .Create(TestJiraUri)
            .SetBasicAuthentication(username, password);

        const string unavailableUser = "Robin Krom";
        // Find all issues in a certain state and assigned to a user who is not available
        var searchResult = await client.Issue.SearchAsync(Where.And(Where.Assignee.Is(unavailableUser), Where.Status.Is("Building")), cancellationToken: TestContext.Current.CancellationToken);

        foreach (var issue in searchResult.Issues)
        {
            // Remote the assignment, to make clear no-one is working on it
            await issue.AssignAsync(User.Nobody, cancellationToken: TestContext.Current.CancellationToken);
            // Comment the reason to the issue
            await issue.AddCommentAsync($"{unavailableUser} is currently not available.", cancellationToken: TestContext.Current.CancellationToken);
        }

        // end-snippet
    }

    [Fact]
    public async Task Test_Search()
    {
        var searchResult = await Client.Issue.SearchAsync(Where.Text.Contains("robin"), cancellationToken: TestContext.Current.CancellationToken);

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
        string[] fields =
        {
            "summary", "status", "assignee", "key", "project", "summary"
        };
        var searchResult = await Client.Issue.SearchAsync(Where.Text.Contains("DPI"), fields: fields, cancellationToken: TestContext.Current.CancellationToken);
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
            searchResult = await Client.Issue.SearchAsync(searchResult.SearchParameter, searchResult.NextPage, cancellationToken: TestContext.Current.CancellationToken);
        }
    }

    [Fact]
    public async Task Test_SearchWithChangelog()
    {
        var searchResult =
            await Client.Issue.SearchAsync(Where.Text.Contains("robin"), expand: new[]
            {
                "changelog"
            }, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(searchResult);
        Assert.True(searchResult.Issues.Count > 0);

        foreach (var issue in searchResult.Issues)
        {
            Assert.NotNull(issue.Fields.Project);
            Assert.NotNull(issue.Changelog);
        }
    }
}
