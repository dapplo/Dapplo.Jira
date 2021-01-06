// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Filter search information see <a href="https://developer.atlassian.com/cloud/jira/platform/rest/v2/api-group-filters/#api-rest-api-2-filter-search-get">here</a>
    /// </summary>
    public class FilterSearch
    {
        /// <summary>
        /// String used to perform a case-insensitive partial match with name.
        /// </summary>
        public string FilterName { get; set; }

        /// <summary>
        /// User account ID used to return filters with the matching owner.accountId. This parameter cannot be used with owner.
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Group name used to returns filters that are shared with a group that matches sharePermissions.group.groupname.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Project ID used to returns filters that are shared with a project that matches sharePermissions.project.id.
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// Order the results by a field:
        /// 
        /// description Sorts by filter description. Note that this sorting works independently of whether the expand to display the description field is in use.
        /// favourite_count Sorts by the count of how many users have this filter as a favorite.
        /// is_favourite Sorts by whether the filter is marked as a favorite.
        /// id Sorts by filter ID.
        /// name Sorts by filter name.
        /// owner Sorts by the ID of the filter owner.
        /// Default: name
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// The list of filter IDs.
        /// </summary>
        public int[] Ids { get; set; }

        /// <summary>
        /// Use expand to include additional information about filter in the response. This parameter accepts a comma-separated list. Expand options include:
        /// 
        /// description Returns the description of the filter.
        /// favourite Returns an indicator of whether the user has set the filter as a favorite.
        /// favouritedCount Returns a count of how many users have set this filter as a favorite.
        /// jql Returns the JQL query that the filter uses.
        /// owner Returns the owner of the filter.
        /// searchUrl Returns a URL to perform the filter's JQL query.
        /// sharePermissions Returns the share permissions defined for the filter.
        /// subscriptions Returns the users that are subscribed to the filter.
        /// viewUrl Returns a URL to view the filter.
        /// </summary>
        public string[] Expand { get; set; }
    }
}
