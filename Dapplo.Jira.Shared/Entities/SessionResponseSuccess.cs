using System.Runtime.Serialization;

namespace Dapplo.Jira.Entities
{
    [DataContract]
    internal class SessionResponseSuccess
    {
        [DataMember(Name = "session")]
        public JiraSeesion Session { get; set; }

        [DataMember(Name = "loginInfo")]
        public JiraLoginInfo LoginInfo { get; set; }
    }

    [DataContract]
    internal class JiraSeesion
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
        public override string ToString()
        {
            return $"{Name ?? string.Empty}={Value ?? string.Empty}";
        }
    }

    /// <summary>
    /// Jira login info
    /// </summary>
    [DataContract]
    public class JiraLoginInfo
    {
        /// <summary>
        /// Failed login count
        /// </summary>
        [DataMember(Name = "failedLoginCount")]
        public int FailedLoginCount { get; set; }

        /// <summary>
        /// Login count
        /// </summary>
        [DataMember(Name = "loginCount")]
        public int LoginCount { get; set; }

        /// <summary>
        /// Last failed login time
        /// </summary>
        [DataMember(Name = "lastFailedLoginTime")]
        public string LastFailedLoginTime { get; set; }

        /// <summary>
        /// Previous login time
        /// </summary>
        [DataMember(Name = "previousLoginTime")]
        public string PreviousLoginTime { get; set; }
    }
}
