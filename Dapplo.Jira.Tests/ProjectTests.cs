//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Jira
// 
//  Dapplo.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System.Drawing;
using System.Threading.Tasks;
using Dapplo.HttpExtensions.Extensions;
using Dapplo.Jira.Converters;
using Dapplo.Jira.Entities;
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
			var project = await _jiraApi.Project.GetAsync("BUG");

			Assert.NotNull(project);
			Assert.NotNull(project.Roles.Count > 0);
		}

		[Fact]
		public async Task TestGetProjectsAsync()
		{
			var projects = await _jiraApi.Project.GetAllAsync();

			Assert.NotNull(projects);
			Assert.NotNull(projects.Count > 0);

			_jiraApi.Behaviour.SetConfig(new SvgConfiguration {Width = 24, Height = 24});

			foreach (var project in projects)
			{
				var avatar = await _jiraApi.GetAvatarAsync<Bitmap>(project.Avatar, AvatarSizes.Medium);
				Assert.True(avatar.Width == 24);

				var projectDetails = await _jiraApi.Project.GetAsync(project.Key);
				Assert.NotNull(projectDetails);
			}
		}
	}
}