// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Domains;

/// <summary>
///     This interface describes the functionality of the IJiraClient which domains can use
/// </summary>
public interface IJiraDomain : IJiraClient
{
    /// <summary>
    ///     The rest URI for your JIRA server
    /// </summary>
    Uri JiraRestUri { get; }

    /// <summary>
    ///     The rest URI for V3 of your JIRA server
    /// </summary>
    Uri JiraV3RestUri { get; }

    /// <summary>
    ///     The agile rest URI for your JIRA server
    /// </summary>
    Uri JiraAgileRestUri { get; }

    /// <summary>
    ///     The base URI for JIRA auth api
    /// </summary>
    Uri JiraAuthUri { get; }

    /// <summary>
    ///     The greenhopper rest URI for your JIRA server
    /// </summary>
    Uri JiraGreenhopperRestUri { get; }
}
