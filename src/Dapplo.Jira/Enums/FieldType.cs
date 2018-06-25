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

namespace Dapplo.Jira.Enumss
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