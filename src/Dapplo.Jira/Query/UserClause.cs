// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Dapplo.Jira.Entities;

namespace Dapplo.Jira.Query
{
    /// <summary>
    /// This is the implementation of all user based where clauses
    /// </summary>
    public class UserClause : IUserClause
    {
        private readonly Fields[] _allowedFields =
        {
            Fields.Approvals, Fields.Assignee, Fields.Creator, Fields.Reporter, Fields.Voter, Fields.Watcher, Fields.WorkLogAuthor
        };

        private readonly Clause _clause;
        private bool _negate;

        internal UserClause(Fields userField)
        {
            if (!_allowedFields.Any(field => userField == field))
            {
                throw new InvalidOperationException("Can't add function for the field {Field}");
            }

            _clause = new Clause
            {
                Field = userField
            };
        }


        /// <inheritDoc />
        public IFinalClause IsCurrentUser
        {
            get
            {
                _clause.Value = "currentUser()";
                if (_negate)
                {
                    _clause.Negate();
                }

                return _clause;
            }
        }

        /// <inheritDoc />
        public IUserClause Not
        {
            get
            {
                _negate = !_negate;
                return this;
            }
        }

        /// <inheritDoc />
        public IFinalClause Is(string user)
        {
            _clause.Operator = Operators.EqualTo;
            _clause.Value = $"\"{user}\"";
            if (_negate)
            {
                _clause.Negate();
            }

            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause Is(User user)
        {
            return Is(user.Name);
        }

        /// <inheritDoc />
        public IFinalClause In(params string[] users)
        {
            _clause.Operator = Operators.In;
            _clause.Value = "(" + string.Join(", ", users.Select(user => $"\"{user}\"")) + ")";
            if (_negate)
            {
                _clause.Negate();
            }

            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause In(params User[] users)
        {
            return In(users.Select(user => user.Name).ToArray());
        }

        /// <inheritDoc />
        public IFinalClause InCurrentUserAnd(params string[] users)
        {
            _clause.Operator = Operators.In;
            _clause.Value = "(currentUser(), " + string.Join(", ", users.Select(user => $"\"{user}\"")) + ")";
            if (_negate)
            {
                _clause.Negate();
            }

            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause InCurrentUserAnd(params User[] users)
        {
            return InCurrentUserAnd(users.Select(user => user.Name).ToArray());
        }
    }
}
