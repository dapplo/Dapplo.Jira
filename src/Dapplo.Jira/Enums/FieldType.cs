// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dapplo.Jira.Enums
{
    /// <summary>
    ///     Represent the kind of field.
    /// </summary>
    public enum FieldType
    {
        /// <summary>
        ///     To be able to know if the field type has not been detected
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     For the JIRA base fields types
        /// </summary>
        Jira = 1,

        /// <summary>
        ///     For the Custom fields types
        /// </summary>
        Custom = 2
    }
}
