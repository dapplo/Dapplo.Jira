#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Jira
// 
// Dapplo.Jira is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Jira is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings

using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit;
using Xunit.Abstractions;
using Dapplo.Jira.Query;

#endregion

namespace Dapplo.Jira.Tests
{
	public class VersionQueryTests
	{
		public VersionQueryTests(ITestOutputHelper testOutputHelper)
		{
			LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
		}

		[Fact]
		public void TestIssueKeyIs()
		{
			var whereClause = Where.AffectedVersion.Is("1.2.3");
			Assert.Equal("affectedVersion = \"1.2.3\"", whereClause.ToString());
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
		public void TestIssueKeyNotInEarliestUnreleasedVersion()
		{
			var whereClause = Where.AffectedVersion.Not.InEarliestUnreleasedVersion("BUG");
			Assert.Equal("affectedVersion not in earliestUnreleasedVersion(BUG)", whereClause.ToString());
		}

		[Fact]
		public void TestIssueKeyInLatestReleasedVersion()
		{
			var whereClause = Where.AffectedVersion.InLatestReleasedVersion("BUG");
			Assert.Equal("affectedVersion in latestReleasedVersion(BUG)", whereClause.ToString());
		}

		[Fact]
		public void TestIssueKeyNotInLatestReleasedVersion()
		{
			var whereClause = Where.AffectedVersion.Not.InLatestReleasedVersion("BUG");
			Assert.Equal("affectedVersion not in latestReleasedVersion(BUG)", whereClause.ToString());
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
		public void TestIssueKeyNotInReleasedVersions()
		{
			var whereClause = Where.AffectedVersion.Not.InReleasedVersions("BUG");
			Assert.Equal("affectedVersion not in releasedVersions(BUG)", whereClause.ToString());
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
		public void TestIssueKeyNotInUnreleasedVersions()
		{
			var whereClause = Where.AffectedVersion.Not.InUnreleasedVersions("BUG");
			Assert.Equal("affectedVersion not in unreleasedVersions(BUG)", whereClause.ToString());
		}
	}
}