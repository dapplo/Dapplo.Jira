using Dapplo.Jira.Entities;

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     An interface for issue based clauses
	/// </summary>
	public interface IIssueClause
	{
		/// <summary>
		///     Negates the expression
		/// </summary>
		IIssueClause Not { get; }

		/// <summary>
		///     This allows fluent constructs like IssueKey.In(BUG-1234, FEATURE-5678)
		/// </summary>
		IFinalClause In(params string[] issueKeys);

		/// <summary>
		///     This allows fluent constructs like IssueKey.In(issue1, issue2)
		/// </summary>
		IFinalClause In(params Issue[] issues);

		/// <summary>
		///     This allows fluent constructs like IssueKey.InIssueHistory()
		/// </summary>
		IFinalClause InIssueHistory();

		/// <summary>
		///     This allows fluent constructs like IssueKey.InLinkedIssues(BUG-12345)
		/// </summary>
		IFinalClause InLinkedIssues(string issueKey, string linkType = null);

		/// <summary>
		///     This allows fluent constructs like IssueKey.InLinkedIssues(issue1)
		/// </summary>
		IFinalClause InLinkedIssues(Issue issue, string linkType = null);

		/// <summary>
		///     This allows fluent constructs like IssueKey.InVotedIssues()
		///     Find issues that you have voted for.
		/// </summary>
		IFinalClause InVotedIssues();

		/// <summary>
		///     This allows fluent constructs like IssueKey.InWatchedIssues()
		///     Find issues that you have voted for.
		/// </summary>
		IFinalClause InWatchedIssues();

		/// <summary>
		///     This allows fluent constructs like Id.Is(12345)
		/// </summary>
		IFinalClause Is(string issueKey);

		/// <summary>
		///     This allows fluent constructs like Id.Is(issue1)
		/// </summary>
		IFinalClause Is(Issue issue);
	}
}