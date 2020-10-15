# Dapplo.Jira <!-- include: readme.md -->
This is a simple REST based JIRA client, written for Greenshot, by using Dapplo.HttpExtension

- Documentation: [Dapplo.Jira](https://www.dapplo.net/Dapplo.Jira/index.html)

- Current build status: [![Build Status](https://dev.azure.com/Dapplo/Dapplo%20framework/_apis/build/status/dapplo.Dapplo.Jira?branchName=master)](https://dev.azure.com/Dapplo/Dapplo%20framework/_build/latest?definitionId=12&branchName=master)
- Coverage Status: [![Coverage Status](https://coveralls.io/repos/github/dapplo/Dapplo.Jira/badge.svg?branch=master)](https://coveralls.io/github/dapplo/Dapplo.Jira?branch=master)
- NuGet package: [![NuGet package](https://badge.fury.io/nu/Dapplo.Jira.svg)](https://badge.fury.io/nu/Dapplo.Jira)

If you like this project, maybe it saves you time or money, and want to support me to continue the development?
You can donate something via Paypal: https://www.paypal.me/dapplo

This client has support for:

* Issue (CRUD, comment, assign, issue types)
* Attachments (CRUD)
* Basic authorization, OAuth & session (via cookie)
* Search, with a JQL builder e.g. Where.And(Where.User.IsCurrentUser,Where.Text.Contains("Urgent"))
* Paging results
* Information on projects, transitions and users
* getting Avatars of users/projects and icons for Issue type 
* CRUD methods for the work-log (time spend on issues)
* CRUD methods for filters
* Some Agile methods, to get sprints/boards/issues. (work in progress)

For examples on how to use this library, I advice you to look at the test cases.

A example to find issues which are assigned to someone who is currently (or langer) not available, and remove the assignment
<!-- snippet: SearchExample -->
<a id='snippet-searchexample'></a>
```cs
var client = JiraClient.Create(TestJiraUri);
// Preferably use a "bot" user for maintenance
var username = Environment.GetEnvironmentVariable("jira_test_username");
var password = Environment.GetEnvironmentVariable("jira_test_password");
client.SetBasicAuthentication(username, password);

const string unavailableUser = "Robin Krom";
// Find all issues in a certain state and assigned to a user who is not available
var searchResult = await client.Issue.SearchAsync(Where.And(Where.Assignee.Is(unavailableUser), Where.Status.Is("Building")));

foreach (var issue in searchResult.Issues)
{
    // Remote the assignment, to make clear no-one is working on it
    await issue.AssignAsync(User.Nobody);
    // Comment the reason to the issue
    await issue.AddCommentAsync($"{unavailableUser} is currently not available.");
}
```
<sup><a href='/src/Dapplo.Jira.Tests/IssueTests.cs#L226-L244' title='Snippet source file'>snippet source</a> | <a href='#snippet-searchexample' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->
<!-- endInclude -->
