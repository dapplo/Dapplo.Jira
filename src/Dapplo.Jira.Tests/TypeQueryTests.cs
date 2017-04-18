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

using Dapplo.Jira.Query;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit;
using Xunit.Abstractions;

#endregion

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