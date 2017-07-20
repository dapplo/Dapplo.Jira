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

using Dapplo.Jira.Entities;

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     The possible methods for a type clause
	/// </summary>
	public interface ITypeClause
	{
		/// <summary>
		///     Negates the expression
		/// </summary>
		ITypeClause Not { get; }

		/// <summary>
		///     Test if the type of the content is one of the specified types
		/// </summary>
		/// <param name="types">Types</param>
		/// <returns>IFinalClause</returns>
		IFinalClause In(params string[] types);

		/// <summary>
		///     Test if the type of the content is one of the specified types
		/// </summary>
		/// <param name="types">IssueType array</param>
		/// <returns>IFinalClause</returns>
		IFinalClause In(params IssueType[] types);

		/// <summary>
		///     This allows fluent constructs like Type.Is("Feature")
		/// </summary>
		IFinalClause Is(string type);

		/// <summary>
		///     Test if the type of the content is the specified type
		/// </summary>
		/// <param name="type">IssueType</param>
		/// <returns>IFinalClause</returns>
		IFinalClause Is(IssueType type);
	}
}