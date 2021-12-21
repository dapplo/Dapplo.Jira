// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma warning disable 1591

using System.Runtime.Serialization;

namespace Dapplo.Jira.Query;

/// <summary>
///     the operators which are used inside a clause
/// </summary>
public enum Operators
{
    [EnumMember(Value = "=")] EqualTo,
    [EnumMember(Value = "!=")] NotEqualTo,
    [EnumMember(Value = ">")] GreaterThan,
    [EnumMember(Value = ">=")] GreaterThanEqualTo,
    [EnumMember(Value = "<=")] LessThanEqualTo,
    [EnumMember(Value = "<")] LessThan,
    [EnumMember(Value = "in")] In,
    [EnumMember(Value = "not in")] NotIn,
    [EnumMember(Value = "was")] Was,
    [EnumMember(Value = "~")] Contains,
    [EnumMember(Value = "!~")] DoesNotContain
}