#region Dapplo 2017 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Jira
// 
// Dapplo.Jira is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Jira is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings

using System;
using System.Net;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;
using Dapplo.Log;

#endregion

namespace Dapplo.Jira
{
	public static class HttpResponseExtensions
	{
		private static readonly LogSource Log = new LogSource();

		/// <summary>
		///     Helper method for handling errors in the response, if the response has an error an exception is thrown.
		///     Else the real response is returned.
		/// </summary>
		/// <typeparam name="TResponse">Type for the ok content</typeparam>
		/// <typeparam name="TError">Type for the error content</typeparam>
		/// <param name="expectedHttpStatusCode">optional HttpStatusCode to expect</param>
		/// <param name="response">TResponse</param>
		/// <returns>TResponse</returns>
		public static TResponse HandleErrors<TResponse, TError>(this HttpResponse<TResponse, TError> response, HttpStatusCode? expectedHttpStatusCode = null)
			where TResponse : class where TError : Error
		{
			if (expectedHttpStatusCode.HasValue)
			{
				if (response.StatusCode == expectedHttpStatusCode.Value)
				{
					return response.Response;
				}
			}
			else if (!response.HasError)
			{
				return response.Response;
			}

			var message = response.StatusCode.ToString();
			if (response.ErrorResponse.ErrorMessages != null)
			{
				message = string.Join(", ", response.ErrorResponse.ErrorMessages);
			}
			else if (response.ErrorResponse?.Message != null)
			{
				message = response.ErrorResponse?.Message;
			}
			Log.Warn().WriteLine("Http status code: {0}. Response from server: {1}", response.StatusCode, message);
			throw new Exception($"Status: {response.StatusCode} Message: {message}");
		}

		/// <summary>
		///     Helper method for handling errors in the response, if the response doesn't have the expected status code an
		///     exception is thrown.
		///     Else the real response is returned.
		/// </summary>
		/// <typeparam name="TResponse">Type for the ok content</typeparam>
		/// <param name="expectedHttpStatusCode">HttpStatusCode to expect</param>
		/// <param name="response">TResponse</param>
		/// <returns>TResponse</returns>
		public static TResponse HandleErrors<TResponse>(this HttpResponse<TResponse> response, HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK)
			where TResponse : class
		{
			if (response.StatusCode != expectedHttpStatusCode)
			{
				var message = response.StatusCode.ToString();
				Log.Warn().WriteLine("Http status code: {0}. Response from server: {1}", response.StatusCode, message);
				throw new Exception($"Status: {response.StatusCode} Message: {message}");
			}
			return response.Response;
		}

		/// <summary>
		///     Helper method for handling errors in the response, if the response doesn't have the expected status code an
		///     exception is thrown.
		/// </summary>
		/// <param name="expectedHttpStatusCode">HttpStatusCode to expect</param>
		/// <param name="response">TResponse</param>
		public static void HandleStatusCode(this HttpResponse response, HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK)
		{
			if (response.StatusCode != expectedHttpStatusCode)
			{
				var message = response.StatusCode.ToString();
				Log.Warn().WriteLine("Http status code: {0}. Response from server: {1}", response.StatusCode, message);
				throw new Exception($"Status: {response.StatusCode} Message: {message}");
			}
		}

		/// <summary>
		///     Helper method for handling errors in the response, if the response doesn't have the expected status code an
		///     exception is thrown.
		/// </summary>
		/// <typeparam name="TError">Type for the error</typeparam>
		/// <param name="expectedHttpStatusCode">HttpStatusCode to expect</param>
		/// <param name="response">TResponse</param>
		public static void HandleStatusCode<TError>(this HttpResponseWithError<TError> response, HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK) where TError : Error
		{
			if (response.StatusCode != expectedHttpStatusCode)
			{
				var message = response.StatusCode.ToString();
				if (response.ErrorResponse.ErrorMessages != null)
				{
					message = string.Join(", ", response.ErrorResponse.ErrorMessages);
				}
				else if (response.ErrorResponse?.Message != null)
				{
					message = response.ErrorResponse?.Message;
				}
				Log.Warn().WriteLine("Http status code: {0}. Response from server: {1}", response.StatusCode, message);
				throw new Exception($"Status: {response.StatusCode} Message: {message}");
			}
		}
	}
}