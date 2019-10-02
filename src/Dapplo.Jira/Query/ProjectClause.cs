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
using Dapplo.Jira.Entities;

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     A clause for searching projects belonging to a project
	/// </summary>
	public class ProjectClause : IProjectClause
	{
		private readonly Clause _clause = new Clause
		{
			Field = Fields.Project
		};

		private bool _negate;

		/// <inheritDoc />
		public IProjectClause Not
		{
			get
			{
				_negate = !_negate;
				return this;
			}
		}

		/// <inheritDoc />
		public IFinalClause Is(string projectKey)
		{
			_clause.Operator = Operators.EqualTo;
			_clause.Value = projectKey;
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause Is(Project project)
		{
			return Is(project.Key);
		}

		/// <inheritDoc />
		public IFinalClause In(params string[] projectKeys)
		{
			_clause.Operator = Operators.In;
			_clause.Value = "(" + string.Join(", ", projectKeys) + ")";
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}

		/// <inheritDoc />
		public IFinalClause In(params Project[] projects)
		{
			return In(projects.Select(project => project.Key).ToArray());
		}

		/// <inheritDoc />
		public IFinalClause InProjectsLeadByUser()
		{
			return InFunction("projectsLeadByUser()");
		}

		/// <inheritDoc />
		public IFinalClause InProjectsWhereUserHasPermission()
		{
			return InFunction("projectsWhereUserHasPermission()");
		}

		/// <inheritDoc />
		public IFinalClause InProjectsWhereUserHasRole()
		{
			return InFunction("projectsWhereUserHasRole()");
		}

		/// <summary>
		/// Create clause for a function
		/// </summary>
		/// <param name="functionName">Name of the function</param>
		/// <returns>IFinalClause</returns>
		private IFinalClause InFunction(string functionName)
		{
			_clause.Operator = Operators.In;
			_clause.Value = functionName;
			if (_negate)
			{
				_clause.Negate();
			}
			return _clause;
		}
	}
}