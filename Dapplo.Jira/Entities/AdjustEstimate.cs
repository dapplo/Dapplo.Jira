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

using System.ComponentModel;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     allows you to provide specific instructions to update the remaining time estimate of the issue. Valid values are
	/// </summary>
	public enum AdjustEstimate
	{
		/// <summary>
		///     Default option. Will automatically adjust the value based on the new timeSpent specified on the worklog
		/// </summary>
		[Description("auto")] Auto,

		/// <summary>
		///     sets the estimate to a specific value
		/// </summary>
		[Description("new")] New,

		/// <summary>
		///     leaves the estimate as is
		/// </summary>
		[Description("leave")] Leave,

		/// <summary>
		///     specify a specific amount to increase remaining estimate by
		/// </summary>
		[Description("manual")] Manual
	}
}