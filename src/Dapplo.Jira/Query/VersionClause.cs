// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;

namespace Dapplo.Jira.Query
{
    /// <summary>
    ///     A clause for version values like fixVersion and more
    /// </summary>
    public class VersionClause : IVersionClause
    {
        private readonly Clause _clause;

        private bool _negate;

        /// <summary>
        /// contructor of a version clause with a field
        /// </summary>
        /// <param name="versionField">Fields</param>
        public VersionClause(Fields versionField)
        {
            _clause = new Clause
            {
                Field = versionField
            };
        }

        /// <inheritDoc />
        public IVersionClause Not
        {
            get
            {
                _negate = !_negate;
                return this;
            }
        }

        /// <inheritDoc />
        public IFinalClause Is(string version)
        {
            _clause.Operator = Operators.EqualTo;
            _clause.Value = $"\"{version}\"";
            if (_negate)
            {
                _clause.Negate();
            }

            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause In(params string[] versions)
        {
            _clause.Operator = Operators.In;
            _clause.Value = "(" + string.Join(", ", versions.Select(version => $"\"{version}\"")) + ")";
            if (_negate)
            {
                _clause.Negate();
            }

            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause InReleasedVersions(string project = null)
        {
            _clause.Operator = Operators.In;
            _clause.Value = $"releasedVersions({project})";
            if (_negate)
            {
                _clause.Negate();
            }

            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause InLatestReleasedVersion(string project)
        {
            _clause.Operator = Operators.In;
            _clause.Value = $"latestReleasedVersion({project})";
            if (_negate)
            {
                _clause.Negate();
            }

            return _clause;
        }

        /// <inheritDoc />
        public IFinalClause InUnreleasedVersions(string project = null)
        {
            _clause.Operator = Operators.In;
            _clause.Value = $"unreleasedVersions({project})";
            if (_negate)
            {
                _clause.Negate();
            }

            return _clause;
        }


        /// <inheritDoc />
        public IFinalClause InEarliestUnreleasedVersion(string project)
        {
            _clause.Operator = Operators.In;
            _clause.Value = $"earliestUnreleasedVersion({project})";
            if (_negate)
            {
                _clause.Negate();
            }

            return _clause;
        }
    }
}
