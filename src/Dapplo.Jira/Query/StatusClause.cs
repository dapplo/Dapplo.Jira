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

using Dapplo.Jira.Entities;

#endregion

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     A clause for checking the status
	/// </summary>
	public class StatusClause : IStatusClause
    {
		private readonly Clause _clause = new Clause
		{
			Field = Fields.Status
		};

		private bool _negate;

		/// <inheritDoc />
		public IStatusClause Not
		{
			get
			{
				_negate = !_negate;
				return this;
			}
		}

	    /// <inheritDoc />
	    public IFinalClause Was(string state)
	    {
	        _clause.Operator = Operators.Was;
	        _clause.Value = state;
	        if (_negate)
	        {
	            _clause.Negate();
	        }
	        return _clause;
	    }

        /// <inheritDoc />
        public IFinalClause Is(string state)
		{
			_clause.Operator = Operators.EqualTo;
			_clause.Value = state;
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
		public IFinalClause In(params string[] states)
		{
			_clause.Operator = Operators.In;
			_clause.Value = "(" + string.Join(", ", states) + ")";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}
	}
}