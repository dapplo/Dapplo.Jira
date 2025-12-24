// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira;

/// <summary>
///     This is the interface which describes the Atlassian Jira client
/// </summary>
public interface IJiraClient
{
    /// <summary>
    ///     The base URI for your JIRA server
    /// </summary>
    Uri JiraBaseUri { get; }

    /// <summary>
    ///     Issue domain
    /// </summary>
    IIssueDomain Issue { get; }

    /// <summary>
    ///     Attachment domain
    /// </summary>
    IAttachmentDomain Attachment { get; }

    /// <summary>
    ///     Project domain
    /// </summary>
    IProjectDomain Project { get; }

    /// <summary>
    ///     User domain
    /// </summary>
    IUserDomain User { get; }

    /// <summary>
    ///     Session domain
    /// </summary>
    ISessionDomain Session { get; }

    /// <summary>
    ///     Filter domain
    /// </summary>
    IFilterDomain Filter { get; }

    /// <summary>
    ///     WorkLog domain
    /// </summary>
    IWorkLogDomain WorkLog { get; }

    /// <summary>
    ///     Server domain
    /// </summary>
    IServerDomain Server { get; }

    /// <summary>
    ///     Agile domain
    /// </summary>
    IAgileDomain Agile { get; }

    /// <summary>
    ///     Grasshopper domain
    /// </summary>
    IGreenhopperDomain Greenhopper { get; }

    /// <summary>
    ///     Set Basic Authentication for the current client
    /// </summary>
    /// <param name="user">username</param>
    /// <param name="password">password</param>
    IJiraClient SetBasicAuthentication(string user, string password);

    /// <summary>
    ///     Set Bearer Authentication for the current client
    /// </summary>
    /// <param name="bearer">bearer</param>
    IJiraClient SetBearerAuthentication(string bearer);
}
