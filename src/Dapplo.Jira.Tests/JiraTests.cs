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
using System.Linq;
using System.Threading.Tasks;
using Dapplo.Log;
using Xunit;
using Xunit.Abstractions;

#endregion

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
			// This should be changed when the title changes
			Assert.Equal("Greenshot JIRA", serverInfo.ServerTitle);
			Log.Debug().WriteLine($"Version {serverInfo.Version} - Title: {serverInfo.ServerTitle}");
		}
	}
}