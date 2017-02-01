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

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Dapplo.HttpExtensions;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Container for the fields
	/// </summary>
	[DataContract]
	public class IssueFields
	{
		/// <summary>
		///     The summary of the time spend on this issue
		/// </summary>
		[DataMember(Name = "aggregatetimespent", EmitDefaultValue = false)]
		public long AggregateTimeSpent { get; set; }

		/// <summary>
		///     User who this issue is assigned to
		/// </summary>
		[DataMember(Name = "assignee", EmitDefaultValue = false)]
		public User Assignee { get; set; }

		/// <summary>
		///     Attachments for this issue
		/// </summary>
		[DataMember(Name = "attachment", EmitDefaultValue = false)]
		public IList<Attachment> Attachments { get; set; }

		/// <summary>
		///     Container for the comments for this issue
		/// </summary>
		[DataMember(Name = "comment", EmitDefaultValue = false)]
		public Comments Comments { get; set; }

		/// <summary>
		///     When was this issue created
		/// </summary>
		[DataMember(Name = "created", EmitDefaultValue = false)]
		public DateTimeOffset Created { get; set; }

		/// <summary>
		///     User who created this issue
		/// </summary>
		[DataMember(Name = "creator", EmitDefaultValue = false)]
		public User Creator { get; set; }

		/// <summary>
		///     All custom field values.
		///     A custom field must match the reg-ex pattern "customfield_.*", otherwise it's ignored.
		/// </summary>
		[ExtensionData(Pattern = "customfield_.*")]
		public IDictionary<string, object> CustomFields { get; } = new Dictionary<string, object>();

		/// <summary>
		///     Description of this issue
		/// </summary>
		[DataMember(Name = "description", EmitDefaultValue = false)]
		public string Description { get; set; }

		/// <summary>
		///     Versions where this issue is fixed
		/// </summary>
		[DataMember(Name = "fixVersions", EmitDefaultValue = false)]
		public IList<Version> FixVersions { get; set; }

		/// <summary>
		///     Type of the issue
		/// </summary>
		[DataMember(Name = "issuetype", EmitDefaultValue = false)]
		public IssueType IssueType { get; set; }

		/// <summary>
		///     Labels for this issue
		/// </summary>
		[DataMember(Name = "labels", EmitDefaultValue = false)]
		public IList<string> Labels { get; set; }

		/// <summary>
		///     When was this issue viewed (by whom??)
		/// </summary>
		[DataMember(Name = "lastViewed", EmitDefaultValue = false)]
		public DateTimeOffset LastViewed { get; set; }

		/// <summary>
		///     Priority for this issue
		/// </summary>
		[DataMember(Name = "priority", EmitDefaultValue = false)]
		public Priority Priority { get; set; }

		/// <summary>
		///     Progress for this issue
		/// </summary>
		[DataMember(Name = "progress", EmitDefaultValue = false)]
		public ProgressInfo Progress { get; set; }

		/// <summary>
		///     Project to which this issue belongs
		/// </summary>
		[DataMember(Name = "project", EmitDefaultValue = false)]
		public Project Project { get; set; }

		/// <summary>
		///     What user reported the issue?
		/// </summary>
		[DataMember(Name = "reporter", EmitDefaultValue = false)]
		public User Reporter { get; set; }

		/// <summary>
		///     The resolution for this issue
		/// </summary>
		[DataMember(Name = "resolution", EmitDefaultValue = false)]
		public Resolution Resolution { get; set; }

		/// <summary>
		///     Resolution date for this issue
		/// </summary>
		[DataMember(Name = "resolutiondate", EmitDefaultValue = false)]
		public DateTimeOffset ResolutionData { get; set; }

		/// <summary>
		///     Current status of the issue
		/// </summary>
		[DataMember(Name = "status", EmitDefaultValue = false)]
		public Status Status { get; set; }

		/// <summary>
		///     Summary for the issue
		/// </summary>
		[DataMember(Name = "summary", EmitDefaultValue = false)]
		public string Summary { get; set; }

		/// <summary>
		///     How much time is spent on this issue
		/// </summary>
		[DataMember(Name = "timespent", EmitDefaultValue = false)]
		public long TimeSpent { get; set; }

		/// <summary>
		///     Time tracking information
		/// </summary>
		[DataMember(Name = "timetracking", EmitDefaultValue = false)]
		public TimeTracking TimeTracking { get; set; }

		/// <summary>
		///     When was the last update
		/// </summary>
		[DataMember(Name = "updated", EmitDefaultValue = false)]
		public DateTimeOffset Updated { get; set; }

		/// <summary>
		///     Version for which this ticket is
		/// </summary>
		[DataMember(Name = "versions", EmitDefaultValue = false)]
		public IList<Version> Versions { get; set; }

		/// <summary>
		///     Information on the watches for the ticket
		/// </summary>
		[DataMember(Name = "watches", EmitDefaultValue = false)]
		public Watches Watches { get; set; }

		/// <summary>
		///     The worklog entries
		/// </summary>
		[DataMember(Name = "worklog", EmitDefaultValue = false)]
		public Worklogs Worklogs { get; set; }
	}
}