// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Threading;
using Dapplo.HttpExtensions;
using Dapplo.HttpExtensions.ContentConverter;
using Dapplo.HttpExtensions.Extensions;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit;

namespace Dapplo.Jira.Tests;

/// <summary>
///     Abstract base class for all tests
/// </summary>
public abstract class TestBase
{
    protected static readonly LogSource Log = new LogSource();
    protected const string TestProjectKey = "DIT";
    protected const string TestIssueKey = "DIT-1";
    protected const string TestIssueKey2 = "DIT-123";
    protected const string TestSubTaskIssueKey = "DIT-179";

    // Test against a well known JIRA
    protected static readonly Uri TestJiraUri = new Uri("https://greenshot.atlassian.net");

    /// <summary>
    ///     Default test setup, can also take care of setting the authentication
    /// </summary>
    /// <param name="testOutputHelper">ITestOutputHelper</param>
    /// <param name="doLogin">bool</param>
    protected TestBase(ITestOutputHelper testOutputHelper, bool doLogin = true)
    {
        var ci = new CultureInfo("en-US");
        Thread.CurrentThread.CurrentCulture = ci;
        Thread.CurrentThread.CurrentUICulture = ci;

        var defaultJsonHttpContentConverterConfiguration = new DefaultJsonHttpContentConverterConfiguration
        {
            LogThreshold = 0
        };
        HttpBehaviour.Current.SetConfig(defaultJsonHttpContentConverterConfiguration);

        LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
        Client = JiraClient.Create(TestJiraUri);
        Username = Environment.GetEnvironmentVariable("JIRA_TEST_USERNAME");
        Password = Environment.GetEnvironmentVariable("JIRA_TEST_PASSWORD");

        if (doLogin && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
        {
            Client.SetBasicAuthentication(Username, Password);
        }
    }

    /// <summary>
    ///     The instance of the JiraClient
    /// </summary>
    protected IJiraClient Client { get; }

    protected string Username { get; }
    protected string Password { get; }
}
