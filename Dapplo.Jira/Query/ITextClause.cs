namespace Dapplo.Jira.Query
{
	/// <summary>
	///     An interface for a date time calculations clause
	/// </summary>
	public interface ITextClause
	{
		/// <summary>
		///     Negates the expression
		/// </summary>
		ITextClause Not { get; }

		/// <summary>
		///     This allows fluent constructs like Text.Contains(customernumber)
		/// </summary>
		IFinalClause Contains(string value);
	}
}