#region Dapplo 2017-2018 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2018 Dapplo
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

using System;

namespace Dapplo.Jira.Domains
{
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
}