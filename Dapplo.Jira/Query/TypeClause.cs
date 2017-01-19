//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Jira
// 
//  Dapplo.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region Usings

#endregion

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
		///     Check if the type is on of the supplied ones
		/// </summary>
		/// <param name="types">array of types</param>
		IFinalClause In(params string[] types);

		/// <summary>
		///     This allows fluent constructs like Type.Is("Feature")
		/// </summary>
		IFinalClause Is(string type);
	}

	///
	public class TypeClause : ITypeClause
	{
		private readonly Clause _clause = new Clause
		{
			Field = Fields.Type
		};

		private bool _negate;

		public ITypeClause Not
		{
			get
			{
				_negate = !_negate;
				return this;
			}
		}

		/// <summary>
		///     Test if the type of the content is one of the specified types
		/// </summary>
		/// <param name="types">Types</param>
		/// <returns>IFinalClause</returns>
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

		/// <summary>
		///     Test if the type of the content is the specified type
		/// </summary>
		/// <param name="type">Type</param>
		/// <returns>IFinalClause</returns>
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
	}
}