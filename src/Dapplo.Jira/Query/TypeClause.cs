// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
