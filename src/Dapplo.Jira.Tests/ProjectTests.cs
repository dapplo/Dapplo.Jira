// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.HttpExtensions.Extensions;
using Dapplo.HttpExtensions.WinForms.ContentConverter;
using Dapplo.HttpExtensions.Wpf.ContentConverter;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Enums;
using Dapplo.Jira.SvgWinForms.Converters;
using Dapplo.Log;
using Xunit;

namespace Dapplo.Jira.Tests;

public class ProjectTests : TestBase
{
    public ProjectTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        // Add SvgBitmapHttpContentConverter if it was not yet added
        if (HttpExtensionsGlobals.HttpContentConverters.All(x => x.GetType() != typeof(SvgBitmapHttpContentConverter)))
        {
            Log.Info().WriteLine("Added SvgBitmapHttpContentConverter");
            HttpExtensionsGlobals.HttpContentConverters.Add(SvgBitmapHttpContentConverter.Instance.Value);
        }

        // Add BitmapHttpContentConverter if it was not yet added
        if (HttpExtensionsGlobals.HttpContentConverters.All(x => x.GetType() != typeof(BitmapHttpContentConverter)))
        {
            Log.Info().WriteLine("Added BitmapHttpContentConverter");
            HttpExtensionsGlobals.HttpContentConverters.Add(BitmapHttpContentConverter.Instance.Value);
        }

        // Add BitmapSourceHttpContentConverter if it was not yet added
        if (HttpExtensionsGlobals.HttpContentConverters.All(x => x.GetType() != typeof(BitmapSourceHttpContentConverter)))
        {
            Log.Info().WriteLine("Added BitmapSourceHttpContentConverter");
            HttpExtensionsGlobals.HttpContentConverters.Add(BitmapSourceHttpContentConverter.Instance.Value);
        }
    }

    [Fact]
    public async Task TestGetProjectAsync()
    {
        var project = await Client.Project.GetAsync(TestProjectKey, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(project);
        Assert.True(project.Roles.Count > 0);
        foreach (var componentDigest in project.Components)
        {
            var component = await Client.Project.GetComponentAsync(componentDigest.Id, cancellationToken: TestContext.Current.CancellationToken);
            Assert.NotNull(component?.Name);
            Log.Info().WriteLine("Component {0}", component.Name);
        }
    }

    [Fact]
    public async Task TestGetSecurityLevelsAsync()
    {
        var securityLevels = await Client.Project.GetSecurityLevelsAsync(TestProjectKey, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(securityLevels);
    }

    [Fact]
    public async Task TestGetIssueCreatorsAsync()
    {
        var creators = await Client.Project.GetIssueCreatorsAsync(TestProjectKey, cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(creators);

        var firstCreator = creators.First();
        await Client.Server.GetAvatarAsync<Bitmap>(firstCreator.Avatars, cancellationToken: TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task TestComponentAsync()
    {
        // Create
        var component = new Component
        {
            Name = "Component from Test",
            Project = TestProjectKey,
            Description = "This was created from a test"
        };
        component = await Client.Project.CreateComponentAsync(component, cancellationToken: TestContext.Current.CancellationToken);

        // Update
        const string descriptionUpdate = "Changed the description";
        component.Description = descriptionUpdate;
        await Client.Project.UpdateComponentAsync(component, cancellationToken: TestContext.Current.CancellationToken);

        // Delete
        component = await Client.Project.GetComponentAsync(component.Id, cancellationToken: TestContext.Current.CancellationToken);
        Assert.Equal(descriptionUpdate, component.Description);
        await Client.Project.DeleteComponentAsync(component.Id, cancellationToken: TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task TestGetProjectsAsync()
    {
        var projects = await Client.Project.GetAllAsync(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(projects);
        Assert.True(projects.Count > 0);

        Client.Behaviour.SetConfig(new SvgConfiguration
        {
            Width = 24,
            Height = 24
        });

        foreach (var project in projects)
        {
            var avatar = await Client.Server.GetAvatarAsync<Bitmap>(project.Avatar, AvatarSizes.Medium, cancellationToken: TestContext.Current.CancellationToken);
            Assert.True(avatar.Width == 24);

            var projectDetails = await Client.Project.GetAsync(project.Key, cancellationToken: TestContext.Current.CancellationToken);
            Assert.NotNull(projectDetails);
        }
    }

    [Fact]
    public async Task TestGetVersionsAsync()
    {
        var versions = await Client.Project.GetVersionsAsync(TestProjectKey, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(versions);
        Log.Info().WriteLine("Found {0} versions for project {1}", versions.Count, TestProjectKey);
        
        foreach (var version in versions)
        {
            Assert.NotNull(version.Name);
            Log.Info().WriteLine("Version: {0}, Released: {1}, Archived: {2}, ReleaseDate: {3}", 
                version.Name, version.Released, version.Archived, version.ReleaseDate);
        }
    }

    [Fact]
    public async Task TestGetVersionAsync()
    {
        // First get all versions to get a valid version ID
        var versions = await Client.Project.GetVersionsAsync(TestProjectKey, cancellationToken: TestContext.Current.CancellationToken);
        
        if (versions.Count > 0)
        {
            var firstVersion = versions.First();
            
            // Get the specific version by ID
            var version = await Client.Project.GetVersionAsync(firstVersion.Id, cancellationToken: TestContext.Current.CancellationToken);
            
            Assert.NotNull(version);
            Assert.Equal(firstVersion.Name, version.Name);
            Log.Info().WriteLine("Retrieved version: {0}", version.Name);
        }
        else
        {
            Log.Info().WriteLine("No versions found for project {0}, skipping version retrieval test", TestProjectKey);
        }
    }
}
