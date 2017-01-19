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

#endregion

namespace Dapplo.Jira.Query
{
	public enum Fields
	{
		[EnumMember(Value = "affectedVersion")] AffectedVersion,
		[EnumMember(Value = "approvals")] Approvals,
		[EnumMember(Value = "assignee")] Assignee,
		[EnumMember(Value = "attachments")] Attachments,
		[EnumMember(Value = "category")] Category,
		[EnumMember(Value = "comment")] Comment,
		[EnumMember(Value = "component")] Component,
		[EnumMember(Value = "created")] Created,
		[EnumMember(Value = "creator")] Creator,
		[EnumMember(Value = "cf")] CustomField,
		// Jira Service Desk only
		[EnumMember(Value = "\"Customer Request Type\"")] CustomerRequestType,
		[EnumMember(Value = "description")] Description,
		[EnumMember(Value = "due")] Due,
		[EnumMember(Value = "environment")] Environment,
		[EnumMember(Value = "\"epic link\"")] EpicLink,
		[EnumMember(Value = "filter")] Filter,
		[EnumMember(Value = "fixVersion")] FixVersion,
		[EnumMember(Value = "issueKey")] IssueKey,
		[EnumMember(Value = "labels")] Labels,
		[EnumMember(Value = "lastViewed")] LastViewed,
		[EnumMember(Value = "level")] Level,
		[EnumMember(Value = "originalEstimate")] OriginalEstimate,
		[EnumMember(Value = "parent")] Parent,
		[EnumMember(Value = "priority")] Priority,
		[EnumMember(Value = "project")] Project,
		[EnumMember(Value = "remainingEstimate")] RemainingEstimate,
		[EnumMember(Value = "reporter")] Reporter,
		[EnumMember(Value = "request-channel-type")] RequestChannelType,
		[EnumMember(Value = "request-last-activity-time")] RequestLastActivityTime,
		[EnumMember(Value = "resolution")] Resolution,
		[EnumMember(Value = "resolved")] Resolved,
		[EnumMember(Value = "sprint")] Sprint,
		[EnumMember(Value = "status")] Status,
		[EnumMember(Value = "summary")] Summary,
		[EnumMember(Value = "text")] Text,
		[EnumMember(Value = "timeSpent")] TimeSpent,
		[EnumMember(Value = "type")] Type,
		[EnumMember(Value = "updated")] Updated,
		[EnumMember(Value = "voter")] Voter,
		[EnumMember(Value = "votes")] Votes,
		[EnumMember(Value = "watcher")] Watcher,
		[EnumMember(Value = "watchers")] Watchers,
		[EnumMember(Value = "worklogAuthor")] WorkLogAuthor,
		[EnumMember(Value = "worklogComment")] WorkLogComment,
		[EnumMember(Value = "worklogDate")] WorkLogDate,
		[EnumMember(Value = "workRatio")] WorkRatio
	}
}