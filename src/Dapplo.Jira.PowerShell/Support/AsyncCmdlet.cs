// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Management.Automation;
using System.Threading.Tasks;

namespace Dapplo.Jira.PowerShell.Support
{
    /// <summary>
    ///     The base for Async Cmdlets
    /// </summary>
    public abstract class AsyncCmdlet : PSCmdlet
    {
        /// <summary>
        ///     This is called from the "Powerhell Cmdlet" framework, calls the BeginProcessingAsync
        /// </summary>
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            AsyncPump.Run(async () => await BeginProcessingAsync().ConfigureAwait(false));
        }

        /// <summary>
        ///     Override this to implement the BeginProcessing with Async code
        /// </summary>
        protected virtual Task BeginProcessingAsync() => Task.FromResult(0);

        /// <summary>
        ///     This is called from the "Powershell Cmdlet" framework, calls the EndProcessingAsync
        /// </summary>
        protected sealed override void EndProcessing()
        {
            AsyncPump.Run(async () => await EndProcessingAsync().ConfigureAwait(false));
        }

        /// <summary>
        ///     Override this to implement the EndProcessing with Async code
        /// </summary>
        protected virtual Task EndProcessingAsync() => Task.FromResult(0);

        /// <summary>
        ///     Override to ProcessRecord and call ProcessRecordAsync
        /// </summary>
        protected sealed override void ProcessRecord()
        {
            AsyncPump.Run(async () => await ProcessRecordAsync().ConfigureAwait(false));
        }

        /// <summary>
        ///     Override this to implement the ProcessRecord with Async code
        /// </summary>
        protected virtual Task ProcessRecordAsync() => Task.FromResult(0);
    }
}
