#region Dapplo 2017-2019 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2019 Dapplo
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

using Dapplo.Jira.Query;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Jira.Tests
{
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
}