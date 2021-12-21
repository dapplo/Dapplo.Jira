// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira;

/// <summary>
///     Use this class to configure some of the behaviour
///     The values that start with Expand are used to set the expand query values, which make the Jira REST API return
///     "more".
/// </summary>
public static class JiraConfig
{
    /// <summary>
    ///     The values that are expanded in the GetFavoriteFilters result
    /// </summary>
    public static string[] ExpandGetFavoriteFilters { get; set; }

    /// <summary>
    ///     The values that are expanded in the GetFilter result
    /// </summary>
    public static string[] ExpandGetFilter { get; set; }

    /// <summary>
    ///     The values that are expanded in the GetIssue result
    ///     Examples are: renderedFields, version
    /// </summary>
    public static string[] ExpandGetIssue { get; set; } =
    {
        "version", "container"
    };

    /// <summary>
    ///     The values that are expanded in the GetProject result
    /// </summary>
    public static string[] ExpandGetProject { get; set; }

    /// <summary>
    ///     The values that are expanded in the GetProjects result
    /// </summary>
    public static string[] ExpandGetProjects { get; set; } =
    {
        "description", "lead"
    };

    /// <summary>
    ///     The values that are expanded in the GetTransitions result
    ///     Examples are: transitions.fields
    /// </summary>
    public static string[] ExpandGetTransitions { get; set; }

    /// <summary>
    ///     The values that are expanded in the Search result
    /// </summary>
    public static string[] ExpandSearch { get; set; }

    /// <summary>
    ///     The fields that are requested by the Search result
    /// </summary>
    public static string[] SearchFields { get; set; } =
    {
        "summary", "status", "assignee", "key", "project"
    };

    /// <summary>
    ///     The custom issue field name to use for sprint information
    /// </summary>
    public static string SpintCustomField { get; set; } = "customfield_10007";
}