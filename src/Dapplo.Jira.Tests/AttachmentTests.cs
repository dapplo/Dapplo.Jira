// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Dapplo.Log;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Jira.Tests
{
    public class AttachmentTests : TestBase
    {
        public AttachmentTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        public async Task TestAttachment()
        {
            const string filename = "test.txt";
            const string testContent = "Testing 1 2 3";
            var attachment = await Client.Attachment.AttachAsync(TestIssueKey, testContent, filename);
            Assert.NotNull(attachment);
            Assert.StartsWith("text/plain", attachment.MimeType);

            if (attachment.ThumbnailUri != null)
            {
                var attachmentThumbnail = await Client.Attachment.GetThumbnailAsAsync<Bitmap>(attachment);
                Assert.NotNull(attachmentThumbnail);
                Assert.True(attachmentThumbnail.Width > 0);
            }

            var returnedContent = await Client.Attachment.GetContentAsAsync<string>(attachment);
            Assert.Equal(testContent, returnedContent);

            var hasBeenRemoved = false;
            var issue = await Client.Issue.GetAsync(TestIssueKey);
            foreach (var attachment2Delete in issue.Fields.Attachments.Where(x => x.Filename == filename))
            {
                Log.Info().WriteLine("Deleting {0} from {1}", attachment2Delete.Filename, attachment2Delete.Created);
                await Client.Attachment.DeleteAsync(attachment2Delete);
                hasBeenRemoved = true;
            }

            Assert.True(hasBeenRemoved);
        }
    }
}
