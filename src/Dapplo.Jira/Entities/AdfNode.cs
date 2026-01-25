// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
/// Represents a single node in the document tree (e.g., paragraph, heading, text, media).
/// </summary>
[JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore)]
public class AdfNode
{
    /// <summary>
    /// The type of the node (e.g., "paragraph", "text", "heading", "bulletList").
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// The child nodes of this node (used for block nodes like paragraphs, lists, tables).
    /// </summary>
    [JsonProperty("content")]
    public List<AdfNode> Content { get; set; }

    /// <summary>
    /// The text content (only used if Type is "text").
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; }

    /// <summary>
    /// Formatting marks applied to the node (e.g., bold, italic, link).
    /// </summary>
    [JsonProperty("marks")]
    public List<AdfMark> Marks { get; set; }

    /// <summary>
    /// Attributes specific to the node type (e.g., heading level, table layout, media ID).
    /// We use Dictionary string, object to handle the flexible schema of attributes.
    /// </summary>
    [JsonProperty("attrs")]
    public Dictionary<string, object> Attrs { get; set; }

    /// <summary>
    /// Gets the heading level of the element, represented as an optional integer value.
    /// </summary>
    /// <remarks>The heading level indicates the structural hierarchy of the element in the document. A value
    /// of null indicates that the heading level is not set.</remarks>
    [JsonIgnore]
    public int? HeadingLevel => GetAttribute<int?>("level");

    /// <summary>
    /// Retrieves the text content from each node in the Content collection.
    /// </summary>
    /// <remarks>Intended for internal use to facilitate extraction of textual data from the Content nodes.
    /// Returns only the text values and does not include any formatting or metadata from the nodes.</remarks>
    /// <returns>An enumerable collection of strings containing the text from each node. Returns an empty collection if the
    /// Content collection is null or contains no elements.</returns>
    internal IEnumerable<string> ExtractText()
    {
        // Check if Content is null or empty
        if (Content == null || Content.Count == 0)
        {
            return Enumerable.Empty<string>();
        }

        // Extract text from each node in Content
        return Content.Select(node => node.Text);
    }

    /// <summary>
    /// Retrieves the value associated with the specified key from the attribute collection and converts it to the
    /// specified type.
    /// </summary>
    /// <remarks>If the key is not present in the attribute collection or if the value cannot be converted to
    /// the specified type, the method returns the default value for type T.</remarks>
    /// <typeparam name="T">The type to which the attribute value is converted.</typeparam>
    /// <param name="key">The key of the attribute to retrieve. This key must exist in the attribute collection.</param>
    /// <returns>The value associated with the specified key, converted to type T, or the default value of T if the key does not
    /// exist or the conversion fails.</returns>
    private T GetAttribute<T>(string key)
    {
        if (Attrs != null && Attrs.ContainsKey(key))
        {
            try
            {
                return (T)Convert.ChangeType(Attrs[key], typeof(T));
            }
            catch
            {
                return default;
            }
        }
        return default;
    }
}
