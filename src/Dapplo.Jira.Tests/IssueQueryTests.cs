// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dapplo.Jira.Query;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit;

namespace Dapplo.Jira.Tests;

public class IssueQueryTests
{
    private const string TestIssueKey = "DIT-1";

    public IssueQueryTests(ITestOutputHelper testOutputHelper)
    {
        LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
    }

    [Fact]
    public void TestIssueKeyInIssueHistory()
    {
        var whereClause = Where.IssueKey.InIssueHistory();
        Assert.Equal("issueKey in issueHistory()", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyInLinkedIssues()
    {
        var whereClause = Where.IssueKey.InLinkedIssues(TestIssueKey);
        Assert.Equal($"issueKey in linkedIssues({TestIssueKey})", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyInVotedIssues()
    {
        var whereClause = Where.IssueKey.InVotedIssues();
        Assert.Equal("issueKey in votedIssues()", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyInWatchedIssues()
    {
        var whereClause = Where.IssueKey.InWatchedIssues();
        Assert.Equal("issueKey in watchedIssues()", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyNotInIssueHistory()
    {
        var whereClause = Where.IssueKey.Not.InIssueHistory();
        Assert.Equal("issueKey not in issueHistory()", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyNotInLinkedIssues()
    {
        var whereClause = Where.IssueKey.Not.InLinkedIssues(TestIssueKey);
        Assert.Equal($"issueKey not in linkedIssues({TestIssueKey})", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyNotInVotedIssues()
    {
        var whereClause = Where.IssueKey.Not.InVotedIssues();
        Assert.Equal("issueKey not in votedIssues()", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyNotInWatchedIssues()
    {
        var whereClause = Where.IssueKey.Not.InWatchedIssues();
        Assert.Equal("issueKey not in watchedIssues()", whereClause.ToString());
    }
}
