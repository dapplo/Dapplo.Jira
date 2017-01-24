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

using System.Threading.Tasks;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Tests.Support;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Jira.Tests
{
	public class CacheTests : TestBase
	{
		private readonly AvatarCache _avatarCache;
		public CacheTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, false)
		{
			_avatarCache = new AvatarCache(Client);
		}

		[Fact]
		public async Task TestCache()
		{
			LoginInfo loginInfo = null;
			if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
			{
				loginInfo = await Client.Session.StartAsync(Username, Password);
			}
			var me = await Client.User.GetMyselfAsync();
			Assert.Equal(me.Name, Username);

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
				await Client.Session.EndAsync();
			}
		}
	}
}