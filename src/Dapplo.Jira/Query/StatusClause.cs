// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Dapplo.Jira.Entities;

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
            _clause.Value = EscapeState(state);
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
            _clause.Value = EscapeState(state);
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
            _clause.Value = "(" + string.Join(", ", states.Select(EscapeState)) + ")";
            if (_negate)
            {
                _clause.Negate();
            }

            return _clause;
        }

        private string EscapeState(string state) => $"\"{state}\"";
    }
}
