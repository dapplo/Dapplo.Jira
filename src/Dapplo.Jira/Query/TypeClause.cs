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

using System.Linq;
using Dapplo.Jira.Entities;

namespace Dapplo.Jira.Query
{
	/// <summary>
	/// Where clauses for the field type
	/// </summary>
	public class TypeClause : ITypeClause
	{
		private readonly Clause _clause = new Clause
		{
			Field = Fields.Type
		};

		private bool _negate;

		/// <inheritDoc />
		public ITypeClause Not
		{
			get
			{
				_negate = !_negate;
				return this;
			}
		}

		/// <inheritDoc />
		public IFinalClause In(params string[] types)
		{
			_clause.Operator = Operators.In;
			_clause.Value = "(" + string.Join(", ", types) + ")";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause In(params IssueType[] issueTypes)
		{
			return In(issueTypes.Select(issueType => issueType.Id).ToArray());
		}


		/// <inheritDoc />
		public IFinalClause Is(string type)
		{
			_clause.Operator = Operators.EqualTo;
			_clause.Value = type;
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause Is(IssueType type)
		{
			return Is(type.Id);
		}

		/// <inheritDoc />
		public IFinalClause In(params long[] issueTypeIds)
		{
			return In(issueTypeIds.Select(issueTypeId => issueTypeId.ToString()).ToArray());
		}

		/// <inheritDoc />
		public IFinalClause Is(long issueTypeId)
		{
			return Is(issueTypeId.ToString());
		}
	}
}