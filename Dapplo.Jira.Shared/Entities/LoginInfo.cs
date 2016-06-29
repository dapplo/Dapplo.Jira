using System.Runtime.Serialization;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	/// Jira login info
	/// </summary>
	[DataContract]
	public class LoginInfo
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
