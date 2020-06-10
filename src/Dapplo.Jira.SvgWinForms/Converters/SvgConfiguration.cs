// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dapplo.HttpExtensions;
using Dapplo.HttpExtensions.Extensions;
using Dapplo.HttpExtensions.Support;

namespace Dapplo.Jira.SvgWinForms.Converters
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