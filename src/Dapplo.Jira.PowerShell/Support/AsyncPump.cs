#region Dapplo 2017-2018 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2018 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Jira
// 
// Dapplo.Jira is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Jira is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#if NET471 || NETCOREAPP3_0

#region Usings

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#endregion

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