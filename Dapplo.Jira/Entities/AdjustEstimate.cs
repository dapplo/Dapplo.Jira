using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Dapplo.Jira.Entities
{
	/// <summary>
	/// allows you to provide specific instructions to update the remaining time estimate of the issue. Valid values are
	/// </summary>
	public enum AdjustEstimate
	{
		/// <summary>
		/// Default option. Will automatically adjust the value based on the new timeSpent specified on the worklog
		/// </summary>
		[Description("auto")]
		Auto,
		/// <summary>
		/// sets the estimate to a specific value
		/// </summary>
		[Description("new")]
		New,
		/// <summary>
		/// leaves the estimate as is
		/// </summary>
		[Description("leave")]
		Leave,
		/// <summary>
		/// specify a specific amount to increase remaining estimate by
		/// </summary>
		[Description("manual")]
		Manual
	}
}
