//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.PowerShell.Jira
// 
//  Dapplo.PowerShell.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.PowerShell.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.PowerShell.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System.Management.Automation;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;
using Dapplo.PowerShell.Jira.Support;
using System.IO;

#endregion

namespace Dapplo.PowerShell.Jira
{
	/// <summary>
	/// A Cmdlet to attach an attachment to a jira issue
	/// </summary>
	[Cmdlet(VerbsCommon.Add, "JiraAttachment")]
	[OutputType(typeof (Attachment))]
	public class AddJiraAttachment : JiraAsyncCmdlet
	{
		/// <summary>
		/// Key for the issue to attach to
		/// </summary>
		[Parameter(ValueFromPipeline = true, Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true)]
		public string IssueKey { get; set; }

		/// <summary>
		/// Filename for the attachment
		/// </summary>
		[Parameter(ValueFromPipeline = true, Mandatory = true, Position = 2, ValueFromPipelineByPropertyName = true)]
		public string Filename { get; set; }

		/// <summary>
		/// Path from which to get the file to upload
		/// </summary>
		[Parameter(ValueFromPipeline = true, Mandatory = true, Position = 3, ValueFromPipelineByPropertyName = true)]
		public string Filepath { get; set; }

		/// <summary>
		/// Content-type for the uploaded file
		/// </summary>
		[Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public string ContentType { get; set; }

		/// <summary>
		/// Do the actual uploading, return the attachment object(s)
		/// </summary>
		protected override async Task ProcessRecordAsync()
		{
			if (!Path.IsPathRooted(Filepath))
			{
				var currentDirectory = CurrentProviderLocation("FileSystem").ProviderPath;
				Filepath = Path.Combine(currentDirectory, Filepath);
			}

			if (!File.Exists(Filepath))
			{
				throw new FileNotFoundException($"Couldn't find file {Filepath}");
			}
			using (var stream = File.OpenRead(Filepath))
			{
				var attachments = await JiraApi.AttachAsync(IssueKey, stream, Filename, ContentType);
				foreach (var attachment in attachments)
				{
					WriteObject(attachment);
				}
			}
		}
	}
}