// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Base id, used in pretty much every entity
	/// </summary>
	[JsonObject]
	public class BaseId<TId>
	{
		/// <summary>
		///     Id of this entity
		/// </summary>
		[JsonProperty("id")]
		[ReadOnly(true)]
		public TId Id { get; set; }
	}
}