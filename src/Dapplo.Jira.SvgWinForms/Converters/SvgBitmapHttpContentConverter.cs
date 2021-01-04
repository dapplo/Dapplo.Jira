﻿// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.HttpExtensions.ContentConverter;
using Dapplo.HttpExtensions.Extensions;
using Dapplo.HttpExtensions.Support;
using Dapplo.Log;
using Svg;

namespace Dapplo.Jira.SvgWinForms.Converters
{
    /// <summary>
    ///     This adds SVG image support as jira uses this.
    /// </summary>
    public class SvgBitmapHttpContentConverter : IHttpContentConverter
    {
        private static readonly LogSource Log = new LogSource();

        /// <summary>
        /// Instance of this IHttpContentConverter for reusing
        /// </summary>
        public static Lazy<IHttpContentConverter> Instance
        {
            get;
        } = new Lazy<IHttpContentConverter>(() => new SvgBitmapHttpContentConverter());

        /// <inheritdoc />
        public int Order => 0;

        /// <summary>
        ///     This checks if the HttpContent can be converted to a Bitmap and is assignable to the specified Type
        /// </summary>
        /// <param name="typeToConvertTo">This should be something we can assign Bitmap to</param>
        /// <param name="httpContent">HttpContent to process</param>
        /// <returns>true if it can convert</returns>
        public bool CanConvertFromHttpContent(Type typeToConvertTo, HttpContent httpContent)
        {
            if ((typeToConvertTo == typeof(object)) || !typeToConvertTo.IsAssignableFrom(typeof(Bitmap)))
            {
                return false;
            }

            var httpBehaviour = HttpBehaviour.Current;
            var configuration = httpBehaviour.GetConfig<SvgConfiguration>();

            return !httpBehaviour.ValidateResponseContentType || configuration.SupportedContentTypes.Contains(httpContent.GetContentType());
        }

        /// <inheritdoc />
        public async Task<object> ConvertFromHttpContentAsync(Type resultType, HttpContent httpContent, CancellationToken cancellationToken = default)
        {
            if (!CanConvertFromHttpContent(resultType, httpContent))
            {
                var exMessage = "CanConvertFromHttpContent resulted in false, ConvertFromHttpContentAsync is not supposed to be called.";
                Log.Error().WriteLine(exMessage);
                throw new NotSupportedException(exMessage);
            }

            var httpBehaviour = HttpBehaviour.Current;
            var configuration = httpBehaviour.GetConfig<SvgConfiguration>();
            using (var memoryStream = (MemoryStream)await StreamHttpContentConverter.Instance.Value
                .ConvertFromHttpContentAsync(typeof(MemoryStream), httpContent, cancellationToken).ConfigureAwait(false))
            {
                Log.Debug().WriteLine("Creating a Bitmap from the SVG.");

                var bitmap = new Bitmap(configuration.Width, configuration.Height, PixelFormat.Format32bppArgb);
                // ImageHelper.CreateEmpty(Width, Height, PixelFormat.Format32bppArgb, Color.Transparent, 96, 96);
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.Transparent);
                }

                var svgDoc = SvgDocument.Open<SvgDocument>(memoryStream);
                svgDoc.Width = configuration.Width;
                svgDoc.Height = configuration.Height;
                svgDoc.Draw(bitmap);
                return bitmap;
            }
        }

        /// <inheritdoc />
        public bool CanConvertToHttpContent(Type typeToConvert, object content)
        {
            return false;
        }

        /// <inheritdoc />
        public HttpContent ConvertToHttpContent(Type typeToConvert, object content)
        {
            return null;
        }

        /// <inheritdoc />
        public void AddAcceptHeadersForType(Type resultType, HttpRequestMessage httpRequestMessage)
        {
            if (resultType == null)
            {
                throw new ArgumentNullException(nameof(resultType));
            }

            if (httpRequestMessage == null)
            {
                throw new ArgumentNullException(nameof(httpRequestMessage));
            }

            if ((resultType == typeof(object)) || !resultType.IsAssignableFrom(typeof(Bitmap)))
            {
                return;
            }

            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypes.Svg.EnumValueOf()));
            Log.Debug().WriteLine("Modified the header(s) of the HttpRequestMessage: Accept: {0}", httpRequestMessage.Headers.Accept);
        }
    }
}
