#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Confluence
// 
// Dapplo.Confluence is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Confluence is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Confluence. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings



#endregion

namespace Dapplo.Jira.Query
{
	/// <summary>
	/// An interface for a date time calculations clause
	/// </summary>
	public interface ITextClause
	{
		/// <summary>
		///     Negates the expression
		/// </summary>
		ITextClause Not { get; }

		/// <summary>
		///     This allows fluent constructs like Text.Contains(customernumber)
		/// </summary>
		IFinalClause Contains(string value);
	}

	/// <summary>
	/// A clause for simple values like ancestor, Id, label, space and more
	/// </summary>
	public class TextClause : ITextClause
	{
		private readonly Clause _clause;
		private bool _negate;

		internal TextClause(Fields textField)
		{
			_clause = new Clause
			{
				Field = textField
			};
		}


		/// <inheritDoc />
		public ITextClause Not
		{
			get
			{
				_negate = !_negate;
				return this;
			}
		}

		/// <inheritDoc />
		public IFinalClause Contains(string value)
		{
			_clause.Operator = Operators.Contains;
			_clause.Value = $"\"{value}\"";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}
	}
}