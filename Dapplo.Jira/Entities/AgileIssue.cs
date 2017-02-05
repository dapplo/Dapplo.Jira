#region Dapplo 2017 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017 Dapplo
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
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Dapplo.HttpExtensions.Json;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Agile Issue information
	///     See: https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/issue-getIssue
	/// </summary>
	[DataContract]
	public class AgileIssue : IssueWithFields<AgileIssueFields>
	{
		/// <summary>
		///     Retrieve the sprint information, this is a hack for getting the information.
		///     More details are <a href="https://jira.atlassian.com/browse/JSW-9928">here</a>
		///     I created a ticket about this <a href="https://jira.atlassian.com/browse/JSW-15530">here</a>
		/// </summary>
		public Sprint Sprint
		{
			get
			{
				if (!Fields.CustomFields.ContainsKey(JiraConfig.SpintCustomField))
				{
					return null;
				}
				var serializedSprintInfomation = (string) ((JsonArray) Fields.CustomFields[JiraConfig.SpintCustomField])[0];

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

		/// <summary>
		///     Retrieve the estimation (story points) from the issue, this only works by using the BoardConfiguration
		/// </summary>
		/// <param name="boardConfiguration">BoardConfiguration</param>
		/// <returns>long with estimation or 0 if nothing</returns>
		public long GetEstimation(BoardConfiguration boardConfiguration)
		{
			if (boardConfiguration == null)
			{
				throw new ArgumentNullException(nameof(boardConfiguration));
			}
			var estimationCustomField = boardConfiguration.Estimation.Field.FieldId;
			if (!Fields.CustomFields.ContainsKey(estimationCustomField))
			{
				return 0;
			}
			return (long) Fields.CustomFields[estimationCustomField];
		}

		/// <summary>
		///     Retrieve the rank from the issue, this only works by using the BoardConfiguration
		/// </summary>
		/// <param name="boardConfiguration">BoardConfiguration</param>
		/// <returns>Something which represents a rank</returns>
		public string GetRank(BoardConfiguration boardConfiguration)
		{
			if (boardConfiguration == null)
			{
				throw new ArgumentNullException(nameof(boardConfiguration));
			}
			var rankCustomField = $"customfield_{boardConfiguration.Ranking.RankCustomFieldId}";
			if (!Fields.CustomFields.ContainsKey(rankCustomField))
			{
				return null;
			}
			return (string) Fields.CustomFields[rankCustomField];
		}
	}
}