// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2019 Dapplo
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

using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Jira.Tests
{
	public class SessionTests : TestBase
	{
		public SessionTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, false)
		{
		}

		//[Fact]
		public async Task TestSession()
		{
			if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
			{
				await Client.Session.StartAsync(Username, Password);
			}
			var me = await Client.User.GetMyselfAsync();
			Assert.Equal(me.Name, Username);
			await Client.Session.EndAsync();

			// WhoAmI should give an exception if there is no login
			await Assert.ThrowsAsync<JiraException>(async () => await Client.User.GetMyselfAsync());
		}
	}
}