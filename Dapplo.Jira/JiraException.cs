using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using Dapplo.Jira.Entities;

namespace Dapplo.Jira
{
	/// <summary>
	/// This wraps the 
	/// </summary>
	public class JiraException : HttpRequestException
	{
		public JiraException(HttpStatusCode httpStatusCode, string response) : base($"{httpStatusCode}({(int)httpStatusCode}) : {response}")
		{
			Errors = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
			ErrorMessages = new ReadOnlyCollection<string>(new List<string>());
		}

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
