// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Dapplo.Log;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Jira.Tests
{
    public class JiraTests : TestBase
    {
        public JiraTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        public void TestConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => JiraClient.Create(null));
        }


        [Fact]
        public async Task TestGetFieldsAsync()
        {
            var fields = await Client.Server.GetFieldsAsync();
            Assert.True(fields.Count > 0);
            Assert.Contains(fields, field => field.Id == "issuetype");
        }

        [Fact]
        public async Task TestGetServerConfigurationAsync()
        {
            Assert.NotNull(Client);
            var configuration = await Client.Server.GetConfigurationAsync();
            Assert.NotNull(configuration.TimeTrackingConfiguration.TimeFormat);
        }

        [Fact]
        public async Task TestGetServerInfoAsync()
        {
            Assert.NotNull(Client);
            var serverInfo = await Client.Server.GetInfoAsync();
            Assert.NotNull(serverInfo.Version);
            Assert.NotNull(serverInfo.ServerTitle);
            Assert.True(serverInfo.ServerTitle.Length > 0);
            Log.Debug().WriteLine($"Version {serverInfo.Version} - Title: {serverInfo.ServerTitle}");
        }
    }
}
