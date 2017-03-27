#region Dapplo 2017 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Jira
// 
// Dapplo.Jira is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Jira is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

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
            DateParseHandling = DateParseHandling.None,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
            ContractResolver = new ReadOnlyConsideringContractResolver()
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
