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

#region Usings

using Newtonsoft.Json;

#endregion

namespace Dapplo.Jira.Entities
{
    /// <summary>
    ///     Epic Field information
    /// </summary>
    [JsonObject]
    public class EpicField
    {
        /// <summary>
        ///     The Id for the Epic
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        ///     The Label for the Epic
        /// </summary>
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        /// <summary>
        ///     Is the Epic editable?
        /// </summary>
        [JsonProperty(PropertyName = "editable")]
        public bool Editable { get; set; }

        /// <summary>
        ///     Renderer for the Epic
        /// </summary>
        [JsonProperty(PropertyName = "renderer")]
        public string Renderer { get; set; }
        
        /// <summary>
        ///     The Key for the Epic
        /// </summary>
        [JsonProperty(PropertyName = "epicKey")]
        public string EpicKey { get; set; }

        /// <summary>
        ///     The Color for the Epic
        /// </summary>
        [JsonProperty(PropertyName = "epicColor")]
        public string EpicColor { get; set; }

        /// <summary>
        ///     The Text for the Epic
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        ///     Can the Epic be removed?
        /// </summary>
        [JsonProperty(PropertyName = "canRemoveEpic")]
        public bool CanRemoveEpic { get; set; }
    }
}
