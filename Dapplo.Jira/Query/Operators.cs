//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Jira
// 
//  Dapplo.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#pragma warning disable 1591

#region using

using System.Runtime.Serialization;

#endregion

namespace Dapplo.Jira.Query
{
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
		[EnumMember(Value = "~")] Contains,
		[EnumMember(Value = "!~")] DoesNotContain
	}
}