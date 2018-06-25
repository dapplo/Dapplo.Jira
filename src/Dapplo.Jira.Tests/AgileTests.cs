#region Dapplo 2017-2018 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2018 Dapplo
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

using System.Linq;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Enums;
using Dapplo.Jira.Query;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Jira.Tests
{
	/// <summary>
	///     Tests for the Agile domain
	/// </summary>
	public class AgileTests : TestBase
	{
		public AgileTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
		{
		}

		[Fact]
		public async Task TestGetBacklog()
		{
			var boards = await Client.Agile.GetBoardsAsync();
			var scrumboard = boards.First(board => board.Type == BoardTypes.Scrum);
			var backlog = await Client.Agile.GetBacklogAsync(scrumboard.Id);
			Assert.NotNull(backlog);
			Assert.True(backlog.Any());
		}

		[Fact]
		public async Task TestCreateBoard()
		{
			// Preparations
			var boards = await Client.Agile.GetBoardsAsync();
			foreach (var board in boards.Where(board => board.Name.Contains("DapploTest")))
			{
				await Client.Agile.DeleteBoardAsync(board.Id);
			}

			var filters = await Client.Filter.GetFiltersAsync();
			foreach (var testFilter in filters.Where(filter => filter.Name.Contains("DapploTest")))
			{
				await Client.Filter.DeleteAsync(testFilter);
			}

			var boardFilter = new Filter
			{
				Jql = Where.Project.Is("DIT").ToString(),
				Name = "DapploTest"
			};
			boardFilter = await Client.Filter.CreateAsync(boardFilter);

			var testBoard = new Board
			{
				Name = "DapploTestBoard",
				FilterId = boardFilter.Id,
				Type = BoardTypes.Scrum
			};

			testBoard = await Client.Agile.CreateBoardAsync(testBoard);
			try
			{
				var issues = await Client.Agile.GetIssuesOnBoardAsync(testBoard.Id);
				Assert.True(issues.Any());
			}
			finally
			{
				await Client.Agile.DeleteBoardAsync(testBoard.Id);
			}
		}


		[Fact]
		public async Task TestGetBoards()
		{
			var boards = await Client.Agile.GetBoardsAsync();
			Assert.NotNull(boards);
			Assert.Contains(boards, board => board.Type == BoardTypes.Scrum);
		}

		[Fact]
		public async Task TestGetIssue()
		{
			var boards = await Client.Agile.GetBoardsAsync();
			var scrumboard = boards.First(board => board.Type == BoardTypes.Scrum);
			var boardConfiguration = await Client.Agile.GetBoardConfigurationAsync(scrumboard.Id);
			var issue = await Client.Agile.GetIssueAsync(TestIssueKey);
			Assert.NotNull(issue);
			Assert.NotNull(issue.Fields);
			Assert.NotNull(issue.Fields.ClosedSprints);
			Assert.NotNull(issue.Sprint);
			Assert.True(issue.Fields.ClosedSprints.Count > 0);

			Assert.True(issue.GetEstimation(boardConfiguration) == 0);
			Assert.NotNull(issue.GetRank(boardConfiguration));
		}

		[Fact]
		public async Task TestGetIssues()
		{
			var boards = await Client.Agile.GetBoardsAsync();
			var scrumboard = boards.First(board => board.Type == BoardTypes.Scrum);
			var issuesOnBoard = await Client.Agile.GetIssuesOnBoardAsync(scrumboard.Id);
			Assert.NotNull(issuesOnBoard);
			Assert.True(issuesOnBoard.Any());
		}

		[Fact]
		public async Task TestGetIssuesInSprint()
		{
			var boards = await Client.Agile.GetBoardsAsync();
			var scrumboard = boards.First(board => board.Type == BoardTypes.Scrum);
			var sprints = await Client.Agile.GetSprintsAsync(scrumboard.Id);
			Assert.NotNull(sprints);
			Assert.True(sprints.Any());
			var issuesInSprint = await Client.Agile.GetIssuesInSprintAsync(scrumboard.Id, sprints.Last().Id);
			Assert.True(issuesInSprint.Any());
		}

		[Fact]
		public async Task TestGetSprints()
		{
			var boards = await Client.Agile.GetBoardsAsync();
			var scrumboard = boards.First(board => board.Type == BoardTypes.Scrum);
			var sprints = await Client.Agile.GetSprintsAsync(scrumboard.Id);
			Assert.NotNull(sprints);
			Assert.True(sprints.Any());
		}
	}
}