using System.Runtime.Serialization;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	/// Response to the session login
	/// </summary>
    [DataContract]
    internal class SessionResponse
    {
        [DataMember(Name = "session")]
        public JiraSession Session { get; set; }

        [DataMember(Name = "loginInfo")]
        public LoginInfo LoginInfo { get; set; }
    }
}
