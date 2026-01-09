// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dapplo.Jira.Entities;

/// <summary>
/// The root object of an Atlassian Document Format (ADF) document.
/// </summary>
public class AdfDocument
{
    [JsonPropertyName("version")]
    public int Version { get; set; } = 1;

    [JsonPropertyName("type")]
    public string Type { get; set; } = "doc";

    [JsonPropertyName("content")]
    public List<AdfNode> Content { get; set; } = new List<AdfNode>();

    /// <summary>
    /// Create a simple text ADF document
    /// </summary>
    /// <param name="text"></param>
    /// <returns>AdfDocument</returns>
    public static AdfDocument FromText(string text)
    {
        return new AdfDocument()
        {
            Version = 1,
            Type = "doc",
            Content  = new List<AdfNode>
            {
                new AdfNode
                {
                    Type = "paragraph",
                    Content = new List<AdfNode>
                    {
                        new AdfNode
                        {
                            Type = "text",
                            Text = text
                        }
                    }
                }
            }
        };
    }
}

/// <summary>
/// Represents a single node in the document tree (e.g., paragraph, heading, text, media).
/// </summary>
public class AdfNode
{
    /// <summary>
    /// The type of the node (e.g., "paragraph", "text", "heading", "bulletList").
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    /// The child nodes of this node (used for block nodes like paragraphs, lists, tables).
    /// </summary>
    [JsonPropertyName("content")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<AdfNode> Content { get; set; }

    /// <summary>
    /// The text content (only used if Type is "text").
    /// </summary>
    [JsonPropertyName("text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Text { get; set; }

    /// <summary>
    /// Formatting marks applied to the node (e.g., bold, italic, link).
    /// </summary>
    [JsonPropertyName("marks")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<AdfMark> Marks { get; set; }

    /// <summary>
    /// Attributes specific to the node type (e.g., heading level, table layout, media ID).
    /// We use Dictionary<string, object> to handle the flexible schema of attributes.
    /// </summary>
    [JsonPropertyName("attrs")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object> Attrs { get; set; }

    // Helper methods for common attributes
    [JsonIgnore]
    public int? HeadingLevel => GetAttribute<int?>("level");

    private T GetAttribute<T>(string key)
    {
        if (Attrs != null && Attrs.ContainsKey(key))
        {
            if (Attrs[key] is JsonElement element)
            {
                return element.Deserialize<T>();
            }
            return (T)Attrs[key];
        }
        return default;
    }
}

/// <summary>
/// Represents a mark applied to a node (e.g., strong, em, strike, link).
/// </summary>
public class AdfMark
{
    /// <summary>
    /// The type of the mark (e.g., "strong", "em", "link", "textColor").
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    /// Attributes for the mark (e.g., href for links, color for textColor).
    /// </summary>
    [JsonPropertyName("attrs")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object> Attrs { get; set; }
}
