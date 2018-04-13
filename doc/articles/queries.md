# Jira Query Language (JQL) with Dapplo.Jira

This acticle is not written to explain you what JQL is and how to use it, but will explain what Dapplo.Jira brings to help you.
To read more about JQL, have a look here:
https://confluence.atlassian.com/jiracore/blog/2015/07/search-jira-like-a-boss-with-jql
https://confluence.atlassian.com/jirasoftwarecloud/advanced-searching-764478330.html
https://www.atlassian.com/blog/jira-software/jql-the-most-flexible-way-to-search-jira-14
https://www.atlassian.com/blog/jira-software/jql-secrets-and-shortcuts

If you already know how to use JQL in Jira, the following information should make it possible to use "dynamic" JQL in your .NET application.

Dapplo.Jira has implemented a JQL builder, using a fluent API, which makes it possible to create typed queries with confidence that they are generated correctly.
The JQL which is generated can be used to create filters or search directly.

After adding Dapplo.Jira to your project, you can directly start by using [Where](xref:Dapplo.Jira.Query.Where), this provides many static properties which represent certain fields to query for.
Using these static properties make the entry point clear, and make the API extendable.
Use Where.And or Where.Or to combine multiple parts together, making the complete JQL.

The Atlassian JQL: issuekey in issueHistory() and text ~ "dapplo" ORDER BY issuekey ASC 
Could be written like this: Where.And(Where.IssueKey.InIssueHistory(), Where.Text.Contains("dapplo")).OrderByAscending(Fields.IssueKey)
A ToString on the resultung object, generates the end query.