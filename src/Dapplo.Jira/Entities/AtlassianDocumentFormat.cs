// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
/// The root object of an Atlassian Document Format (ADF) document.
/// </summary>
[JsonObject]
public class AdfDocument
{
    /// <summary>
    /// The version of the ADF format
    /// </summary>
    [JsonProperty("version")]
    public int Version { get; set; } = 1;

    /// <summary>
    /// Type of ADF content
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; } = "doc";

    /// <summary>
    /// The content of the ADF document, represented as a list of nodes.
    /// </summary>
    [JsonProperty("content")]
    public List<AdfNode> Content { get; set; } = new List<AdfNode>();

    /// <summary>
    /// Exclict cast from string to AdfDocument
    /// </summary>
    /// <param name="text">string with Text</param>
    public static explicit operator AdfDocument(string text) => FromText(text);

    /// <summary>
    /// Exclict cast from AdfDocument to string
    /// </summary>
    /// <param name="adfDocument">AdfDocument which needs to be converted</param>
    public static explicit operator string(AdfDocument adfDocument) => adfDocument?.ToString();

    /// <summary>
    /// Create a simple text ADF document
    /// </summary>
    /// <param name="text"></param>
    /// <returns>AdfDocument</returns>
    public static AdfDocument FromText(string text)
    {
        return new AdfDocument()
        {
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

    /// <summary>
    /// Returns a string that represents the concatenated text content of all nodes in the document, separated by line breaks.
    /// </summary>
    /// <remarks>This method is useful for obtaining a plain text representation of the document's content,
    /// with each node's text output on a separate line. The result does not include any formatting or markup.</remarks>
    /// <returns>A string containing the combined text extracted from each node in the content collection, with each segment
    /// separated by a newline character. Returns an empty string if the content is null or contains no nodes.</returns>
    override public string ToString()
    {
        if (Content == null || Content.Count == 0)
        {
            return string.Empty;
        }
        var texts = new List<string>();
        foreach (var node in Content)
        {
            texts.AddRange(node.ExtractText());
        }
        return string.Join(Environment.NewLine, texts);
    }
}
