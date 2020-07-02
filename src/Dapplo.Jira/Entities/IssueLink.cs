using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Describes a link between jira issues  
    /// </summary>
    [JsonObject]
    public class IssueLink
    {
        /// <summary>
        /// Used to add link
        /// </summary>
        [JsonProperty("add")]
        public IssueLinkAdd Add { get; set; }

    }

    [JsonObject]
    public class IssueLinkAdd
    {
        /// <summary>
        /// Describes type of link
        /// </summary>
        [JsonProperty("type")]
        public IssuelinkType Type { get; set; }

        /// <summary>
        /// The outward issue to link to
        /// </summary>
        [JsonProperty("outwardIssue")]
        public OutwardIssue OutwardIssue { get; set; }
    }

    [JsonObject]
    public class OutwardIssue
    {
        /// <summary>
        /// Issue key of linked issue
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }
        
    }

    [JsonObject]
    public class IssuelinkType
    {
        /// <summary>
        /// Name of issue link type
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Inward relation link name
        /// </summary>
        [JsonProperty("inward")]
        public string Inward { get; set; }

        /// <summary>
        /// Outward relation link name
        /// </summary>
        [JsonProperty("outward")]
        public string Outward { get; set; }

    }
}