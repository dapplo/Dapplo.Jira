// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Domains;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Internal;
using Dapplo.Log;

namespace Dapplo.Jira
{
    /// <summary>
    ///     This holds all the filter related extension methods
    /// </summary>
    public static class FilterDomainExtensions
    {
        private static readonly LogSource Log = new LogSource();

        /// <summary>
        ///     Get filter favorites
        ///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1388
        /// </summary>
        /// <param name="jiraClient">IFilterDomain to bind the extension method to</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>List of filter</returns>
        public static async Task<IList<Filter>> GetFavoritesAsync(this IFilterDomain jiraClient, CancellationToken cancellationToken = default)
        {
            Log.Debug().WriteLine("Retrieving favorite filters");

            jiraClient.Behaviour.MakeCurrent();
            var filterFavouriteUri = jiraClient.JiraRestUri.AppendSegments("filter", "favourite");

            // Add the configurable expand values, if the value is not null or empty
            if (JiraConfig.ExpandGetFavoriteFilters?.Length > 0)
            {
                filterFavouriteUri = filterFavouriteUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetFavoriteFilters));
            }

            var response = await filterFavouriteUri.GetAsAsync<HttpResponse<IList<Filter>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get my filters
        /// </summary>
        /// <param name="jiraClient">IFilterDomain to bind the extension method to</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>List of filter</returns>
        public static async Task<IList<Filter>> GetMyFiltersAsync(this IFilterDomain jiraClient, CancellationToken cancellationToken = default)
        {
            var myself = await jiraClient.User.GetMyselfAsync(cancellationToken).ConfigureAwait(false);
            Log.Debug().WriteLine("Retrieving filters for current user {0}", myself.AccountId);
            var filterSearch = new FilterSearch
            {
                AccountId = myself.AccountId
            };
            var filters = await jiraClient.Filter.SearchFiltersAsync(filterSearch, null, cancellationToken).ConfigureAwait(false);
            return filters.Items;
        }

        /// <summary>
        ///     Search filters
        ///     See <a href="https://developer.atlassian.com/cloud/jira/platform/rest/v2/api-group-filters/#api-rest-api-2-filter-search-get">filter search</a>
        /// </summary>
        /// <param name="jiraClient">IFilterDomain to bind the extension method to</param>
        /// <param name="filterSearch">FilterSearch</param>
        /// <param name="page">Page for paging results</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Filters</returns>
        public static async Task<Filters> SearchFiltersAsync(this IFilterDomain jiraClient, FilterSearch filterSearch = null, Page page = null, CancellationToken cancellationToken = default)
        {
            Log.Debug().WriteLine("Searching for filters");

            jiraClient.Behaviour.MakeCurrent();
            var filtersUri = jiraClient.JiraRestUri.AppendSegments("filter", "search");

            if (page != null)
            {
                filtersUri = filtersUri.ExtendQuery(new Dictionary<string, object>
                {
                    {
                        "startAt", page.StartAt
                    },
                    {
                        "maxResults", page.MaxResults
                    }
                });
            }

            if (filterSearch != null)
            {
                if (!string.IsNullOrEmpty(filterSearch.FilterName))
                {
                    filtersUri = filtersUri.ExtendQuery("filterName", filterSearch.FilterName);
                }
                if (!string.IsNullOrEmpty(filterSearch.AccountId))
                {
                    filtersUri = filtersUri.ExtendQuery("accountId", filterSearch.AccountId);
                }
                if (!string.IsNullOrEmpty(filterSearch.GroupName))
                {
                    filtersUri = filtersUri.ExtendQuery("groupname", filterSearch.GroupName);
                }
                if (filterSearch.ProjectId.HasValue)
                {
                    filtersUri = filtersUri.ExtendQuery("projectid", filterSearch.ProjectId);
                }
                if (!string.IsNullOrEmpty(filterSearch.OrderBy))
                {
                    filtersUri = filtersUri.ExtendQuery("orderBy", filterSearch.OrderBy);
                }
                if (filterSearch.Ids != null)
                {
                    foreach (var filterId in filterSearch.Ids)
                    {
                        filtersUri = filtersUri.ExtendQuery("id", filterId);
                    }
                }
            }

            var expand = filterSearch?.Expand ?? JiraConfig.ExpandSearchFilters;
            // Add the configurable expand values, if the value is not null or empty
            if (expand?.Length > 0)
            {
                filtersUri = filtersUri.ExtendQuery("expand", string.Join(",", expand));
            }

            var response = await filtersUri.GetAsAsync<HttpResponse<Filters, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get filter
        ///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1388
        /// </summary>
        /// <param name="jiraClient">IFilterDomain to bind the extension method to</param>
        /// <param name="id">filter id</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Filter</returns>
        public static async Task<Filter> GetAsync(this IFilterDomain jiraClient, long id, CancellationToken cancellationToken = default)
        {
            Log.Debug().WriteLine("Retrieving filter {0}", id);

            jiraClient.Behaviour.MakeCurrent();
            var filterUri = jiraClient.JiraRestUri.AppendSegments("filter", id);

            // Add the configurable expand values, if the value is not null or empty
            if (JiraConfig.ExpandGetFilter?.Length > 0)
            {
                filterUri = filterUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetFilter));
            }

            var response = await filterUri.GetAsAsync<HttpResponse<Filter, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Create a filter
        /// </summary>
        /// <param name="jiraClient">IFilterDomain to bind the extension method to</param>
        /// <param name="filter">Filter to create</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Filter</returns>
        public static async Task<Filter> CreateAsync(this IFilterDomain jiraClient, Filter filter, CancellationToken cancellationToken = default)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var filterCopy = new Filter
            {
                Name = filter.Name,
                Jql = filter.Jql,
                Description = filter.Description,
                IsFavorite = filter.IsFavorite
            };
            jiraClient.Behaviour.MakeCurrent();
            var filterUri = jiraClient.JiraRestUri.AppendSegments("filter");

            // Add the configurable expand values, if the value is not null or empty
            if (JiraConfig.ExpandGetFilter?.Length > 0)
            {
                filterUri = filterUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetFilter));
            }

            var response = await filterUri.PostAsync<HttpResponse<Filter, Error>>(filterCopy, cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Update a filter
        /// </summary>
        /// <param name="jiraClient">IFilterDomain to bind the extension method to</param>
        /// <param name="filter">Filter to update</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Filter</returns>
        public static async Task<Filter> UpdateAsync(this IFilterDomain jiraClient, Filter filter, CancellationToken cancellationToken = default)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var filterCopy = new Filter
            {
                Name = filter.Name,
                Jql = filter.Jql,
                Description = filter.Description,
                IsFavorite = filter.IsFavorite
            };

            jiraClient.Behaviour.MakeCurrent();
            var filterUri = jiraClient.JiraRestUri.AppendSegments("filter", filter.Id);

            // Add the configurable expand values, if the value is not null or empty
            if (JiraConfig.ExpandGetFilter?.Length > 0)
            {
                filterUri = filterUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetFilter));
            }

            var response = await filterUri.PutAsync<HttpResponse<Filter, Error>>(filterCopy, cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Delete filter
        ///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1388
        /// </summary>
        /// <param name="jiraClient">IFilterDomain to bind the extension method to</param>
        /// <param name="filter">Filter to delete</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task DeleteAsync(this IFilterDomain jiraClient, Filter filter, CancellationToken cancellationToken = default)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            Log.Debug().WriteLine("Deleting filter {0}", filter.Id);

            jiraClient.Behaviour.MakeCurrent();
            var filterUri = jiraClient.JiraRestUri.AppendSegments("filter", filter.Id);

            var response = await filterUri.DeleteAsync<HttpResponse>(cancellationToken).ConfigureAwait(false);
            response.HandleStatusCode(HttpStatusCode.NoContent);
        }
    }
}
