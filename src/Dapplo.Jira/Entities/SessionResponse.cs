// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Response to the session login
	/// </summary>
	[JsonObject]
	internal class SessionResponse
	{
		[JsonProperty("loginInfo")]
		public LoginInfo LoginInfo { get; set; }

		[JsonProperty("session")]
		public JiraSession Session { get; set; }
	}
}