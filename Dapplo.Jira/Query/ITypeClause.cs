using Dapplo.Jira.Entities;

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     The possible methods for a type clause
	/// </summary>
	public interface ITypeClause
	{
		/// <summary>
		///     Negates the expression
		/// </summary>
		ITypeClause Not { get; }

		/// <summary>
		///     Test if the type of the content is one of the specified types
		/// </summary>
		/// <param name="types">Types</param>
		/// <returns>IFinalClause</returns>
		IFinalClause In(params string[] types);

		/// <summary>
		///     Test if the type of the content is one of the specified types
		/// </summary>
		/// <param name="types">IssueType array</param>
		/// <returns>IFinalClause</returns>
		IFinalClause In(params IssueType[] types);

		/// <summary>
		///     This allows fluent constructs like Type.Is("Feature")
		/// </summary>
		IFinalClause Is(string type);

		/// <summary>
		///     Test if the type of the content is the specified type
		/// </summary>
		/// <param name="type">IssueType</param>
		/// <returns>IFinalClause</returns>
		IFinalClause Is(IssueType type);
	}
}