// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Dapplo.Jira.Enums;
using Dapplo.Jira.Tests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Jira.Tests
{
	public class CacheTests : TestBase
	{
		public CacheTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, true)
		{
			_avatarCache = new AvatarCache(Client);
		}

		private readonly AvatarCache _avatarCache;

		[Fact]
		public async Task TestCache()
		{
			var me = await Client.User.GetMyselfAsync();
			Assert.NotEmpty(me.Name);

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
		}
	}
}