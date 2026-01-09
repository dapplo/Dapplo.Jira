# Dapplo.Jira<!-- include: readme.md -->
This is a simple REST based JIRA client, written for Greenshot, by using Dapplo.HttpExtension

- Documentation: [Dapplo.Jira](https://www.dapplo.net/Dapplo.Jira/index.html)

- Current build status: [![Build Status](https://github.com/dapplo/Dapplo.Jira/actions/workflows/build.yml/badge.svg)](https://github.com/dapplo/Dapplo.Jira/actions/workflows/build.yml)
- Coverage Status: [![Coverage Status](https://coveralls.io/repos/github/dapplo/Dapplo.Jira/badge.svg?branch=master)](https://coveralls.io/github/dapplo/Dapplo.Jira?branch=master)
- NuGet package: [![NuGet package](https://badge.fury.io/nu/Dapplo.Jira.svg)](https://badge.fury.io/nu/Dapplo.Jira)

If you like this project, maybe it saves you time or money, and want to support me to continue the development?
You can donate something via [GitHub Sponsors](https://github.com/sponsors/Lakritzator) or Paypal: https://www.paypal.me/dapplo

This client has support for:

* Issue (CRUD, comment, assign, issue types)
* Attachments (CRUD)
* Basic authorization, OAuth & session (via cookie)
* Search, with a JQL builder e.g. Where.And(Where.User.IsCurrentUser,Where.Text.Contains("Urgent"))
* Paging results
* Information on projects, transitions, users, versions/releases and components
* getting Avatars of users/projects and icons for Issue type 
* CRUD methods for the work-log (time spend on issues)
* CRUD methods for filters
* Some Agile methods, to get sprints/boards/issues. (work in progress)

For examples on how to use this library, I advice you to look at the test cases.

A example to find issues which are assigned to someone who is currently (or langer) not available, and remove the assignment
<!-- snippet: SearchExample -->
<a id='snippet-SearchExample'></a>
```cs
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
```
<sup><a href='/src/Dapplo.Jira.Tests/IssueTests.cs#L271-L291' title='Snippet source file'>snippet source</a> | <a href='#snippet-SearchExample' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->
<!-- endInclude -->
