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
using Xunit.Abstractions;

namespace Dapplo.Jira.Tests
{
	public class ProjectTests : TestBase
	{
 		public ProjectTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
		{
			// Add SvgBitmapHttpContentConverter if it was not yet added
			if (HttpExtensionsGlobals.HttpContentConverters.All(x => x.GetType() != typeof(SvgBitmapHttpContentConverter)))
			{
				HttpExtensionsGlobals.HttpContentConverters.Add(SvgBitmapHttpContentConverter.Instance.Value);
			}
			// Add BitmapHttpContentConverter if it was not yet added
			if (HttpExtensionsGlobals.HttpContentConverters.All(x => x.GetType() != typeof(BitmapHttpContentConverter)))
			{
				HttpExtensionsGlobals.HttpContentConverters.Add(BitmapHttpContentConverter.Instance.Value);
			}
			// Add BitmapSourceHttpContentConverter if it was not yet added
			if (HttpExtensionsGlobals.HttpContentConverters.All(x => x.GetType() != typeof(BitmapSourceHttpContentConverter)))
			{
				HttpExtensionsGlobals.HttpContentConverters.Add(BitmapSourceHttpContentConverter.Instance.Value);
			}
		}

		[Fact]
		public async Task TestGetProjectAsync()
		{
			var project = await Client.Project.GetAsync("DIT");

			Assert.NotNull(project);
			Assert.True(project.Roles.Count > 0);
			foreach (var componentDigest in project.Components)
			{
				var component = await Client.Project.GetComponentAsync(componentDigest.Id);
				Assert.NotNull(component?.Name);
				Log.Info().WriteLine("Component {0}", component.Name);
			}
		}

		[Fact]
		public async Task TestGetSecurityLevelsAsync()
		{
			var securityLevels = await Client.Project.GetSecurityLevelsAsync("DIT");

			Assert.NotNull(securityLevels);
			Assert.True(securityLevels.Any());
		}

		[Fact]
		public async Task TestGetIssueCreatorsAsync()
		{
			var creators = await Client.Project.GetIssueCreatorsAsync("DIT");
			Assert.NotNull(creators);

			var firstCreator = creators.First();
			await Client.Server.GetAvatarAsync<Bitmap>(firstCreator.Avatars);
		}

		[Fact]
		public async Task TestComponentAsync()
		{
			// Create
			var component = new Component
			{
				Name = "Component from Test",
				Project = "DIT",
				Description = "This was created from a test"
			};
			component = await Client.Project.CreateComponentAsync(component);

			// Update
			const string descriptionUpdate = "Changed the description";
			component.Description = descriptionUpdate;
			await Client.Project.UpdateComponentAsync(component);

			// Delete
			component = await Client.Project.GetComponentAsync(component.Id);
			Assert.Equal(descriptionUpdate, component.Description);
			await Client.Project.DeleteComponentAsync(component.Id);
		}

		[Fact]
		public async Task TestGetProjectsAsync()
		{
			var projects = await Client.Project.GetAllAsync();

			Assert.NotNull(projects);
			Assert.True(projects.Count > 0);

			Client.Behaviour.SetConfig(new SvgConfiguration {Width = 24, Height = 24});

			foreach (var project in projects)
			{
				var avatar = await Client.Server.GetAvatarAsync<Bitmap>(project.Avatar, AvatarSizes.Medium);
				Assert.True(avatar.Width == 24);

				var projectDetails = await Client.Project.GetAsync(project.Key);
				Assert.NotNull(projectDetails);
			}
		}
	}
}