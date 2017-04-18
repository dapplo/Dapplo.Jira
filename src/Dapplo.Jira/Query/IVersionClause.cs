namespace Dapplo.Jira.Query
{
	/// <summary>
	///     An interface for a version based clauses
	/// </summary>
	public interface IVersionClause
	{
		/// <summary>
		///     Negates the expression
		/// </summary>
		IVersionClause Not { get; }

		/// <summary>
		///     This allows fluent constructs like IssueKey.In(BUG-1234, FEATURE-5678)
		/// </summary>
		IFinalClause In(params string[] issueKeys);

		/// <summary>
		///     This allows fluent constructs like IssueKey.InEarliestUnreleasedVersion(BUGS)
		/// </summary>
		IFinalClause InEarliestUnreleasedVersion(string project);

		/// <summary>
		///     This allows fluent constructs like IssueKey.InLatestReleasedVersion(BUGS)
		/// </summary>
		IFinalClause InLatestReleasedVersion(string project);

		/// <summary>
		///     This allows fluent constructs like IssueKey.InReleasedVersions()
		/// </summary>
		IFinalClause InReleasedVersions(string project = null);

		/// <summary>
		///     This allows fluent constructs like IssueKey.InUnreleasedVersions()
		/// </summary>
		IFinalClause InUnreleasedVersions(string project = null);

		/// <summary>
		///     This allows fluent constructs like Id.Is(12345)
		/// </summary>
		IFinalClause Is(string issueKey);
	}
}