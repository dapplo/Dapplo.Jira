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
using Dapplo.Jira.Entities;
using Xunit;
using Xunit.Abstractions;

#endregion

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