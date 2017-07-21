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

using System.Linq;
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
		public async Task TestCreateAsync()
		{
			const string testFilterName = "MyTestFilter";
			var filters = await Client.Filter.GetFiltersAsync();
			var myTestFilter = filters.FirstOrDefault(filter => filter.Name == testFilterName);
			if (myTestFilter != null)
			{
				await Client.Filter.DeleteAsync(myTestFilter);
			}
			var query = Where.IssueKey.In(TestIssueKey);
			var createdFilter = await Client.Filter.CreateAsync(new Filter(testFilterName, query));
			Assert.NotNull(createdFilter);
			Assert.Equal(query.ToString(), createdFilter.Jql);
			query = Where.IssueKey.In(TestIssueKey).OrderByAscending(Fields.IssueKey);
			createdFilter.Jql = query.ToString();
			var updatedFilter = await Client.Filter.UpdateAsync(createdFilter);
			Assert.NotNull(updatedFilter);
			Assert.Equal(query.ToString(), updatedFilter.Jql);

			await Client.Filter.DeleteAsync(createdFilter);
		}

		[Fact]
		public async Task TestGetFavoritesAsync()
		{
			var filters = await Client.Filter.GetFavoritesAsync();
			Assert.NotNull(filters);
			foreach (var filter in filters)
			{
				await Client.Filter.GetAsync(filter.Id);
			}
		}
	}
}