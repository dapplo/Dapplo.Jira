#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016 Dapplo
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

using System;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;
using Dapplo.Log.Facade;
using Dapplo.Log.XUnit;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Jira.Tests
{
	public class JiraCacheTests
	{
		public JiraCacheTests(ITestOutputHelper testOutputHelper)
		{
			LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
			_jiraApi = new JiraApi(TestJiraUri);
			_avatarCache = new AvatarCache(_jiraApi);
		}

		// Test against a well known JIRA
		private static readonly Uri TestJiraUri = new Uri("https://greenshot.atlassian.net");
		private readonly JiraApi _jiraApi;
		private readonly AvatarCache _avatarCache;

		[Fact]
		public async Task TestCache()
		{
			var username = Environment.GetEnvironmentVariable("jira_test_username");
			var password = Environment.GetEnvironmentVariable("jira_test_password");
			LoginInfo loginInfo = null;
			if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
			{
				loginInfo = await _jiraApi.StartSessionAsync(username, password);
			}
			var me = await _jiraApi.WhoAmIAsync();
			Assert.Equal(me.Name, username);

			var avatar = await _avatarCache.GetOrCreateAsync(me.Avatars);
			Assert.NotNull(avatar);
			Assert.True(avatar.Width > 0);

			avatar = await _avatarCache.GetAsync(me.Avatars);
			Assert.NotNull(avatar);
			Assert.True(avatar.Width > 0);

			// when changing the size, the value is no longer available.
			_avatarCache.AvatarSize = AvatarSizes.Small;
			avatar = await _avatarCache.GetAsync(me.Avatars);
			Assert.Null(avatar);

			if (loginInfo != null)
			{
				await _jiraApi.EndSessionAsync();
			}
		}
	}
}