# Dapplo.Jira
This is a simple REST based JIRA client, written for Greenshot, by using Dapplo.HttpExtension

- Current build status: [![Build Status](https://dev.azure.com/Dapplo/Dapplo%20framework/_apis/build/status/dapplo.Dapplo.Jira?branchName=master)](https://dev.azure.com/Dapplo/Dapplo%20framework/_build/latest?definitionId=12&branchName=master)
- Coverage Status: [![Coverage Status](https://coveralls.io/repos/github/dapplo/Dapplo.Jira/badge.svg?branch=master)](https://coveralls.io/github/dapplo/Dapplo.Jira?branch=master)
- NuGet package: [![NuGet package](https://badge.fury.io/nu/Dapplo.Jira.svg)](https://badge.fury.io/nu/Dapplo.Jira)

If you like this project, maybe it saves you time or money, and want to support me to continue the development?
You can donate something via [GitHub Sponsors](https://github.com/sponsors/Lakritzator) or [Paypal](https://www.paypal.me/dapplo)

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

A exampe to find issues which are assigned to someone who is currently (or langer) not available, and remove the assignment
snippet: SearchExample
