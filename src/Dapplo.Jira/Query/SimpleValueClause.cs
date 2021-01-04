// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
