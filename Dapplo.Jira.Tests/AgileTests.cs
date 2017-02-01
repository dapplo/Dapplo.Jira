//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Jira
// 
//  Dapplo.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Jira.Tests
{
	/// <summary>
	/// Tests for the Agile domain
	/// </summary>
	public class AgileTests : TestBase
	{
		public AgileTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
		{
		}

		[Fact]
		public async Task TestGetIssue()
		{
			var issue = await Client.Agile.GetIssueAsync("BUG-2125");
			Assert.NotNull(issue);
			Assert.NotNull(issue.Fields);
			Assert.NotNull(issue.Fields.ClosedSprints);
			Assert.NotNull(issue.Sprint);
			Assert.NotNull(issue.Fields.ClosedSprints.Count > 0);
		}

		[Fact]
		public async Task TestGetBoards()
		{
			var boards = await Client.Agile.GetBoardsAsync();
			Assert.NotNull(boards);
			Assert.True(boards.Any(board => board.Type == "scrum"));
		}

		[Fact]
		public async Task TestGetSprints()
		{
			var boards = await Client.Agile.GetBoardsAsync();
			var scrumboard = boards.First(board => board.Type == "scrum");
			var sprints = await Client.Agile.GetSprintsAsync(scrumboard.Id);
			Assert.NotNull(sprints);
			Assert.True(sprints.Any());
		}
	}
}