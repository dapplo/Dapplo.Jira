// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     Factory method for JQL where clauses
	/// </summary>
	public static class Where
	{
		/// <summary>
		///     Create a clause for the IssueKey field
		/// </summary>
		public static IIssueClause IssueKey => new IssueClause();

	    /// <summary>
	    ///     Create a clause for the Status field
	    /// </summary>
	    public static IStatusClause Status => new StatusClause();

        /// <summary>
        ///     Create a clause for the type field
        /// </summary>
        public static ITypeClause Type => new TypeClause();

        /// <summary>
		///     Create a clause for the creator
		/// </summary>
		public static IUserClause Creator => new UserClause(Fields.Creator);

		/// <summary>
		///     Create a clause for the assignee
		/// </summary>
		public static IUserClause Assignee => new UserClause(Fields.Assignee);

		/// <summary>
		///     Create a clause for the mention
		/// </summary>
		public static IUserClause Approvals => new UserClause(Fields.Approvals);

		/// <summary>
		///     Create a clause for the watcher
		/// </summary>
		public static IUserClause Watcher => new UserClause(Fields.Watcher);

		/// <summary>
		///     Create a clause for the reporter
		/// </summary>
		public static IUserClause Reporter => new UserClause(Fields.Reporter);

		/// <summary>
		///     Create a clause for the voter
		/// </summary>
		public static IUserClause Voter => new UserClause(Fields.Voter);

		/// <summary>
		///     Create a clause for the WorkLogAuthor
		/// </summary>
		public static IUserClause WorkLogAuthor => new UserClause(Fields.WorkLogAuthor);

        /// <summary>
		///     Create a clause for the created field
		/// </summary>
		public static IDatetimeClause Created => new DatetimeClause(Fields.Created);

		/// <summary>
		///     Create a clause for the due field
		/// </summary>
		public static IDatetimeClause Due => new DatetimeClause(Fields.Due);

		/// <summary>
		///     Create a clause for the LastViewed field
		/// </summary>
		public static IDatetimeClause LastViewed => new DatetimeClause(Fields.LastViewed);

		/// <summary>
		///     Create a clause for the RequestLastActivityTime field
		/// </summary>
		public static IDatetimeClause RequestLastActivityTime => new DatetimeClause(Fields.RequestLastActivityTime);

		/// <summary>
		///     Create a clause for the Resolved field
		/// </summary>
		public static IDatetimeClause Resolved => new DatetimeClause(Fields.Resolved);

		/// <summary>
		///     Create a clause for the Updated field
		/// </summary>
		public static IDatetimeClause Updated => new DatetimeClause(Fields.Updated);

		/// <summary>
		///     Create a clause for the WorkLogDate field
		/// </summary>
		public static IDatetimeClause WorkLogDate => new DatetimeClause(Fields.WorkLogDate);

        /// <summary>
		/// Create an And of two or more where clauses
		/// </summary>
		/// <param name="clauses">Two or more IFinalClause</param>
		/// <returns>A new IFinalClause with an "and" of the specified IFinalClause</returns>
		public static IFinalClause And(params IFinalClause[] clauses)
		{
			if (clauses.Length < 2)
			{
				throw new ArgumentException("And needs two or more clauses.", nameof(clauses));
			}
			return new Clause("(" + string.Join(" and ", clauses.ToList()) + ")");
		}

		/// <summary>
		/// Create an Or of two or more where clauses
		/// </summary>
		/// <param name="clauses">Two or more IFinalClause</param>
		/// <returns>A new IFinalClause with an "or" of the specified IFinalClause</returns>
		public static IFinalClause Or(params IFinalClause[] clauses)
		{
			if (clauses.Length < 2)
			{
				throw new ArgumentException("Or needs two or more clauses.", nameof(clauses));
			}
			return new Clause("(" + string.Join(" or ", clauses.ToList()) + ")");
		}

        /// <summary>
		///     Create a clause for the Comment field
		/// </summary>
		public static ITextClause Comment => new TextClause(Fields.Comment);

		/// <summary>
		///     Create a clause for the Text field
		/// </summary>
		public static ITextClause Text => new TextClause(Fields.Text);

		/// <summary>
		///     Create a clause for the Environment field
		/// </summary>
		public static ITextClause Environment => new TextClause(Fields.Environment);

		/// <summary>
		///     Create a clause for the Description field
		/// </summary>
		public static ITextClause Description => new TextClause(Fields.Description);

		/// <summary>
		///     Create a clause for the Summary field
		/// </summary>
		public static ITextClause Summary => new TextClause(Fields.Summary);

		/// <summary>
		///     Create a clause for the WorkLogComment field
		/// </summary>
		public static ITextClause WorkLogComment => new TextClause(Fields.WorkLogComment);

		/// <summary>
		///     Create a clause for the RequestChannelType field
		/// </summary>
		public static ITextClause RequestChannelType => new TextClause(Fields.RequestChannelType);

        /// <summary>
		///     Create a clause for the AffectedVersion field
		/// </summary>
		public static IVersionClause AffectedVersion => new VersionClause(Fields.AffectedVersion);

		/// <summary>
		///     Create a clause for the FixVersion field
		/// </summary>
		public static IVersionClause FixVersion => new VersionClause(Fields.FixVersion);

        /// <summary>
		///     Create a clause for the Parent field
		/// </summary>
		public static ISimpleValueClause Parent => new SimpleValueClause(Fields.Parent);

		/// <summary>
		///     Create a clause for the Labels field
		/// </summary>
		public static ISimpleValueClause Labels => new SimpleValueClause(Fields.Labels);

        /// <summary>
		///     Create a clause for the Project field
		/// </summary>
		public static IProjectClause Project => new ProjectClause();

	}
}