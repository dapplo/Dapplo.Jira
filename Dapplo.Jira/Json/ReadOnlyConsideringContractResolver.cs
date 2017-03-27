using System.ComponentModel;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Dapplo.Jira.Json
{
    /// <summary>
    /// This takes care that the members are only serialized when there is no ReadOnlyAttribute
    /// </summary>
    public class ReadOnlyConsideringContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// This takes care that the members are only serialized when there is no ReadOnlyAttribute
        /// </summary>
        /// <param name="member">MemberInfo</param>
        /// <param name="memberSerialization">MemberSerialization</param>
        /// <returns>JsonProperty</returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
	        property.ShouldSerialize = o => member.GetCustomAttribute<ReadOnlyAttribute>() == null;
            return property;
        }
    }
}
