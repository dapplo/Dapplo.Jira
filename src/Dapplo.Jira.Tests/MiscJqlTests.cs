// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Query;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Jira.Tests
{
	public class MiscJqlTests
	{
		public MiscJqlTests(ITestOutputHelper testOutputHelper)
		{
			LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
		}

		[Fact]
		public void Issue44()
        {
            var project = new Project
            {
                Key = "PROJ",
                IssueTypes = new List<IssueType>
                {
                    new IssueType {Id = 1, Name = "Bug"}
                }
            };

            var issueTypes = new List<string> {"Bug"};
            var startDate = DateTime.FromFileTime(1234567890);
            var endDate = DateTime.FromFileTime(12356789000);
            var jql = Where.And(
                Where.Project.Is(project),
                Where.Type.In(project.IssueTypes.Where(t => issueTypes.Contains(t.Name)).ToArray()),
                Where.Or(
                    Where.And(
                        Where.Resolved.AfterOrOn.DateTime(startDate),
                        Where.Resolved.BeforeOrOn.DateTime(endDate)
                    ),
                    Where.Status.Is("End to End Testing")
                )
            );
            var jqlString = jql.ToString();
            Assert.Equal("(project = PROJ and type in (1) and ((resolved >= \"1601-01-01 00:02\" and resolved <= \"1601-01-01 00:20\") or status = \"End to End Testing\"))", jqlString);
        }

	}
}