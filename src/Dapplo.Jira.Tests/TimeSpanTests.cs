// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dapplo.Jira.Entities;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Jira.Tests
{
    public class TimeSpanTests : TestBase
    {
        public TimeSpanTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        public void TestJiraTimeRangeToTimeSpan_RealWeek_FromWorkingTime()
        {
            var testTimeSpan = TimeSpanExtensions.FromWorkingTime("4w 1d 1h 10m");
            var testResult = TimeSpan.FromDays(7);
            testResult = testResult.Add(TimeSpan.FromHours(1));
            testResult = testResult.Add(TimeSpan.FromMinutes(10));
            Assert.Equal(testResult, testTimeSpan);
        }

        [Fact]
        public void TestJiraTimeRangeToTimeSpan_RealWeek_7HalfHours_FromWorkingTime()
        {
            var timeTrackingConfiguration = new TimeTrackingConfiguration
            {
                WorkingHoursPerDay = 7.5f
            };
            var testTimeSpan = TimeSpanExtensions.FromWorkingTime("1w 1d 1h 10m", timeTrackingConfiguration);
            // in hours => (5 (Working days per week) + 1 )* 7,5 (working hours per day)
            var testResult = TimeSpan.FromHours(6 * timeTrackingConfiguration.WorkingHoursPerDay);
            testResult = testResult.Add(TimeSpan.FromHours(1));
            testResult = testResult.Add(TimeSpan.FromMinutes(10));
            Assert.Equal(testResult, testTimeSpan);
        }

        [Fact]
        public void TestTimeSpan_ToJiraTimeRange_RealWeek_ToWorkingTime()
        {
            var testTimeSpan = TimeSpan.FromDays(7);
            var jiraTimeRange = testTimeSpan.ToWorkingTime();
            Assert.Equal("4w 1d", jiraTimeRange);
        }

        [Fact]
        public void TestTimeSpan_ToJiraTimeRange_RealWeek_7HalfHours_ToWorkingTime()
        {
            var timeTrackingConfiguration = new TimeTrackingConfiguration
            {
                WorkingHoursPerDay = 7.5f
            };
            var testTimeSpan = TimeSpan.FromDays(7);
            var jiraTimeRange = testTimeSpan.ToWorkingTime(timeTrackingConfiguration);
            Assert.Equal("4w 2d 3h", jiraTimeRange);
        }

        [Fact]
        public void TestTimeSpan_ToJiraTimeRange_RealWeekInMinutes_ToWorkingTime()
        {
            var testTimeSpan = TimeSpan.FromMinutes(7 * 24 * 60);
            var jiraTimeRange = testTimeSpan.ToWorkingTime();
            Assert.Equal("4w 1d", jiraTimeRange);
        }
    }
}
