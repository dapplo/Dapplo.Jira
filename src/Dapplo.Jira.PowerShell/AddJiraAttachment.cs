// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Management.Automation;
using System.Threading.Tasks;
using Dapplo.Jira.Entities;
using Dapplo.Jira.PowerShell.Support;

namespace Dapplo.Jira.PowerShell;

/// <summary>
///     A Cmdlet to attach an attachment to a jira issue
/// </summary>
[Cmdlet(VerbsCommon.Add, "JiraAttachment")]
[OutputType(typeof(Attachment))]
public class AddJiraAttachment : JiraAsyncCmdlet
{
    /// <summary>
    ///     Content-type for the uploaded file
    /// </summary>
    [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
    public string ContentType { get; set; }

    /// <summary>
    ///     Filename for the attachment
    /// </summary>
    [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 2, ValueFromPipelineByPropertyName = true)]
    public string Filename { get; set; }

    /// <summary>
    ///     Path from which to get the file to upload
    /// </summary>
    [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 3, ValueFromPipelineByPropertyName = true)]
    public string Filepath { get; set; }

    /// <summary>
    ///     Key for the issue to attach to
    /// </summary>
    [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true)]
    public string IssueKey { get; set; }

    /// <summary>
    ///     Do the actual uploading, return the attachment object(s)
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
            var attachment = await this.JiraApi.Attachment.AttachAsync(IssueKey, stream, Filename, ContentType).ConfigureAwait(false);
            WriteObject(attachment);
        }
    }
}