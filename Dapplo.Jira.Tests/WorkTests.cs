//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Jira
// 
//  Dapplo.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Jira.Tests
{
	public class WorkTests : TestBase
	{
		public WorkTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, false)
		{
		}

		[Fact]
		public async Task TestWorklogs()
		{
			var worklogs = await Client.Work.GetAsync("BUG-2104");
			Assert.NotNull(worklogs);
			Assert.True(worklogs.Elements.Count > 0);
		}

		[Fact]
		public async Task TestLogWork()
		{
			const string issueKey = "BUG-2104";
			var worklog = await Client.Work.CreateAsync(issueKey, new Worklog(TimeSpan.FromHours(16))
			{
				Comment = "Testing the logging of work"
			});

			Assert.NotNull(worklog);
			Assert.True(worklog.TimeSpent == "2d");
			worklog.TimeSpent = "3d";
			await Client.Work.UpdateAsync(issueKey, worklog);

			// Delete again
			await Client.Work.DeleteAsync(issueKey, worklog);
		}
	}
}