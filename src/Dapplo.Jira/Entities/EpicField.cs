// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

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
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        ///     The Label for the Epic
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        ///     Is the Epic editable?
        /// </summary>
        [JsonProperty("editable")]
        public bool Editable { get; set; }

        /// <summary>
        ///     Renderer for the Epic
        /// </summary>
        [JsonProperty("renderer")]
        public string Renderer { get; set; }
        
        /// <summary>
        ///     The Key for the Epic
        /// </summary>
        [JsonProperty("epicKey")]
        public string EpicKey { get; set; }

        /// <summary>
        ///     The Color for the Epic
        /// </summary>
        [JsonProperty("epicColor")]
        public string EpicColor { get; set; }

        /// <summary>
        ///     The Text for the Epic
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        ///     Can the Epic be removed?
        /// </summary>
        [JsonProperty("canRemoveEpic")]
        public bool CanRemoveEpic { get; set; }
    }
}
