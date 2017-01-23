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

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Entities;
using Dapplo.Log;

#endregion

namespace Dapplo.Jira.Internal
{
	/// <summary>
	///     This holds all the issue related methods
	/// </summary>
	internal class FilterApi : IFilterApi
	{
		private static readonly LogSource Log = new LogSource();
		private readonly JiraApi _jiraApi;

		internal FilterApi(JiraApi jiraApi)
		{
			_jiraApi = jiraApi;
		}

		/// <inheritdoc />
		public async Task<IList<Filter>> GetFavoritesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			Log.Debug().WriteLine("Retrieving favorite filters");

			_jiraApi.Behaviour.MakeCurrent();
			var filterFavouriteUri = _jiraApi.JiraRestUri.AppendSegments("filter", "favourite");

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetFavoriteFilters?.Length > 0)
			{
				filterFavouriteUri = filterFavouriteUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetFavoriteFilters));
			}

			var response = await filterFavouriteUri.GetAsAsync<HttpResponse<IList<Filter>, Error>>(cancellationToken).ConfigureAwait(false);
			return _jiraApi.HandleErrors(response);
		}

		/// <inheritdoc />
		public async Task<Filter> GetAsync(long id, CancellationToken cancellationToken = default(CancellationToken))
		{
			Log.Debug().WriteLine("Retrieving filter {0}", id);

			_jiraApi.Behaviour.MakeCurrent();
			var filterUri = _jiraApi.JiraRestUri.AppendSegments("filter", id);

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetFilter?.Length > 0)
			{
				filterUri = filterUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetFilter));
			}

			var response = await filterUri.GetAsAsync<HttpResponse<Filter, Error>>(cancellationToken).ConfigureAwait(false);
			return _jiraApi.HandleErrors(response);
		}

		/// <inheritdoc />
		public async Task<Filter> CreateAsync(Filter filter, CancellationToken cancellationToken = default(CancellationToken))
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
			_jiraApi.Behaviour.MakeCurrent();
			var filterUri = _jiraApi.JiraRestUri.AppendSegments("filter");

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetFilter?.Length > 0)
			{
				filterUri = filterUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetFilter));
			}

			var response = await filterUri.PostAsync<HttpResponse<Filter, Error>>(filterCopy, cancellationToken).ConfigureAwait(false);
			return _jiraApi.HandleErrors(response);
		}

		/// <inheritdoc />
		public async Task<Filter> UpdateAsync(Filter filter, CancellationToken cancellationToken = default(CancellationToken))
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

			_jiraApi.Behaviour.MakeCurrent();
			var filterUri = _jiraApi.JiraRestUri.AppendSegments("filter", filter.Id);

			// Add the configurable expand values, if the value is not null or empty
			if (JiraConfig.ExpandGetFilter?.Length > 0)
			{
				filterUri = filterUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetFilter));
			}

			var response = await filterUri.PutAsync<HttpResponse<Filter, Error>>(filterCopy, cancellationToken).ConfigureAwait(false);
			return _jiraApi.HandleErrors(response);
		}

		/// <inheritdoc />
		public async Task DeleteAsync(Filter filter, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (filter == null)
			{
				throw new ArgumentNullException(nameof(filter));
			}
			Log.Debug().WriteLine("Deleting filter {0}", filter.Id);

			_jiraApi.Behaviour.MakeCurrent();
			var filterUri = _jiraApi.JiraRestUri.AppendSegments("filter", filter.Id);

			var response = await filterUri.DeleteAsync<HttpResponse>(cancellationToken).ConfigureAwait(false);
			if (response.StatusCode != HttpStatusCode.NoContent)
			{
				throw new Exception(response.StatusCode.ToString());
			}
		}
	}
}