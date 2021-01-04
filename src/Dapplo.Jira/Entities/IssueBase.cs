// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Issue information
    ///     See: https://docs.atlassian.com/jira/REST/latest/#api/2/issue
    /// </summary>
    [JsonObject]
    public class IssueBase : BaseProperties<string>
    {
        /// <summary>
        ///     Key of the issue
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        ///     Gets or sets the change logs.
        /// </summary>
        /// <value>
        ///     The change logs.
        /// </value>
        [JsonProperty("changelog")]
        public Changelog Changelog { get; set; }

        /// <summary>
        /// Specify the IJiraClient used to perform certain actions with
        /// </summary>
        /// <param name="jiraClient"></param>
        /// <returns>IssueBase (this)</returns>
        public IssueBase WithClient(IJiraClient jiraClient)
        {
            AssociatedJiraClient = jiraClient;
            return this;
        }

        /// <summary>
        /// The JiraClient which is associated with this issue.
        /// In general this is the JiraClient which was used to retrieve the issue
        /// </summary>
        [JsonIgnore]
        public IJiraClient AssociatedJiraClient { get; private set; }
    }
}
