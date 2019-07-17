#region Dapplo 2017-2019 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2019 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Jira
// 
// Dapplo.Jira is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Jira is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings

using System;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Domains;

#endregion

namespace Dapplo.Jira
{
    /// <summary>
    ///     This is the interface which describes the Atlassian Jira client
    /// </summary>
    public interface IJiraClient
    {
        /// <summary>
        ///     Store the specific HttpBehaviour, which contains a IHttpSettings and also some additional logic for making a
        ///     HttpClient which works with Jira
        /// </summary>
        IHttpBehaviour Behaviour { get; }

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
        ///     Work domain
        /// </summary>
        IWorkDomain Work { get; }

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
        void SetBasicAuthentication(string user, string password);
    }
}