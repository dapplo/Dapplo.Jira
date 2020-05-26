// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     This interface defines the ways to identify a user
	/// </summary>
	public interface IUserIdentifier
	{
        /// <summary>
        ///     Value for the account ID
        /// </summary>
        [JsonProperty("accountId")]
        [ReadOnly(true)]
        public string AccountId { get; set; }

		/// <summary>
		///     Name of the user
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }
	}
}