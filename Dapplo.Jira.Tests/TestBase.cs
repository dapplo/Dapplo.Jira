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
using Dapplo.HttpExtensions.ContentConverter;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Jira.Tests
{
	/// <summary>
	///     Abstract base class for all tests
	/// </summary>
	public abstract class TestBase
	{
		protected static readonly LogSource Log = new LogSource();
		protected static readonly string TestIssueKey = "DIT-1";

		// Test against a well known JIRA
		private static readonly Uri TestJiraUri = new Uri("https://greenshot.atlassian.net");

		/// <summary>
		///     Default test setup, can also take care of setting the authentication
		/// </summary>
		/// <param name="testOutputHelper"></param>
		/// <param name="doLogin"></param>
		protected TestBase(ITestOutputHelper testOutputHelper, bool doLogin = true)
		{
			// For tests, make sure we get everything
			DefaultJsonHttpContentConverter.Instance.Value.LogThreshold = 0;

			LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
			Client = JiraClient.Create(TestJiraUri);
			Username = Environment.GetEnvironmentVariable("jira_test_username");
			Password = Environment.GetEnvironmentVariable("jira_test_password");

			if (doLogin && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
			{
				Client.SetBasicAuthentication("robin.krom@gmail.com", Password);
			}
		}

		/// <summary>
		///     The instance of the JiraClient
		/// </summary>
		protected IJiraClient Client { get; }

		protected string Username { get; }
		protected string Password { get; }
	}
}