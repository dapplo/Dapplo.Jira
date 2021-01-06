// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Query;
using Xunit;
using Xunit.Abstractions;

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
            var filterSearch = new FilterSearch
            {
                FilterName = testFilterName
            };
            var filters = await Client.Filter.SearchFiltersAsync(filterSearch);
            var myTestFilter = filters.Items.FirstOrDefault(filter => filter.Name == testFilterName);
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

        [Fact]
        public async Task TestFavoriteFiltersAsync()
        {
            var filters = await Client.Filter.GetFavoritesAsync();
            Assert.NotNull(filters);
            foreach (var filter in filters)
            {
                await Client.Filter.GetAsync(filter.Id);
            }
        }

        [Fact]
        public async Task TestMyFiltersAsync()
        {
            var filters = await Client.Filter.GetMyFiltersAsync();
            Assert.NotNull(filters);
        }
    }
}
