// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Information on the custom field id for the estimation information
	/// </summary>
	[JsonObject]
	public class EstimationFieldInfo
	{
		/// <summary>
		///     Field name of the estimation custom field
		/// </summary>
		[JsonProperty("fieldId")]
		public string FieldId { get; set; }

		/// <summary>
		///     Display name for the estimation
		/// </summary>
		[JsonProperty("displayName")]
		public string DisplayName { get; set; }
	}
}