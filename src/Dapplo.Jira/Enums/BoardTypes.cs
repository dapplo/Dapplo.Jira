// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;

namespace Dapplo.Jira.Enums
{
    /// <summary>
    /// Types of an agile board
    /// </summary>
    public enum BoardTypes
    {
        /// <summary>
        /// Identifies the board to be of type Kanban
        /// </summary>
        [Description("Kanban")] Kanban,

        /// <summary>
        /// Identifies the board to be of type Scrum
        /// </summary>
        [Description("Scrum")] Scrum,

        /// <summary>
        /// Identifies the board to be of type Simple
        /// </summary>
        [Description("Simple")] Simple
    }
}
