// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Xunit;

namespace Dapplo.Jira.Tests;

public class GroupTests : TestBase
{
    public GroupTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task TestSearchGroupsAsync()
    {
        // Search for groups
        var groups = await Client.Group.SearchAsync("jira", cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(groups);
        Assert.NotNull(groups.Values);
    }

    [Fact]
    public async Task TestSearchGroupsWithoutQueryAsync()
    {
        // Search for all groups
        var groups = await Client.Group.SearchAsync(cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(groups);
        Assert.NotNull(groups.Values);
    }
}
