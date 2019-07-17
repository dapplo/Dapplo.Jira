#region Dapplo 2017-2019 - GNU Lesser General Public License

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

#endregion

using Dapplo.Jira.Entities;

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     An interface for issue based clauses.
	///     <a href="https://confluence.atlassian.com/jiracoreserver072/advanced-searching-functions-reference-829092674.html">Advanced searching - functions reference</a>
	/// </summary>
	public interface IIssueClause
	{
		/// <summary>
		///     Negates the expression
		/// </summary>
		IIssueClause Not { get; }

		/// <summary>
		///     This allows fluent constructs like IssueKey.In(BUG-1234, FEATURE-5678)
		/// </summary>
		IFinalClause In(params string[] issueKeys);

		/// <summary>
		///     This allows fluent constructs like IssueKey.In(issue1, issue2)
		/// </summary>
		IFinalClause In(params Issue[] issues);

		/// <summary>
		///     This allows fluent constructs like IssueKey.InIssueHistory()
		///     Find issues that you have recently viewed, i.e. issues that are in the 'Recent Issues' section of the 'Issues' drop-down menu.
		/// </summary>
		IFinalClause InIssueHistory();

		/// <summary>
		///     This allows fluent constructs like IssueKey.InLinkedIssues(BUG-12345)
		///     Perform searches based on issues that are linked to a specified issue.
		///     You can optionally restrict the search to links of a particular type. Note that LinkType is case-sensitive.
		/// </summary>
		/// <param name="issueKey">string</param>
		/// <param name="linkType">string</param>
		/// <returns>IFinalClause</returns>
		IFinalClause InLinkedIssues(string issueKey, string linkType = null);

		/// <summary>
		///     This allows fluent constructs like IssueKey.InLinkedIssues(issue1)
		///     Perform searches based on issues that are linked to a specified issue.
		///     You can optionally restrict the search to links of a particular type. Note that LinkType is case-sensitive.
		/// </summary>
		/// <param name="issue">Issue</param>
		/// <param name="linkType">string</param>
		/// <returns>IFinalClause</returns>
		IFinalClause InLinkedIssues(Issue issue, string linkType = null);

		/// <summary>
		///     This allows fluent constructs like IssueKey.InVotedIssues()
		///     Find issues that you have voted for.
		/// </summary>
		IFinalClause InVotedIssues();

		/// <summary>
		///     This allows fluent constructs like IssueKey.InWatchedIssues()
		///     Find issues that you have voted for.
		/// </summary>
		IFinalClause InWatchedIssues();

		/// <summary>
		///     This allows fluent constructs like Id.Is(12345)
		/// </summary>
		IFinalClause Is(string issueKey);

		/// <summary>
		///     This allows fluent constructs like Id.Is(issue1)
		/// </summary>
		IFinalClause Is(Issue issue);
	}
}