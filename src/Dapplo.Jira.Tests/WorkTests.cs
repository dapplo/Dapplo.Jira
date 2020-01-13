// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Threading.Tasks;
using Dapplo.HttpExtensions.JsonNet;
using Dapplo.Jira.Entities;
using Dapplo.Log;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Jira.Tests
{
    public class WorkTests : TestBase
    {
        public WorkTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        public void TestLogWork_Serializing()
        {
            var started = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(2));
            // Make sure we only have millis, ignoring the ticks
            started = started.AddTicks(-started.Ticks % TimeSpan.TicksPerMillisecond);

            var worklog = new Worklog
            {
                TimeSpentSeconds = (long) TimeSpan.FromHours(16).TotalSeconds,
                Comment = "Testing the logging of work",
                Started = started
            };
            var serializer = new JsonNetJsonSerializer();
            var json = serializer.Serialize(worklog);
            Log.Info().WriteLine(json);
            var deserializedWorklog = (Worklog)serializer.Deserialize(typeof(Worklog), json);
            Assert.Equal(worklog.Started, deserializedWorklog.Started);
        }

        [Fact]
        public async Task TestLogWork()
        {
            var started = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(2));

            var newWorkLog = new Worklog
            {
                TimeSpentSeconds = (long) TimeSpan.FromHours(16).TotalSeconds,
                Comment = "Testing the logging of work",
                Started = started
            };

            var worklog = await Client.Work.CreateAsync(TestIssueKey, newWorkLog);

            Assert.NotNull(worklog);
            Assert.Equal("2d", worklog.TimeSpent);
            Assert.NotNull(worklog.Started);
            var worklogStarted = worklog.Started.Value;
            Assert.Equal(started.AddTicks(-started.Ticks % TimeSpan.TicksPerSecond), worklogStarted.AddTicks(-worklogStarted.Ticks % TimeSpan.TicksPerSecond));
            worklog.TimeSpent = "3d";
            worklog.TimeSpentSeconds = null;
            worklog.Started = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(3));
            await Client.Work.UpdateAsync(TestIssueKey, worklog);
            var worklogs = await Client.Work.GetAsync(TestIssueKey);
            var retrievedWorklog = worklogs.FirstOrDefault(worklogItem => string.Equals(worklog.Id, worklogItem.Id));
            Assert.NotNull(retrievedWorklog);
            Assert.Equal("3d", retrievedWorklog.TimeSpent);

            // Delete again
            await Client.Work.DeleteAsync(TestIssueKey, worklog);
        }

        [Fact]
        public async Task TestWorklogs()
        {
            var worklogs = await Client.Work.GetAsync(TestIssueKey);
            Assert.NotNull(worklogs);
            Assert.True(worklogs.Elements.Count > 0);
        }
    }
}