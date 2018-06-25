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

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Enums;
using Dapplo.Utils;

#endregion

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