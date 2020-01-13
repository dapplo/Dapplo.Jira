// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization;

namespace Dapplo.Jira.Query
{
	/// <summary>
	/// All possible fields in a JQL
	/// </summary>
	public enum Fields
	{
#pragma warning disable 1591
		[EnumMember(Value = "affectedVersion")]
		AffectedVersion,
		[EnumMember(Value = "approvals")]
		Approvals,
		[EnumMember(Value = "assignee")]
		Assignee,
		[EnumMember(Value = "attachments")]
		Attachments,
		[EnumMember(Value = "category")]
		Category,
		[EnumMember(Value = "comment")]
		Comment,
		[EnumMember(Value = "component")]
		Component,
		[EnumMember(Value = "created")]
		Created,
		[EnumMember(Value = "creator")]
		Creator,
		[EnumMember(Value = "cf")]
		CustomField,

		/// <summary>
		///     This field is only available when Jira Service Desk is installed
		/// </summary>
		[EnumMember(Value = "\"Customer Request Type\"")]
		CustomerRequestType,
		[EnumMember(Value = "description")]
		Description,
		[EnumMember(Value = "due")]
		Due,
		[EnumMember(Value = "environment")]
		Environment,
		[EnumMember(Value = "\"epic link\"")]
		EpicLink,
		[EnumMember(Value = "filter")]
		Filter,
		[EnumMember(Value = "fixVersion")]
		FixVersion,
		[EnumMember(Value = "issueKey")]
		IssueKey,
		[EnumMember(Value = "labels")]
		Labels,
		[EnumMember(Value = "lastViewed")]
		LastViewed,
		[EnumMember(Value = "level")]
		Level,
		[EnumMember(Value = "originalEstimate")]
		OriginalEstimate,
		[EnumMember(Value = "parent")]
		Parent,
		[EnumMember(Value = "priority")]
		Priority,
		[EnumMember(Value = "project")]
		Project,
		[EnumMember(Value = "remainingEstimate")]
		RemainingEstimate,
		[EnumMember(Value = "reporter")]
		Reporter,
		[EnumMember(Value = "request-channel-type")]
		RequestChannelType,
		[EnumMember(Value = "request-last-activity-time")]
		RequestLastActivityTime,
		[EnumMember(Value = "resolution")]
		Resolution,
		[EnumMember(Value = "resolved")]
		Resolved,
		[EnumMember(Value = "sprint")]
		Sprint,
		[EnumMember(Value = "status")]
		Status,
		[EnumMember(Value = "summary")]
		Summary,
		[EnumMember(Value = "text")]
		Text,
		[EnumMember(Value = "timeSpent")]
		TimeSpent,
		[EnumMember(Value = "type")]
		Type,
		[EnumMember(Value = "updated")]
		Updated,
		[EnumMember(Value = "voter")]
		Voter,
		[EnumMember(Value = "votes")]
		Votes,
		[EnumMember(Value = "watcher")]
		Watcher,
		[EnumMember(Value = "watchers")]
		Watchers,
		[EnumMember(Value = "worklogAuthor")]
		WorkLogAuthor,
		[EnumMember(Value = "worklogComment")]
		WorkLogComment,
		[EnumMember(Value = "worklogDate")]
		WorkLogDate,
		[EnumMember(Value = "workRatio")]
		WorkRatio
#pragma warning restore 1591
	}
}