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

using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Dapplo.HttpExtensions;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Agile Issue information
	///     See: https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/issue-getIssue
	/// </summary>
	[DataContract]
	public class AgileIssue : BaseProperties<string>
	{
		private const string SprintCustomKey = "customfield_10007";
		/// <summary>
		///     Fields for the Agile issue
		/// </summary>
		[DataMember(Name = "fields", EmitDefaultValue = false)]
		public AgileIssueFields Fields { get; set; }

		/// <summary>
		/// Retrieve the sprint information, this is a hack for getting the information.
		/// More details are <a href="https://jira.atlassian.com/browse/JSW-9928">here</a>
		/// I created a ticket about this <a href="https://jira.atlassian.com/browse/JSW-15530">here</a>
		/// </summary>
		public Sprint Sprint
		{
			get
			{
				if (!Fields.CustomFields.ContainsKey(SprintCustomKey))
				{
					return null;
				}
				string serializedSprintInfomation = (string)((JsonArray)Fields.CustomFields[SprintCustomKey])[0];

				var matchId = Regex.Match(serializedSprintInfomation, "id=([^,]+),");
				var matchName = Regex.Match(serializedSprintInfomation, "name=([^,]+),");
				var matchState = Regex.Match(serializedSprintInfomation, "state=([^,]+),");
				return new Sprint
				{
					Name = matchName.Groups[1].Value,
					Id = int.Parse(matchId.Groups[1].Value),
					State = matchState.Groups[1].Value
				};
			}
		}
	}
}