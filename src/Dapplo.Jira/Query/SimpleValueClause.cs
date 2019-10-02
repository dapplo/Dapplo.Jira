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

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     A clause for simple values like container, macro and label
	/// </summary>
	public class SimpleValueClause : ISimpleValueClause
	{
		private readonly Clause _clause;
		private bool _negate;

		internal SimpleValueClause(Fields simpleField)
		{
			_clause = new Clause
			{
				Field = simpleField
			};
		}


		/// <inheritDoc />
		public ISimpleValueClause Not
		{
			get
			{
				_negate = !_negate;
				return this;
			}
		}

		/// <inheritDoc />
		public IFinalClause Is(string value)
		{
			_clause.Operator = Operators.EqualTo;
			_clause.Value = $"\"{value}\"";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause In(params string[] values)
		{
			_clause.Operator = Operators.In;
			_clause.Value = "(" + string.Join(", ", values.Select(value => $"\"{value}\"")) + ")";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}
	}
}