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

#region using

using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Subscriptions information (looks more or less pagable, although I don't know how to specify the page size etc)
	/// </summary>
	[DataContract]
	public class Subscriptions
	{
		/// <summary>
		///     Where does this "page" end?
		/// </summary>
		[DataMember(Name = "end-index", EmitDefaultValue = false)]
		public long EndIndex { get; set; }

		/// <summary>
		///     The actual list of subscriptions
		/// </summary>
		[DataMember(Name = "items", EmitDefaultValue = false)]
		public IList<Subscription> Items { get; set; }

		/// <summary>
		///     How many results are given back
		/// </summary>
		[DataMember(Name = "max-results", EmitDefaultValue = false)]
		public long MaxResults { get; set; }

		/// <summary>
		///     How many subscritions are there?
		///     This could be more than the amount of items, not only due to rights (maybe the subscriber is not visible) but also
		///     as the value is not expanded.
		/// </summary>
		[DataMember(Name = "size", EmitDefaultValue = false)]
		public long Size { get; set; }

		/// <summary>
		///     Where does this page start?
		/// </summary>
		[DataMember(Name = "start-index", EmitDefaultValue = false)]
		public long StartIndex { get; set; }
	}
}