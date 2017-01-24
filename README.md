# Dapplo.Jira
This is a simple REST based JIRA client, written for Greenshot, by using Dapplo.HttpExtension

- Current build status: [![Build status](https://ci.appveyor.com/api/projects/status/d78ubenwypiwg3j4?svg=true)](https://ci.appveyor.com/project/dapplo/dapplo-jira)
- Coverage Status: [![Coverage Status](https://coveralls.io/repos/github/dapplo/Dapplo.Jira/badge.svg?branch=master)](https://coveralls.io/github/dapplo/Dapplo.Jira?branch=master)
- NuGet package: [![NuGet package](https://badge.fury.io/nu/Dapplo.Jira.svg)](https://badge.fury.io/nu/Dapplo.Jira)

This client has support for:
* Issue (CRUD, comment, assign, issue types)
* Attachments (CRUD)
* Basic authorization, OAuth & session (via cookie)
* Search, with a JQL builder e.g. Where.And(Where.User.IsCurrentUser,Where.Text.Contains("Urgent"))
* Information on projects, transitions and users
* getting Avatars of users/projects and icons for Issue type 
* CRUD methods for the work-log (time spend on issues)
* CRUD methods for filters

More to come.

For examples on how to use this library, I advice you to look at the test cases.

A simple exampe to find issues, and output their description
```
var jiraClient = JiraClient.Create(new Uri("https://jira"));
jiraClient.SetBasicAuthentication(username, password);
var searchResult = await jiraClient.Issue.SearchAsync(Where.Text.Contains("my text"));
foreach (var issue in searchResult.Issues)
{
	Debug.WriteLine(issue.Fields.Description);
}
```