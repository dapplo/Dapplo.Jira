// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using System.Threading.Tasks;
using Dapplo.Jira.Enums;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Jira.Tests;

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
        const string selectedBoard = "greenshot releases";
        const string selectedSprint = "greenshot 1.2.9 bf1";

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
        Assert.Equal(2, report.Contents.IssueKeysAddedDuringSprint.Count);
    }
}