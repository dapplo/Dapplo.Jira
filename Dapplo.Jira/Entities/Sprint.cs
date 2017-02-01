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
using System.Runtime.Serialization;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Sprint information
	/// </summary>
	[DataContract]
	public class Sprint : BaseProperties<long>
	{
		/// <summary>
		///     Name of the Sprint
		/// </summary>
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }

		/// <summary>
		///     Goal of the Sprint
		/// </summary>
		[DataMember(Name = "goal", EmitDefaultValue = false)]
		public string Goal { get; set; }

		/// <summary>
		///     From what board is this sprint?
		/// </summary>
		[DataMember(Name = "originBoardId", EmitDefaultValue = false)]
		public int OriginBoardId { get; set; }

		/// <summary>
		///     State of the Sprint
		/// </summary>
		[DataMember(Name = "state", EmitDefaultValue = false)]
		public string State { get; set; }

		/// <summary>
		///     When was the sprint started
		/// </summary>
		[DataMember(Name = "startDate", EmitDefaultValue = false)]
		[ReadOnly(true)]
		public DateTimeOffset StartDate { get; set; }

		/// <summary>
		///     When was the sprint ended
		/// </summary>
		[DataMember(Name = "endDate", EmitDefaultValue = false)]
		[ReadOnly(true)]
		public DateTimeOffset EndDate { get; set; }

		/// <summary>
		///     When was the sprint completed
		/// </summary>
		[DataMember(Name = "completeDate", EmitDefaultValue = false)]
		[ReadOnly(true)]
		public DateTimeOffset CompleteDate { get; set; }
	}
}