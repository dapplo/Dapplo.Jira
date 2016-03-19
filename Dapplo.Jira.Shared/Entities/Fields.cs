//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2016 Dapplo
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
//  You should have Config a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace Dapplo.Jira.Entities
{
	/// <summary>
	///     Container for the fields
	/// </summary>
	[DataContract]
	public class Fields
	{
		[DataMember(Name = "aggregatetimespent")]
		public long AggregateTimeSpent { get; set; }

		[DataMember(Name = "assignee")]
		public User Assignee { get; set; }

		[DataMember(Name = "attachment")]
		public IList<Attachment> Attachments { get; set; }

		[DataMember(Name = "comment")]
		public Comments Comments { get; set; }

		[DataMember(Name = "created")]
		public DateTimeOffset Created { get; set; }

		[DataMember(Name = "creator")]
		public User Creator { get; set; }

		[DataMember(Name = "description")]
		public string Description { get; set; }

		[DataMember(Name = "fixVersions")]
		public IList<Version> FixVersions { get; set; }

		[DataMember(Name = "versions")]
		public IList<Version> Versions { get; set; }

		[DataMember(Name = "issueType")]
		public IssueType IssueType { get; set; }

		[DataMember(Name = "labels")]
		public IList<string> Labels { get; set; }

		[DataMember(Name = "lastViewed")]
		public DateTimeOffset LastViewed { get; set; }

		[DataMember(Name = "priority")]
		public Priority Priority { get; set; }

		[DataMember(Name = "progress")]
		public ProgressInfo Progress { get; set; }

		[DataMember(Name = "project")]
		public Project Project { get; set; }

		[DataMember(Name = "reporter")]
		public User Reporter { get; set; }

		[DataMember(Name = "resolution")]
		public Resolution Resolution { get; set; }

		[DataMember(Name = "resolutiondate")]
		public DateTimeOffset ResolutionData { get; set; }

		[DataMember(Name = "updated")]
		public DateTimeOffset Updated { get; set; }

		[DataMember(Name = "status")]
		public Status Status { get; set; }

		[DataMember(Name = "summary")]
		public string Summary { get; set; }

		[DataMember(Name = "timespent")]
		public long TimeSpent { get; set; }

		[DataMember(Name = "watches")]
		public Watches Watches { get; set; }

		[DataMember(Name = "worklog")]
		public Worklogs Worklogs { get; set; }
	}
}