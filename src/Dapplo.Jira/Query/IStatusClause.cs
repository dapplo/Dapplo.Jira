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
	///     An interface for status based clauses
	/// </summary>
	public interface IStatusClause
    {
        /// <summary>
        ///     Negates the expression
        /// </summary>
        IStatusClause Not { get; }

		/// <summary>
		///     This allows fluent constructs like IssueKey.In(BUG-1234, FEATURE-5678)
		/// </summary>
		IFinalClause In(params string[] states);
        
		/// <summary>
		///     This allows fluent constructs like Id.Is(12345)
		/// </summary>
		IFinalClause Is(string state);

        /// <summary>
        ///     This allows fluent constructs like Status.Was("Closed")
        /// TODO: Add the different predicates like after and before etc... See: <a href="https://confluence.atlassian.com/jira/advanced-searching-179442050.html#id-__JQLWAScaveats-Status">here</a>
        /// </summary>
        IFinalClause Was(string state);
	}
}