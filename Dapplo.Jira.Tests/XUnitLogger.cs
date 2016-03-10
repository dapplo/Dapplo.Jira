//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Config
// 
//  Dapplo.Config is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Config is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have Config a copy of the GNU Lesser General Public License
//  along with Dapplo.Config. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Threading;
using Dapplo.LogFacade;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Jira.Tests
{
	/// <summary>
	///     xUnit will have tests run parallel, and due to this it won't capture trace output correctly.
	///     This is where their ITestOutputHelper comes around, but Dapplo.LogFacade can only have one logger.
	///     This class solves the problem by registering the ITestOutputHelper in the CallContext.
	///     Every log statement will retrieve the ITestOutputHelper from the context and use it to log.
	/// </summary>
	public class XUnitLogger : ILogger
	{
		private static readonly AsyncLocal<ITestOutputHelper> TestOutputHelperAsyncLocal = new AsyncLocal<ITestOutputHelper>();
		private static readonly AsyncLocal<LogLevel> LogLevelAsyncLocal = new AsyncLocal<LogLevel>();

		/// <summary>
		///     Prevent the constructor from being use elsewhere
		/// </summary>
		private XUnitLogger()
		{
		}

		/// <summary>
		///     LogLevel, this can give a different result pro xUnit test...
		///     It will depend on the RegisterLogger value which was used in the current xUnit test
		/// </summary>
		public LogLevel Level
		{
			get
			{
				var logLevel = LogLevelAsyncLocal.Value;
				if (logLevel != LogLevel.None)
				{
					return logLevel;
				}
				return LogSettings.DefaultLevel;
			}
			set { LogLevelAsyncLocal.Value = value; }
		}

		/// <summary>
		///     If the level is enabled, this returns true
		///     The level depends on what the xUnit test used in the RegisterLogger
		/// </summary>
		/// <param name="level">LogLevel</param>
		/// <returns>true if the level is enabled</returns>
		public bool IsLogLevelEnabled(LogLevel level)
		{
			return level != LogLevel.None && level >= Level;
		}

		public void Write(LogInfo logInfo, string messageTemplate, params object[] logParameters)
		{
			var testOutputHelper = TestOutputHelperAsyncLocal.Value;
			if (testOutputHelper == null)
			{
				throw new ArgumentNullException(nameof(testOutputHelper), "Couldn't find a ITestOutputHelper in the CallContext");
			}
			testOutputHelper.WriteLine($"{logInfo} - {messageTemplate}", logParameters);
		}

		public void Write(LogInfo logInfo, Exception exception, string messageTemplate, params object[] logParameters)
		{
			var testOutputHelper = TestOutputHelperAsyncLocal.Value;
			if (testOutputHelper == null)
			{
				throw new ArgumentNullException(nameof(testOutputHelper), "Couldn't find a ITestOutputHelper in the CallContext");
			}
			testOutputHelper.WriteLine($"{logInfo} - {messageTemplate}", logParameters);
			testOutputHelper.WriteLine(exception.ToString());
		}

		/// <summary>
		///     Register the XUnitLogger,  as the global LogFacade logger
		///     This also places the ITestOutputHelper in the CallContext, so the output is mapped to the xUnit test
		/// </summary>
		/// <param name="testOutputHelper">ITestOutputHelper</param>
		/// <param name="level">LogLevel, when none is given the LogSettings.DefaultLevel is used</param>
		public static void RegisterLogger(ITestOutputHelper testOutputHelper, LogLevel level = default(LogLevel))
		{
			TestOutputHelperAsyncLocal.Value = testOutputHelper;
			LogLevelAsyncLocal.Value = level;
			if (!(LogSettings.Logger is XUnitLogger))
			{
				LogSettings.Logger = new XUnitLogger();
			}
		}
	}
}