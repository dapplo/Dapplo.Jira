using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;

namespace Dapplo.Jira
{
	/// <summary>
	/// The methods of the issue domain
	/// </summary>
	public interface IIssueApi
	{

		/// <summary>
		///     Add comment to the specified issue
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1139
		/// </summary>
		/// <param name="issueKey">key for the issue</param>
		/// <param name="body">the body of the comment</param>
		/// <param name="visibility">optional visibility role</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Attachment</returns>
		Task AddCommentAsync(string issueKey, string body, string visibility = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Get issue information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e4539
		/// </summary>
		/// <param name="issueKey">the issue key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Issue</returns>
		Task<Issue> GetAsync(string issueKey, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Get possible transitions for the specified issue
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1289
		/// </summary>
		/// <param name="issueKey">the issue key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Issue</returns>
		Task<IList<Transition>> GetPossibleTransitionsAsync(string issueKey, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		///     Search for issues, with a JQL (e.g. from a filter)
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e2713
		/// </summary>
		/// <param name="jql">Jira Query Language, like SQL, for the search</param>
		/// <param name="maxResults">Maximum number of results returned, default is 20</param>
		/// <param name="fields">Jira fields to include, if null the defaults from the JiraConfig.SearchFields are taken</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>SearchResult</returns>
		Task<SearchResult> SearchAsync(string jql, int maxResults = 20, IEnumerable<string> fields = null, CancellationToken cancellationToken = default(CancellationToken));
	}
}