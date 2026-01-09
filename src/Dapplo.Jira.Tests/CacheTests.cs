// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.HttpExtensions.WinForms.ContentConverter;
using Dapplo.HttpExtensions.Wpf.ContentConverter;
using Dapplo.Jira.SvgWinForms.Converters;
using Dapplo.Jira.Tests.Support;
using Xunit;

namespace Dapplo.Jira.Tests;

public class CacheTests : TestBase
{
    public CacheTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, true)
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

        avatarCache = new AvatarCache(Client);
    }

    private readonly AvatarCache avatarCache;

    [Fact]
    public async Task TestCache()
    {
        var me = await Client.User.GetMyselfAsync(TestContext.Current.CancellationToken);
        Assert.NotEmpty(me.AccountId);

        var avatar = await avatarCache.GetOrCreateAsync(me.Avatars, cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(avatar);
        Assert.True(avatar.Width > 0);

        avatar = await avatarCache.GetAsync(me.Avatars);
        Assert.NotNull(avatar);
        Assert.True(avatar.Width > 0);
    }
}
