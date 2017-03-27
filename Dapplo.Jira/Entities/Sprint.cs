#region Dapplo 2017 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017 Dapplo
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

#region Usings

using System;
using System.ComponentModel;
using Newtonsoft.Json;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Sprint information
	/// </summary>
	[JsonObject]
	public class Sprint : BaseProperties<long>
	{
		/// <summary>
		///     Name of the Sprint
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		/// <summary>
		///     Goal of the Sprint
		/// </summary>
		[JsonProperty(PropertyName = "goal")]
		public string Goal { get; set; }

		/// <summary>
		///     From what board is this sprint?
		/// </summary>
		[JsonProperty(PropertyName = "originBoardId")]
		public int OriginBoardId { get; set; }

		/// <summary>
		///     State of the Sprint
		/// </summary>
		[JsonProperty(PropertyName = "state")]
		public string State { get; set; }

		/// <summary>
		///     When was the sprint started
		/// </summary>
		[JsonProperty(PropertyName = "startDate")]
		[ReadOnly(true)]
		public DateTimeOffset StartDate { get; set; }

		/// <summary>
		///     When was the sprint ended
		/// </summary>
		[JsonProperty(PropertyName = "endDate")]
		[ReadOnly(true)]
		public DateTimeOffset EndDate { get; set; }

		/// <summary>
		///     When was the sprint completed
		/// </summary>
		[JsonProperty(PropertyName = "completeDate")]
		[ReadOnly(true)]
		public DateTimeOffset CompleteDate { get; set; }
	}
}