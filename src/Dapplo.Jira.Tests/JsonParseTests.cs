// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using Dapplo.HttpExtensions;
using Dapplo.HttpExtensions.JsonNet;
using Dapplo.Jira.Entities;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit;

namespace Dapplo.Jira.Tests;

public class JsonParseTests
{
    private readonly IJsonSerializer jsonSerializer;
    private const string FilesDir = "JsonTestFiles";
    private readonly string testFileLocation;

    public JsonParseTests(ITestOutputHelper testOutputHelper)
    {
        LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
        jsonSerializer = new JsonNetJsonSerializer();
        testFileLocation = FilesDir;
        if (!Directory.Exists(FilesDir))
        {
            testFileLocation = Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), FilesDir);
        }
    }

    [Fact]
    public void TestParseIssue()
    {
        var json = File.ReadAllText(Path.Combine(testFileLocation, "issue.json"));

        var issue = (IssueV2)jsonSerializer.Deserialize(typeof(IssueV2), json);
        Assert.NotNull(issue);
    }

    [Fact]
    public void TestParseServerInfo()
    {
        var json = File.ReadAllText(Path.Combine(testFileLocation, "serverInfo.json"));
        var serverInfo = (ServerInfo)jsonSerializer.Deserialize(typeof(ServerInfo), json);
        Assert.NotNull(serverInfo);
        Assert.Equal("http://localhost:8080/jira", serverInfo.BaseUrl.AbsoluteUri);
        Assert.Equal("Greenshot JIRA", serverInfo.ServerTitle);
    }

    [Fact]
    public void TestParseProjects()
    {
        var json = File.ReadAllText(Path.Combine(testFileLocation, "projects.json"));
        var projects = (IList<ProjectDigest>)jsonSerializer.Deserialize(typeof(IList<ProjectDigest>), json);
        Assert.NotNull(projects);
        Assert.True(projects.Count > 0);
        Assert.Contains(projects, digest => "Greenshot bugs".Equals(digest.Name));
    }

    [Fact]
    public void TestParseAgileIssueOld()
    {
        var json = File.ReadAllText(Path.Combine(testFileLocation, "agileIssueOld.json"));
        var issue = (AgileIssue)jsonSerializer.Deserialize(typeof(AgileIssue), json);
        Assert.NotNull(issue);
        Assert.NotNull(issue.Sprint);
    }

    [Fact]
    public void TestParseAgileIssueNew()
    {
        var json = File.ReadAllText(Path.Combine(testFileLocation, "agileIssueNew.json"));
        var issue = (AgileIssue)jsonSerializer.Deserialize(typeof(AgileIssue), json);
        Assert.NotNull(issue);
        Assert.NotNull(issue.Sprint);
    }

    [Fact]
    public void TestParsePossibleTransitions()
    {
        var json = File.ReadAllText(Path.Combine(testFileLocation, "possibleTransitions.json"));
        var transitions = (Transitions)jsonSerializer.Deserialize(typeof(Transitions), json);
        Assert.NotNull(transitions);
        Assert.True(transitions.Items.Count > 0);
    }

    [Fact]
    public void TestParseServerConfiguration()
    {
        var json = File.ReadAllText(Path.Combine(testFileLocation, "configuration.json"));
        var configuration = (Configuration)jsonSerializer.Deserialize(typeof(Configuration), json);
        Assert.NotNull(configuration);
        Assert.NotNull(configuration.TimeTrackingConfiguration);
    }
}
