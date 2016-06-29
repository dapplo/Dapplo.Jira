using System.Runtime.Serialization;

namespace Dapplo.Jira.Entities
{

	[DataContract]
	internal class JiraSession
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
}
