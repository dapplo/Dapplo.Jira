using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using Dapplo.Jira.Entities;

namespace Dapplo.Jira
{
	/// <summary>
	/// This wraps the HttpRequestException with Jira specific informationen
	/// </summary>
	public class JiraException : HttpRequestException
	{
		/// <summary>
		/// Constructor with a HttpStatus code and an error response
		/// </summary>
		/// <param name="httpStatusCode">HttpStatusCode</param>
		/// <param name="response">string with the error response message</param>
		public JiraException(HttpStatusCode httpStatusCode, string response) : base($"{httpStatusCode}({(int)httpStatusCode}) : {response}")
		{
			Errors = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
			ErrorMessages = new ReadOnlyCollection<string>(new List<string>());
		}

		/// <summary>
		///  Constructor with a HttpStatus code and an Error object
		/// </summary>
		/// <param name="httpStatusCode">HttpStatusCode</param>
		/// <param name="error">Error</param>
		public JiraException(HttpStatusCode httpStatusCode, Error error = null) : base(error?.Message ?? $"{httpStatusCode}({(int)httpStatusCode})")
		{
			Errors = new ReadOnlyDictionary<string, string>(error?.Errors ?? new Dictionary<string, string>());
			ErrorMessages = new ReadOnlyCollection<string>(error?.ErrorMessages ?? new List<string>());
		}

		/// <summary>
		/// Get the errors
		/// </summary>
		public IReadOnlyDictionary<string, string> Errors { get; }

		/// <summary>
		/// Get the error messages
		/// </summary>
		public IEnumerable<string> ErrorMessages { get; }
	}
}
