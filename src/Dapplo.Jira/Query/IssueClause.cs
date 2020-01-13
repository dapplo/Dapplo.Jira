// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Dapplo.Jira.Entities;

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     A clause for content identifying values like ancestor, content, id and parent
	/// </summary>
	public class IssueClause : IIssueClause
	{
		private readonly Clause _clause = new Clause
		{
			Field = Fields.IssueKey
		};

		private bool _negate;

		/// <inheritDoc />
		public IIssueClause Not
		{
			get
			{
				_negate = !_negate;
				return this;
			}
		}

		/// <inheritDoc />
		public IFinalClause Is(string issueKey)
		{
			_clause.Operator = Operators.EqualTo;
			_clause.Value = issueKey;
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause Is(Issue issue)
		{
			return Is(issue.Key);
		}

		/// <inheritDoc />
		public IFinalClause In(params string[] issueKeys)
		{
			_clause.Operator = Operators.In;
			_clause.Value = "(" + string.Join(", ", issueKeys) + ")";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause In(params Issue[] issues)
		{
			return In(issues.Select(issue => issue.Key).ToArray());
		}

		/// <inheritDoc />
		public IFinalClause InIssueHistory()
		{
			_clause.Operator = Operators.In;
			_clause.Value = "issueHistory()";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause InLinkedIssues(string issueKey, string linkType = null)
		{
			_clause.Operator = Operators.In;
			var linkTypeArgument = string.IsNullOrEmpty(linkType) ? "" : $", {linkType}";

			_clause.Value = $"linkedIssues({issueKey}{linkTypeArgument})";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause InLinkedIssues(Issue issue, string linkType = null)
		{
			return InLinkedIssues(issue.Key, linkType);
		}

		/// <inheritDoc />
		public IFinalClause InVotedIssues()
		{
			_clause.Operator = Operators.In;
			_clause.Value = "votedIssues()";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause InWatchedIssues()
		{
			_clause.Operator = Operators.In;
			_clause.Value = "watchedIssues()";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}
	}
}