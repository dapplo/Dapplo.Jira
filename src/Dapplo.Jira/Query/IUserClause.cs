// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dapplo.Jira.Entities;

namespace Dapplo.Jira.Query
{
	/// <summary>
	///     The possible methods for a user clause
	/// </summary>
	public interface IUserClause
	{
		/// <summary>
		///     This allows fluent constructs like Creator.IsCurrentUser
		/// </summary>
		IFinalClause IsCurrentUser { get; }

		/// <summary>
		///     Negates the expression
		/// </summary>
		IUserClause Not { get; }

		/// <summary>
		///     This allows fluent constructs like Creator.In("smith", "squarepants")
		/// </summary>
		IFinalClause In(params string[] users);

		/// <summary>
		///     This allows fluent constructs like Creator.In(user1, user2)
		/// </summary>
		IFinalClause In(params User[] users);

		/// <summary>
		///     This allows fluent constructs like Creator.InCurrentUserAnd("smith", "squarepants")
		/// </summary>
		/// <param name="users"></param>
		/// <returns></returns>
		IFinalClause InCurrentUserAnd(params string[] users);

		/// <summary>
		///     This allows fluent constructs like Creator.InCurrentUserAnd(user)
		/// </summary>
		/// <param name="users"></param>
		/// <returns></returns>
		IFinalClause InCurrentUserAnd(params User[] users);

		/// <summary>
		///     This allows fluent constructs like Creator.Is("smith")
		/// </summary>
		IFinalClause Is(string user);


		/// <summary>
		///     This allows fluent constructs like Creator.Is(user)
		/// </summary>
		IFinalClause Is(User user);
	}
}