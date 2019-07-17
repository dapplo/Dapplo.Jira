#region Dapplo 2017-2019 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017-2019 Dapplo
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


#if NET461 || NETCOREAPP3_0
using System.Collections.Generic;
using Dapplo.HttpExtensions;
using Dapplo.HttpExtensions.Extensions;
using Dapplo.HttpExtensions.Support;

namespace Dapplo.Jira.Converters
{
	/// <summary>
	///     Configuration for the SvgBitmapHttpContentConverter or SvgBitmapSourceHttpContentConverter
	/// </summary>
	public class SvgConfiguration : IHttpRequestConfiguration
	{
		/// <summary>
		///     Specify the supported content types
		/// </summary>
		public IList<string> SupportedContentTypes { get; } = new List<string>
		{
			MediaTypes.Svg.EnumValueOf()
		};

		/// <summary>
		///     Target width for the generated SVG Bitmap
		/// </summary>
		public int Width { get; set; } = 64;


		/// <summary>
		///     Target height for the generated SVG Bitmap
		/// </summary>
		public int Height { get; set; } = 64;

		/// <summary>
		///     Name of the configuration, this should be unique and usually is the type of the object
		/// </summary>
		public string Name { get; } = nameof(SvgConfiguration);
	}
}
#endif