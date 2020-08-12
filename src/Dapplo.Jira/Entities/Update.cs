using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Container for certain update fields
    /// </summary>
    [JsonObject]
    public class Update
    {
        /// <summary>
        ///     Container for issue links
        /// </summary>
        [JsonProperty("issuelinks")]
        public List<IssueLink> IssueLinks { get; set; }
    }
}