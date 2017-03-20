using Dapplo.Jira.Entities;

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     An interface for project based clauses
	/// </summary>
	public interface IProjectClause
	{
		/// <summary>
		///     Negates the expression
		/// </summary>
		IProjectClause Not { get; }

		/// <summary>
		///     This allows fluent constructs like Project.In(BUG, FEATURE)
		/// </summary>
		IFinalClause In(params string[] projectKeys);

		/// <summary>
		///     This allows fluent constructs like Project.In(project1, project2)
		/// </summary>
		IFinalClause In(params Project[] projects);

		/// <summary>
		///     This allows fluent constructs like Project.InProjectsLeadByUser()
		/// </summary>
		IFinalClause InProjectsLeadByUser();

		/// <summary>
		///     This allows fluent constructs like Project.InProjectsWhereUserHasPermission()
		/// </summary>
		IFinalClause InProjectsWhereUserHasPermission();

		/// <summary>
		///     This allows fluent constructs like Project.InProjectsWhereUserHasRole()
		/// </summary>
		IFinalClause InProjectsWhereUserHasRole();

		/// <summary>
		///     This allows fluent constructs like Id.Is(12345)
		/// </summary>
		IFinalClause Is(string projectKey);

		/// <summary>
		///     This allows fluent constructs like Id.Is(project)
		/// </summary>
		IFinalClause Is(Project project);
	}
}