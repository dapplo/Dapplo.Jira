// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Changelog informations
	/// </summary>
	/// <seealso cref="Dapplo.Jira.Entities.PageableResult" />
	[JsonObject]
	public class Changelog : PageableResult
	{
		/// <summary>
		///     The actual history in changelog
		/// </summary>
		[JsonProperty("histories")]
		public IList<History> Elements { get; set; }
	}
}