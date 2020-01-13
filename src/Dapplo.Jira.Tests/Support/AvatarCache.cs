// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Enums;
using Dapplo.Utils;

namespace Dapplo.Jira.Tests.Support
{
	/// <summary>
	///     An example of a AvatarCache
	/// </summary>
	public class AvatarCache : AsyncMemoryCache<AvatarUrls, BitmapSource>
	{
		private readonly IJiraClient _jiraClient;

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="jiraClient"></param>
		public AvatarCache(IJiraClient jiraClient)
		{
			_jiraClient = jiraClient;
		}

		/// <summary>
		///     This should rather be supplied to the CreateAsync by having a key as Tuple with AvatarUrls and Size...
		/// </summary>
		public AvatarSizes AvatarSize { get; set; } = AvatarSizes.ExtraLarge;

		/// <summary>
		///     Retrieve the avatar for the current size.
		/// </summary>
		/// <param name="key">AvatarUrls</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>BitmapSource</returns>
		protected override async Task<BitmapSource> CreateAsync(AvatarUrls key, CancellationToken cancellationToken = new CancellationToken())
		{
			return await _jiraClient.Server.GetAvatarAsync<BitmapSource>(key, AvatarSize, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Create a string key of the keyObject
		/// </summary>
		/// <param name="keyObject">AvatarUrls</param>
		/// <returns>string</returns>
		protected override string CreateKey(AvatarUrls keyObject)
		{
			return keyObject.GetUri(AvatarSize).AbsoluteUri;
		}
	}
}