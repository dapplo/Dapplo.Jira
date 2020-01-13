// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     A clause for simple values like ancestor, Id, label, space and more
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