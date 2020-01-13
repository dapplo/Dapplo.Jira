// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Container for the Error
	/// </summary>
	[JsonObject]
	public class Error
	{
		/// <summary>
		///     The HTTP status code of the error
		/// </summary>
		[JsonProperty("status-code")]
		public int StatusCode { get; set; }

		/// <summary>
		///     The list of error messages
		/// </summary>
		[JsonProperty("errorMessages")]
		public IList<string> ErrorMessages { get; set; }

		/// <summary>
		///     The message
		/// </summary>
		[JsonProperty("message")]
		public string Message { get; set; }

		/// <summary>
		/// A list of errors
		/// </summary>
		[JsonProperty("errors")]
		public IDictionary<string, string> Errors { get; set; }
	}
}