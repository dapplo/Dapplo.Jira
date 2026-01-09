// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dapplo.Jira.Query;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit;

namespace Dapplo.Jira.Tests;

public class VersionQueryTests
{
    public VersionQueryTests(ITestOutputHelper testOutputHelper)
    {
        LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
    }

    [Fact]
    public void TestIssueKeyIn()
    {
        var whereClause = Where.AffectedVersion.In("1.2.3", "1.2.4");
        Assert.Equal("affectedVersion in (\"1.2.3\", \"1.2.4\")", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyInEarliestUnreleasedVersion()
    {
        var whereClause = Where.AffectedVersion.InEarliestUnreleasedVersion("BUG");
        Assert.Equal("affectedVersion in earliestUnreleasedVersion(BUG)", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyInLatestReleasedVersion()
    {
        var whereClause = Where.AffectedVersion.InLatestReleasedVersion("BUG");
        Assert.Equal("affectedVersion in latestReleasedVersion(BUG)", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyInReleasedVersions()
    {
        var whereClause = Where.AffectedVersion.InReleasedVersions("BUG");
        Assert.Equal("affectedVersion in releasedVersions(BUG)", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyInReleasedVersions_Null()
    {
        var whereClause = Where.AffectedVersion.InReleasedVersions();
        Assert.Equal("affectedVersion in releasedVersions()", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyInUnreleasedVersions()
    {
        var whereClause = Where.AffectedVersion.InUnreleasedVersions("BUG");
        Assert.Equal("affectedVersion in unreleasedVersions(BUG)", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyInUnreleasedVersions_Null()
    {
        var whereClause = Where.AffectedVersion.InUnreleasedVersions();
        Assert.Equal("affectedVersion in unreleasedVersions()", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyIs()
    {
        var whereClause = Where.AffectedVersion.Is("1.2.3");
        Assert.Equal("affectedVersion = \"1.2.3\"", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyNotInEarliestUnreleasedVersion()
    {
        var whereClause = Where.AffectedVersion.Not.InEarliestUnreleasedVersion("BUG");
        Assert.Equal("affectedVersion not in earliestUnreleasedVersion(BUG)", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyNotInLatestReleasedVersion()
    {
        var whereClause = Where.AffectedVersion.Not.InLatestReleasedVersion("BUG");
        Assert.Equal("affectedVersion not in latestReleasedVersion(BUG)", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyNotInReleasedVersions()
    {
        var whereClause = Where.AffectedVersion.Not.InReleasedVersions("BUG");
        Assert.Equal("affectedVersion not in releasedVersions(BUG)", whereClause.ToString());
    }

    [Fact]
    public void TestIssueKeyNotInUnreleasedVersions()
    {
        var whereClause = Where.AffectedVersion.Not.InUnreleasedVersions("BUG");
        Assert.Equal("affectedVersion not in unreleasedVersions(BUG)", whereClause.ToString());
    }
}
