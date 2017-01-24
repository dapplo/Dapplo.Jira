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
		///     Set Basic Authentication for the current client
		/// </summary>
		/// <param name="user">username</param>
		/// <param name="password">password</param>
		void SetBasicAuthentication(string user, string password);

		/// <summary>
		///     Returns the content, specified by the Uri from the JIRA server.
		///     This is used internally, but can also be used to get e.g. the icon for an issue type.
		/// </summary>
		/// <typeparam name="TResponse"></typeparam>
		/// <param name="contentUri">Uri</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>TResponse</returns>
		Task<TResponse> GetUriContentAsync<TResponse>(Uri contentUri, CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class;

		/// <summary>
		///     Retrieve the Avatar for the supplied avatarUrls object
		/// </summary>
		/// <typeparam name="TResponse">the type to return the result into. e.g. Bitmap,BitmapSource or MemoryStream</typeparam>
		/// <param name="avatarUrls">AvatarUrls object from User or Myself method, or a project from the projects</param>
		/// <param name="avatarSize">Use one of the AvatarSizes to specify the size you want to have</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Bitmap,BitmapSource or MemoryStream (etc) depending on TResponse</returns>
		Task<TResponse> GetAvatarAsync<TResponse>(AvatarUrls avatarUrls, AvatarSizes avatarSize = AvatarSizes.ExtraLarge,
			CancellationToken cancellationToken = default(CancellationToken))
			where TResponse : class;

		/// <summary>
		///     Get server information
		///     See: https://docs.atlassian.com/jira/REST/latest/#d2e3828
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>ServerInfo</returns>
		Task<ServerInfo> GetServerInfoAsync(CancellationToken cancellationToken = default(CancellationToken));
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

		/// <summary>
		///     Helper method for handling errors in the response, if the response has an error an exception is thrown.
		///     Else the real response is returned.
		/// </summary>
		/// <typeparam name="TResponse">Type for the ok content</typeparam>
		/// <typeparam name="TError">Type for the error content</typeparam>
		/// <param name="response">TResponse</param>
		/// <returns>TResponse</returns>
		TResponse HandleErrors<TResponse, TError>(HttpResponse<TResponse, TError> response) where TResponse : class where TError : Error;
	}
}