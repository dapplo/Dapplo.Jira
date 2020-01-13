// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Transitions information
	/// </summary>
	[JsonObject]
	public class Transitions
	{
		/// <summary>
		///     The actual list of transitions
		/// </summary>
		[JsonProperty("transitions")]
		public IList<Transition> Items { get; set; }
	}
}