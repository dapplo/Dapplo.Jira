#region Dapplo 2017 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017 Dapplo
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
using Dapplo.Jira.Enums;
using Xunit;
using Xunit.Abstractions;

#endregion
namespace Dapplo.Jira.Tests
{
    /// <summary>
    ///     Tests for the Greenhopper domain
    /// </summary>
    public class GreenhopperTests : TestBase
    {
        public GreenhopperTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        public async Task TestGetSprintReport()
        {
            // Arrange
            var selectedBoard = "greenshot releases";
            var selectedSprint = "greenshot 1.2.9 bf1";

            var boards = await Client.Agile.GetBoardsAsync();
            var scrumboard = boards.First(board => board.Type == BoardTypes.Scrum && board.Name.ToLowerInvariant() == selectedBoard);
            var sprints = await Client.Agile.GetSprintsAsync(scrumboard.Id);
            var sprint = sprints.First(s => s.Name.ToLowerInvariant() == selectedSprint);

            // Act
            var report = await Client.Greenhopper.GetSprintReportAsync(scrumboard.Id, sprint.Id);

            // Assert
            Assert.NotNull(report);
            Assert.NotEmpty(report.Contents.CompletedIssues);
            Assert.Equal(7, report.Contents.CompletedIssues.Count());
            Assert.NotEmpty(report.Contents.IssuesCompletedInAnotherSprint);
            Assert.Equal(2, report.Contents.IssuesCompletedInAnotherSprint.Count());
            Assert.NotEmpty(report.Contents.PuntedIssues);
            Assert.Equal(3, report.Contents.PuntedIssues.Count());
            Assert.NotEmpty(report.Contents.IssueKeysAddedDuringSprint);
            Assert.Equal(2, report.Contents.IssueKeysAddedDuringSprint.Count());
        }
    }
}
