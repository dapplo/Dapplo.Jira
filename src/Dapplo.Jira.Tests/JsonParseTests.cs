// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Jira.Tests;

public class JsonParseTests
{
    private readonly IJsonSerializer _jsonSerializer;
    private const string FilesDir = "JsonTestFiles";
    private readonly string _testFileLocation;

    public JsonParseTests(ITestOutputHelper testOutputHelper)
    {
        LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
        _jsonSerializer = new SystemTextJsonSerializer();
        _testFileLocation = FilesDir;
        if (!Directory.Exists(FilesDir))
        {
            _testFileLocation = Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), FilesDir);
        }
    }

    [Fact]
    public void TestParseIssue()
    {
        var json = File.ReadAllText(Path.Combine(_testFileLocation, "issue.json"));

        var issue = (Issue)_jsonSerializer.Deserialize(typeof(Issue), json);
        Assert.NotNull(issue);
    }

    [Fact]
    public void TestParseServerInfo()
    {
        var json = File.ReadAllText(Path.Combine(_testFileLocation, "serverInfo.json"));
        var serverInfo = (ServerInfo)_jsonSerializer.Deserialize(typeof(ServerInfo), json);
        Assert.NotNull(serverInfo);
        Assert.Equal("http://localhost:8080/jira", serverInfo.BaseUrl.AbsoluteUri);
        Assert.Equal("Greenshot JIRA", serverInfo.ServerTitle);
    }

    [Fact]
    public void TestParseProjects()
    {
        var json = File.ReadAllText(Path.Combine(_testFileLocation, "projects.json"));
        var projects = (IList<ProjectDigest>)_jsonSerializer.Deserialize(typeof(IList<ProjectDigest>), json);
        Assert.NotNull(projects);
        Assert.True(projects.Count > 0);
        Assert.Contains(projects, digest => "Greenshot bugs".Equals(digest.Name));
    }

    [Fact]
    public void TestParseAgileIssueOld()
    {
        var json = File.ReadAllText(Path.Combine(_testFileLocation, "agileIssueOld.json"));
        var issue = (AgileIssue)_jsonSerializer.Deserialize(typeof(AgileIssue), json);
        Assert.NotNull(issue);
        Assert.NotNull(issue.Sprint);
    }

    [Fact]
    public void TestParseAgileIssueNew()
    {
        var json = File.ReadAllText(Path.Combine(_testFileLocation, "agileIssueNew.json"));
        var issue = (AgileIssue)_jsonSerializer.Deserialize(typeof(AgileIssue), json);
        Assert.NotNull(issue);
        Assert.NotNull(issue.Sprint);
    }

    [Fact]
    public void TestParsePossibleTransitions()
    {
        var json = File.ReadAllText(Path.Combine(_testFileLocation, "possibleTransitions.json"));
        var transitions = (Transitions)_jsonSerializer.Deserialize(typeof(Transitions), json);
        Assert.NotNull(transitions);
        Assert.True(transitions.Items.Count > 0);
    }

    [Fact]
    public void TestParseServerConfiguration()
    {
        var json = File.ReadAllText(Path.Combine(_testFileLocation, "configuration.json"));
        var configuration = (Configuration)_jsonSerializer.Deserialize(typeof(Configuration), json);
        Assert.NotNull(configuration);
        Assert.NotNull(configuration.TimeTrackingConfiguration);
    }
}