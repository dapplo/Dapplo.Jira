// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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