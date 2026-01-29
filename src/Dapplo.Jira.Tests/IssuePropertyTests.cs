// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Jira.Tests;

public class IssuePropertyTests : TestBase
{
    public IssuePropertyTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task Test_SetAndGetProperty()
    {
        const string propertyKey = "test-property";
        var propertyValue = new
        {
            testField = "test value",
            numericField = 42
        };

        // Set the property
        await Client.Issue.SetPropertyAsync(TestIssueKey, propertyKey, propertyValue, cancellationToken: TestContext.Current.CancellationToken);

        // Get the property back
        var property = await Client.Issue.GetPropertyAsync(TestIssueKey, propertyKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(property);
        Assert.Equal(propertyKey, property.Key);
        Assert.NotNull(property.Value);
    }

    [Fact]
    public async Task Test_GetPropertyKeys()
    {
        const string propertyKey = "test-property-keys";
        var propertyValue = new { data = "test" };

        // Set a property to ensure at least one exists
        await Client.Issue.SetPropertyAsync(TestIssueKey, propertyKey, propertyValue, cancellationToken: TestContext.Current.CancellationToken);

        // Get all property keys
        var propertyKeys = await Client.Issue.GetPropertyKeysAsync(TestIssueKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(propertyKeys);
        Assert.NotNull(propertyKeys.Keys);
        Assert.NotEmpty(propertyKeys.Keys);
    }

    [Fact]
    public async Task Test_DeleteProperty()
    {
        const string propertyKey = "test-property-to-delete";
        var propertyValue = new { data = "test" };

        // Set a property
        await Client.Issue.SetPropertyAsync(TestIssueKey, propertyKey, propertyValue, cancellationToken: TestContext.Current.CancellationToken);

        // Verify it exists
        var property = await Client.Issue.GetPropertyAsync(TestIssueKey, propertyKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(property);

        // Delete the property
        await Client.Issue.DeletePropertyAsync(TestIssueKey, propertyKey, cancellationToken: TestContext.Current.CancellationToken);

        // Verify it's deleted by checking that getting it throws an exception
        await Assert.ThrowsAnyAsync<System.Exception>(async () =>
            await Client.Issue.GetPropertyAsync(TestIssueKey, propertyKey, cancellationToken: TestContext.Current.CancellationToken)
        );
    }

    [Fact]
    public async Task Test_UpdateProperty()
    {
        const string propertyKey = "test-property-update";
        var initialValue = new { counter = 1 };
        var updatedValue = new { counter = 2 };

        // Set initial property
        await Client.Issue.SetPropertyAsync(TestIssueKey, propertyKey, initialValue, cancellationToken: TestContext.Current.CancellationToken);

        // Update the property
        await Client.Issue.SetPropertyAsync(TestIssueKey, propertyKey, updatedValue, cancellationToken: TestContext.Current.CancellationToken);

        // Get the updated property
        var property = await Client.Issue.GetPropertyAsync(TestIssueKey, propertyKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(property);
        Assert.NotNull(property.Value);

        // Clean up
        await Client.Issue.DeletePropertyAsync(TestIssueKey, propertyKey, cancellationToken: TestContext.Current.CancellationToken);
    }
}
