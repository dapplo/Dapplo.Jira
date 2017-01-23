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

using System.Threading.Tasks;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Query;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Jira.Tests
{
	public class FilterTests : TestBase
	{
		public FilterTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
		{
		}

		[Fact]
		public async Task TestGetFavoritesAsync()
		{
			var filters = await _jiraApi.Filter.GetFavoritesAsync();
			Assert.NotNull(filters);
			foreach (var filter in filters)
			{
				await _jiraApi.Filter.GetAsync(filter.Id);
			}
		}

		[Fact]
		public async Task TestCreateAsync()
		{
			var query = Where.IssueKey.In("BUG-2104");
			var filter = await _jiraApi.Filter.CreateAsync(new Filter("MyTestFilter",query));
			Assert.NotNull(filter);
			Assert.Equal(query.ToString(), filter.Jql);
			query = Where.IssueKey.In("BUG-2104", "BUG-2105");
			filter.Jql = query.ToString();
			filter = await _jiraApi.Filter.UpdateAsync(filter);
			Assert.NotNull(filter);
			Assert.Equal(query.ToString(), filter.Jql);

			await _jiraApi.Filter.DeleteAsync(filter);
		}
	}
}