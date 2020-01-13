// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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