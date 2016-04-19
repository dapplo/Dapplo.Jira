//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.PowerShell.Jira
// 
//  Dapplo.PowerShell.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.PowerShell.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.PowerShell.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.



namespace Dapplo.Jira
{
	/// <summary>
	///     Use this class to configure some of the behaviour
	///     The values that start with Expand are used to set the expand query values, which make the Jira REST API return "more".
	/// </summary>
	public static class JiraConfig
	{
		/// <summary>
		///     The values that are expanded in the GetIssue result
		///     Examples are: renderedFields, version
		/// </summary>
		public static string[] ExpandGetIssue { get; set; } = { "version", "container" };

		/// <summary>
		///     The values that are expanded in the GetProject result
		/// </summary>
		public static string[] ExpandGetProject { get; set; }

		/// <summary>
		///     The values that are expanded in the GetProjects result
		/// </summary>
		public static string[] ExpandGetProjects { get; set; } = { "description", "lead"};

		/// <summary>
		///     The values that are expanded in the GetFavoriteFilters result
		/// </summary>
		public static string[] ExpandGetFavoriteFilters { get; set; }

		/// <summary>
		///     The values that are expanded in the GetFilter result
		/// </summary>
		public static string[] ExpandGetFilter { get; set; }

		/// <summary>
		///     The values that are expanded in the Search result
		/// </summary>
		public static string[] ExpandSearch { get; set; }

		/// <summary>
		///     The fields that are requested by the Search result
		/// </summary>
		public static string[] SearchFields { get; set; } = { "summary", "status", "assignee", "key", "project" };
	}
}
