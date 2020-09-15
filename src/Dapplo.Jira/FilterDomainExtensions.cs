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
        ///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1388
        /// </summary>
        /// <param name="jiraClient">IFilterDomain to bind the extension method to</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>List of filter</returns>
        public static async Task<IList<Filter>> GetFiltersAsync(this IFilterDomain jiraClient, CancellationToken cancellationToken = default)
        {
            Log.Debug().WriteLine("Retrieving filters for current user");

            jiraClient.Behaviour.MakeCurrent();
            var filtersUri = jiraClient.JiraRestUri.AppendSegments("filter", "my");

            // Add the configurable expand values, if the value is not null or empty
            if (JiraConfig.ExpandGetFavoriteFilters?.Length > 0)
            {
                filtersUri = filtersUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetFavoriteFilters));
            }

            var response = await filtersUri.GetAsAsync<HttpResponse<IList<Filter>, Error>>(cancellationToken).ConfigureAwait(false);
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