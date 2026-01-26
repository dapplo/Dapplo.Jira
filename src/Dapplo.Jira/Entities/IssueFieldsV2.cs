// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Container for the fields
/// </summary>
[JsonObject]
public class IssueFieldsV2 : BaseIssueFields
{
    /// <summary>
    ///     Description of this issue in ADF
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Parent of this issue
    /// </summary>
    [JsonProperty("parent")]
    public IssueWithFields<IssueFieldsV2> Parent { get; set; }
}
