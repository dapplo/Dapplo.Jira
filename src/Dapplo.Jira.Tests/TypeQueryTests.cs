// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dapplo.Jira.Query;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Jira.Tests
{
    public class TypeQueryTests
    {
        public TypeQueryTests(ITestOutputHelper testOutputHelper)
        {
            LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
        }

        [Fact]
        public void TestTypeIn()
        {
            var whereClause = Where.Type.In("BUG", "FEATURE");
            Assert.Equal("type in (BUG, FEATURE)", whereClause.ToString());
        }

        [Fact]
        public void TestTypeIs()
        {
            var whereClause = Where.Type.Is("BUG");
            Assert.Equal("type = BUG", whereClause.ToString());
        }

        [Fact]
        public void TestTypeNotIn()
        {
            var whereClause = Where.Type.Not.In("BUG", "FEATURE");
            Assert.Equal("type not in (BUG, FEATURE)", whereClause.ToString());
        }

        [Fact]
        public void TestTypeNotIs()
        {
            var whereClause = Where.Type.Not.Is("BUG");
            Assert.Equal("type != BUG", whereClause.ToString());
        }
    }
}
