// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Transition information
	/// </summary>
	[JsonObject]
	public class Transition : BaseId<long>
	{
		/// <summary>
		///     Does this transition have a screen?
		/// </summary>
		[JsonProperty("hasScreen")]
		public bool HasScreen { get; set; }

		/// <summary>
		///     Name for this transition
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///     Possible fields for the transation
		/// </summary>
		[JsonProperty("fields")]
		public IDictionary<string, PossibleField> PossibleFields { get; set; }

		/// <summary>
		///     To status for the transation
		/// </summary>
		[JsonProperty("to")]
		public Status To { get; set; }
	}
}