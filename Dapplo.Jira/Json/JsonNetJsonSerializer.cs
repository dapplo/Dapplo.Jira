using System;
using Dapplo.HttpExtensions;
using Newtonsoft.Json;

namespace Dapplo.Jira.Json
{
    /// <summary>
    /// Made to have Dapplo.HttpExtension use Json.NET
    /// </summary>
    public class JsonNetJsonSerializer : IJsonSerializer
    {
        /// <summary>
        /// The JsonSerializerSettings used in the JsonNetJsonSerializer
        /// </summary>
        public JsonSerializerSettings Settings { get; set; } = new JsonSerializerSettings
        {
            DateParseHandling = DateParseHandling.None
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public bool CanSerializeTo(Type sourceType)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public bool CanDeserializeFrom(Type targetType)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public object Deserialize(Type targetType, string jsonString)
        {
            return JsonConvert.DeserializeObject(jsonString, targetType, Settings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string Serialize<T>(T jsonObject)
        {
            return JsonConvert.SerializeObject(jsonObject, Settings);
        }
    }
}
