using System;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;

namespace Dapplo.Jira
{
	/// <summary>
	/// This is the interface which describes the Atlassian Jira client
	/// </summary>
	public interface IJiraClient
	{
		/// <summary>
		///     Store the specific HttpBehaviour, which contains a IHttpSettings and also some additional logic for making a
		///     HttpClient which works with Jira
		/// </summary>
		IHttpBehaviour Behaviour { get; }

		/// <summary>
		///     The base URI for your JIRA server
		/// </summary>
		Uri JiraBaseUri { get; }

		/// <summary>
		///     Issue domain
		/// </summary>
		IIssueDomain Issue { get; }

		/// <summary>
		///     Attachment domain
		/// </summary>
		IAttachmentDomain Attachment { get; }

		/// <summary>
		///     Project domain
		/// </summary>
		IProjectDomain Project { get; }

		/// <summary>
		///     User domain
		/// </summary>
		IUserDomain User { get; }

		/// <summary>
		///     Session domain
		/// </summary>
		ISessionDomain Session { get; }

		/// <summary>
		///     Filter domain
		/// </summary>
		IFilterDomain Filter { get; }

		/// <summary>
		///     Work domain
		/// </summary>
		IWorkDomain Work { get; }

		/// <summary>
		///     Server domain
		/// </summary>
		IServerDomain Server { get; }

		/// <summary>
		///     Set Basic Authentication for the current client
		/// </summary>
		/// <param name="user">username</param>
		/// <param name="password">password</param>
		void SetBasicAuthentication(string user, string password);
	}

	/// <summary>
	/// This interface describes the functionality of the IJiraClient which domains can use
	/// </summary>
	public interface IJiraDomain : IJiraClient
	{
		/// <summary>
		///     The rest URI for your JIRA server
		/// </summary>
		Uri JiraRestUri { get; }

		/// <summary>
		///     The base URI for JIRA auth api
		/// </summary>
		Uri JiraAuthUri { get; }
	}
}