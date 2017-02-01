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

using System.IO;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Jira.Tests
{
	public class JsonParseTests
	{
		public JsonParseTests(ITestOutputHelper testOutputHelper)
		{
			LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
		}

		[Fact]
		public void TestParseIssue()
		{
			var json = File.ReadAllText("JsonTestFiles/issue.json");
			var issue = SimpleJson.DeserializeObject<Issue>(json);
			Assert.NotNull(issue);
		}

		[Fact]
		public void TestParseServerInfo()
		{
			var json = File.ReadAllText("JsonTestFiles/serverInfo.json");
			var serverInfo = SimpleJson.DeserializeObject<ServerInfo>(json);
			Assert.NotNull(serverInfo);
			Assert.Equal("http://localhost:8080/jira", serverInfo.BaseUrl.AbsoluteUri);
			Assert.Equal("Greenshot JIRA", serverInfo.ServerTitle);
		}
	}
}