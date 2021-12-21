// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Management.Automation;
using System.Threading.Tasks;

namespace Dapplo.Jira.PowerShell.Support;

/// <summary>
///     This is the base class for all (most?) Jira CmdLets
///     It will create the JiraApi instance, so the deriving class only needs to implement the logic
/// </summary>
public class JiraAsyncCmdlet : AsyncCmdlet
{
    /// <summary>
    ///     The Jira API which should be used to get information
    /// </summary>
    protected IJiraClient JiraApi { get; set; }

    /// <summary>
    ///     Url to the Jira system
    /// </summary>
    [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
    public Uri JiraUri { get; set; }

    /// <summary>
    ///     Password for the user
    /// </summary>
    [Parameter(ValueFromPipelineByPropertyName = true)]
    public string Password { get; set; }

    /// <summary>
    ///     User for the Jira connection
    /// </summary>
    [Parameter(ValueFromPipelineByPropertyName = true)]
    public string Username { get; set; }

    /// <summary>
    ///     Override the BeginProcessingAsync to connect to our jira
    /// </summary>
    protected override Task BeginProcessingAsync()
    {
        JiraApi = JiraClient.Create(JiraUri);
        if (Username != null)
        {
            _ = JiraApi.SetBasicAuthentication(Username, Password);
        }

        return Task.FromResult(true);
    }
}