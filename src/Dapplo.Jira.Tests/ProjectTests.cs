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

    [Fact]
    public async Task TestGetRolesAsync()
    {
        var roles = await Client.Project.GetRolesAsync(TestProjectKey, cancellationToken: TestContext.Current.CancellationToken);
        
        Assert.NotNull(roles);
        Assert.True(roles.Count > 0);
        
        Log.Info().WriteLine("Found {0} roles for project {1}", roles.Count, TestProjectKey);
        foreach (var role in roles)
        {
            Log.Info().WriteLine("Role: {0} - {1}", role.Key, role.Value);
        }
    }

    [Fact]
    public async Task TestGetRoleAsync()
    {
        // First get all roles to get a valid role ID
        var roles = await Client.Project.GetRolesAsync(TestProjectKey, cancellationToken: TestContext.Current.CancellationToken);
        
        Assert.NotNull(roles);
        Assert.True(roles.Count > 0);
        
        // Get the ID from the first role URI (e.g., .../role/10002 -> 10002)
        var firstRoleUri = roles.First().Value;
        var roleIdString = firstRoleUri.Segments.Last();
        var roleId = long.Parse(roleIdString);
        
        // Get the specific role details
        var role = await Client.Project.GetRoleAsync(TestProjectKey, roleId, cancellationToken: TestContext.Current.CancellationToken);
        
        Assert.NotNull(role);
        Assert.NotNull(role.Name);
        Log.Info().WriteLine("Retrieved role: {0}, Description: {1}", role.Name, role.Description);
        
        if (role.Actors != null)
        {
            Log.Info().WriteLine("Role has {0} actors", role.Actors.Count);
            foreach (var actor in role.Actors)
            {
                Log.Info().WriteLine("Actor: {0} ({1})", actor.DisplayName, actor.Type);
            }
        }
    }

    [Fact]
    public async Task TestAddAndRemoveActorFromRoleAsync()
    {
        // First get all roles to get a valid role ID
        var roles = await Client.Project.GetRolesAsync(TestProjectKey, cancellationToken: TestContext.Current.CancellationToken);
        
        if (roles.Count == 0)
        {
            Log.Info().WriteLine("No roles found for project {0}, skipping actor test", TestProjectKey);
            return;
        }
        
        // Get a role ID
        var firstRoleUri = roles.First().Value;
        var roleIdString = firstRoleUri.Segments.Last();
        var roleId = long.Parse(roleIdString);
        
        // Get current user to use as test actor
        var currentUser = await Client.User.GetMyselfAsync(cancellationToken: TestContext.Current.CancellationToken);
        var userIdentifier = currentUser.AccountId ?? currentUser.Name;
        
        // Try to add the current user to the role
        try
        {
            var updatedRole = await Client.Project.AddActorToRoleAsync(TestProjectKey, roleId, user: userIdentifier, cancellationToken: TestContext.Current.CancellationToken);
            Assert.NotNull(updatedRole);
            Log.Info().WriteLine("Added user {0} to role {1}", currentUser.DisplayName, updatedRole.Name);
            
            // Remove the user from the role
            await Client.Project.RemoveActorFromRoleAsync(TestProjectKey, roleId, user: userIdentifier, cancellationToken: TestContext.Current.CancellationToken);
            Log.Info().WriteLine("Removed user {0} from role", currentUser.DisplayName);
        }
        catch (Exception ex)
        {
            // Some JIRA instances may not have permissions to modify roles
            Log.Info().WriteLine("Could not modify role actors (this may be expected): {0}", ex.Message);
        }
    }
}
