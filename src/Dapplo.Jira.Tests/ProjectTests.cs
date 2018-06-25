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

using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Dapplo.HttpExtensions.Extensions;
using Dapplo.Jira.Converters;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Enums;
using Dapplo.Log;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Jira.Tests
{
	public class ProjectTests : TestBase
	{
 	    public ProjectTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
	    {
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