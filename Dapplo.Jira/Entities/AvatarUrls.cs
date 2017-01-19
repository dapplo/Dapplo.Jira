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

using System;
using System.Runtime.Serialization;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Avatar information, has 16x16,24x24,32x32,48x48 Urls
	///     See: <a href="https://docs.atlassian.com/jira/REST/latest/#api/2/project">Jira project</a>
	/// </summary>
	[DataContract]
	public class AvatarUrls
	{
		/// <summary>
		///     Url to the 48x48 Avatar
		/// </summary>
		[DataMember(Name = "48x48")]
		public Uri ExtraLarge { get; set; }

		/// <summary>
		///     Url to the 32x32 Avatar
		/// </summary>
		[DataMember(Name = "32x32")]
		public Uri Large { get; set; }

		/// <summary>
		///     Url to the 24x24 Avatar
		/// </summary>
		[DataMember(Name = "24x24")]
		public Uri Medium { get; set; }

		/// <summary>
		///     Url to the 16x16 Avatar
		/// </summary>
		[DataMember(Name = "16x16")]
		public Uri Small { get; set; }

		/// <summary>
		///     Helper method to get the Uri for a certain avatar size
		/// </summary>
		/// <param name="avatarSize"></param>
		/// <returns>Uri</returns>
		/// <exception cref="ArgumentException">when an unknown avatar size is requested</exception>
		public Uri GetUri(AvatarSizes avatarSize)
		{
			switch (avatarSize)
			{
				case AvatarSizes.Small:
					return Small;
				case AvatarSizes.Medium:
					return Medium;
				case AvatarSizes.Large:
					return Large;
				case AvatarSizes.ExtraLarge:
					return ExtraLarge;
				default:
					throw new ArgumentException($"Unknown avatar size: {avatarSize}", nameof(avatarSize));
			}
		}
	}
}