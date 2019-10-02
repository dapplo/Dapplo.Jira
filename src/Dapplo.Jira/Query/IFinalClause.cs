// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2019 Dapplo
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

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     A clause which cannot be modified anymore, only ToString() makes sense
	/// </summary>
	public interface IFinalClause
	{
		/// <summary>
		///     Specify the order by field, default field order is used, this can be called mutiple times
		/// </summary>
		/// <param name="field">Field to specify what to order by</param>
		/// <returns>IFinalClause</returns>
		IFinalClause OrderBy(Fields field);

		/// <summary>
		///     Specify the order by, ascending, this can be called mutiple times
		/// </summary>
		/// <param name="field">Field to specify what to order by</param>
		/// <returns>IFinalClause</returns>
		IFinalClause OrderByAscending(Fields field);

		/// <summary>
		///     Specify the order by, descending, this can be called mutiple times
		/// </summary>
		/// <param name="field">Field to specify what to order by</param>
		/// <returns>IFinalClause</returns>
		IFinalClause OrderByDescending(Fields field);
	}
}