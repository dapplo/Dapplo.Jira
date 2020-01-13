// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if NET461 || NETCOREAPP3_0

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dapplo.Jira.PowerShell.Support
{
	/// <summary>
	///     Provides a pump that supports running asynchronous methods on the current thread.
	/// </summary>
	public static class AsyncPump
	{
		/// <summary>Runs the specified asynchronous function.</summary>
		/// <param name="func">The asynchronous function to execute.</param>
		public static void Run(Func<Task> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			var prevCtx = SynchronizationContext.Current;
			try
			{
				// Establish the new context
				var syncCtx = new SingleThreadSynchronizationContext();
				SynchronizationContext.SetSynchronizationContext(syncCtx);

				// Invoke the function and alert the context to when it completes
				var t = func();
				if (t == null)
				{
					throw new InvalidOperationException("No task provided.");
				}
				t.ContinueWith(delegate { syncCtx.Complete(); }, TaskScheduler.Default);

				// Pump continuations and propagate any exceptions
				syncCtx.RunOnCurrentThread();
				t.GetAwaiter().GetResult();
			}
			finally
			{
				SynchronizationContext.SetSynchronizationContext(prevCtx);
			}
		}

		/// <summary>Provides a SynchronizationContext that's single-threaded.</summary>
		private sealed class SingleThreadSynchronizationContext : SynchronizationContext
		{
			/// <summary>The queue of work items.</summary>
			private readonly BlockingCollection<KeyValuePair<SendOrPostCallback, object>> _queue =
				new BlockingCollection<KeyValuePair<SendOrPostCallback, object>>();

			/// <summary>Notifies the context that no more work will arrive.</summary>
			public void Complete()
			{
				_queue.CompleteAdding();
			}

			/// <summary>Dispatches an asynchronous message to the synchronization context.</summary>
			/// <param name="d">The System.Threading.SendOrPostCallback delegate to call.</param>
			/// <param name="state">The object passed to the delegate.</param>
			public override void Post(SendOrPostCallback d, object state)
			{
				if (d == null)
				{
					throw new ArgumentNullException(nameof(d));
				}
				_queue.Add(new KeyValuePair<SendOrPostCallback, object>(d, state));
			}

			/// <summary>Runs an loop to process all queued work items.</summary>
			public void RunOnCurrentThread()
			{
				foreach (var workItem in _queue.GetConsumingEnumerable())
				{
					workItem.Key(workItem.Value);
				}
			}

			/// <summary>Not supported.</summary>
			public override void Send(SendOrPostCallback d, object state)
			{
				throw new NotSupportedException("Synchronously sending is not supported.");
			}
		}
	}
}

#endif