﻿// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Jira.Tests;

public class UserTests : TestBase
{
    public UserTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task TestSearchUsersAsync()
    {
        var users = await Client.User.SearchAsync("krom");
        Assert.NotNull(users);
        Assert.True(users.Count > 0);
    }

    [Fact]
    public async Task TestSearchUsersByQueryAsync()
    {
        var users = await Client.User.SearchByQueryAsync("is assignee of BUG");
        Assert.NotNull(users);
        Assert.True(users.Count > 0);
    }

    [Fact]
    public async Task TestUser()
    {
        var meMyselfAndI = await Client.User.GetMyselfAsync();
        Assert.NotNull(meMyselfAndI);
        var meAgain = await Client.User.GetAsync(meMyselfAndI);
        Assert.NotNull(meAgain);
    }
}