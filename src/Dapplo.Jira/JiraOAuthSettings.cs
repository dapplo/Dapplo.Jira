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


#if NET461

using Dapplo.HttpExtensions.OAuth;

using System.Security.Cryptography;

namespace Dapplo.Jira
{
	/// <summary>
	/// OAuth 1 settings for Jira Oauth connections
	/// </summary>
	public class JiraOAuthSettings
	{
		/// <summary>
		/// Consumer Key which is set in the Jira Application link
		/// </summary>
		public string ConsumerKey { get; set; }

		/// <summary>
		/// Jira uses OAuth1 with RSA-SHA1, for this a RSACryptoServiceProvider is used.
		/// This needs to be created from a private key, the represented public key is set in the linked-applications
		/// </summary>
		public RSACryptoServiceProvider RsaSha1Provider { get; set; }

		/// <summary>
		/// The AuthorizeMode to use
		/// </summary>
		public AuthorizeModes AuthorizeMode { get; set; } = AuthorizeModes.LocalhostServer;

		/// <summary>
		/// Name of the cloud service, which is displayed in the embedded browser / browser
		/// </summary>
		public string CloudServiceName { get; set; } = "Jira";

		/// <summary>
		/// The token object for storing the OAuth 1 secret etc, implement your own IOAuth1Token to be able to store these
		/// </summary>
		public IOAuth1Token Token { get; set; } = new OAuth1Token();
	}
}
#endif