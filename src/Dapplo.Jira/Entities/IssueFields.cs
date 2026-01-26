// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Container for the fields for the V3 API
/// </summary>
[JsonObject]
public class IssueFields : BaseIssueFields
{

    /// <summary>
    ///     Description of this issue in ADF
    /// </summary>
    [JsonProperty("description")]
    public AdfDocument Description { get; set; }

    /// <summary>
    ///     Parent of this issue
    /// </summary>
    [JsonProperty("parent")]
    public IssueWithFields<IssueFields> Parent { get; set; }
}
