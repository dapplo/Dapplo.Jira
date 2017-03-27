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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Json;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Jira.Tests
{
	public class JsonParseTests
	{
		private readonly IJsonSerializer _jsonSerializer;
		public JsonParseTests(ITestOutputHelper testOutputHelper)
		{
			LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
			_jsonSerializer = new JsonNetJsonSerializer();
		}

		[Fact]
		public void TestParseIssue()
		{
			var json = File.ReadAllText("JsonTestFiles/issue.json");
			
			var issue = (Issue)_jsonSerializer.Deserialize(typeof(Issue), json);
			Assert.NotNull(issue);
		}

		[Fact]
		public void TestParseServerInfo()
		{
			var json = File.ReadAllText("JsonTestFiles/serverInfo.json");
			var serverInfo = (ServerInfo)_jsonSerializer.Deserialize(typeof(ServerInfo), json);
			Assert.NotNull(serverInfo);
			Assert.Equal("http://localhost:8080/jira", serverInfo.BaseUrl.AbsoluteUri);
			Assert.Equal("Greenshot JIRA", serverInfo.ServerTitle);
		}

		[Fact]
		public void TestParseProjects()
		{
			var json = File.ReadAllText("JsonTestFiles/projects.json");
			var projects = (IList<ProjectDigest>)_jsonSerializer.Deserialize(typeof(IList<ProjectDigest>), json);
			Assert.NotNull(projects);
			Assert.True(projects.Count > 0);
			Assert.True(projects.Any(digest => "Greenshot bugs".Equals(digest.Name)));
		}
		[Fact]
		public void TestParseAgileIssue()
		{
			var json = File.ReadAllText("JsonTestFiles/agileIssue.json");
			var issue = (AgileIssue)_jsonSerializer.Deserialize(typeof(AgileIssue), json);
			Assert.NotNull(issue);
		}

		[Fact]
		public void TestParsePossibleTransitions()
		{
			var json = File.ReadAllText("JsonTestFiles/possibleTransitions.json");
			var transitions = (Transitions)_jsonSerializer.Deserialize(typeof(Transitions), json);
			Assert.NotNull(transitions);
			Assert.True(transitions.Items.Count > 0);
		}

		[Fact]
		public void TestParseServerConfiguration()
		{
			var json = File.ReadAllText("JsonTestFiles/configuration.json");
			var configuration = (Configuration)_jsonSerializer.Deserialize(typeof(Configuration), json);
			Assert.NotNull(configuration);
			Assert.NotNull(configuration.TimeTrackingConfiguration);
		}
	}
}