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

#endregion

#region Usings

using System.Linq;

#endregion

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     An interface for a version based clauses
	/// </summary>
	public interface IVersionClause
	{
		/// <summary>
		///     Negates the expression
		/// </summary>
		IVersionClause Not { get; }

		/// <summary>
		///     This allows fluent constructs like IssueKey.In(BUG-1234, FEATURE-5678)
		/// </summary>
		IFinalClause In(params string[] issueKeys);

		/// <summary>
		///     This allows fluent constructs like IssueKey.InEarliestUnreleasedVersion(BUGS)
		/// </summary>
		IFinalClause InEarliestUnreleasedVersion(string project);

		/// <summary>
		///     This allows fluent constructs like IssueKey.InLatestReleasedVersion(BUGS)
		/// </summary>
		IFinalClause InLatestReleasedVersion(string project);

		/// <summary>
		///     This allows fluent constructs like IssueKey.InReleasedVersions()
		/// </summary>
		IFinalClause InReleasedVersions(string project = null);

		/// <summary>
		///     This allows fluent constructs like IssueKey.InUnreleasedVersions()
		/// </summary>
		IFinalClause InUnreleasedVersions(string project = null);

		/// <summary>
		///     This allows fluent constructs like Id.Is(12345)
		/// </summary>
		IFinalClause Is(string issueKey);
	}

	/// <summary>
	///     A clause for version values like fixVersion and more
	/// </summary>
	public class VersionClause : IVersionClause
	{
		private readonly Clause _clause;

		private bool _negate;

		public VersionClause(Fields versionField)
		{
			_clause = new Clause
			{
				Field = versionField
			};
		}

		/// <inheritDoc />
		public IVersionClause Not
		{
			get
			{
				_negate = !_negate;
				return this;
			}
		}

		/// <inheritDoc />
		public IFinalClause Is(string version)
		{
			_clause.Operator = Operators.EqualTo;
			_clause.Value = $"\"{version}\"";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause In(params string[] versions)
		{
			_clause.Operator = Operators.In;
			_clause.Value = "(" + string.Join(", ", versions.Select(version => $"\"{version}\"")) + ")";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause InReleasedVersions(string project = null)
		{
			_clause.Operator = Operators.In;
			_clause.Value = $"releasedVersions({project})";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause InLatestReleasedVersion(string project)
		{
			_clause.Operator = Operators.In;
			_clause.Value = $"latestReleasedVersion({project})";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause InUnreleasedVersions(string project = null)
		{
			_clause.Operator = Operators.In;
			_clause.Value = $"unreleasedVersions({project})";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}


		/// <inheritDoc />
		public IFinalClause InEarliestUnreleasedVersion(string project)
		{
			_clause.Operator = Operators.In;
			_clause.Value = $"earliestUnreleasedVersion({project})";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}
	}
}