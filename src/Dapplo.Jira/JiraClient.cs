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

#region Usings

using System;
using Dapplo.HttpExtensions;
using Dapplo.HttpExtensions.JsonNet;
using Dapplo.Jira.Domains;

#if NET471 || NETCOREAPP3_0
using Dapplo.HttpExtensions.OAuth;
using Dapplo.HttpExtensions.Extensions;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using Dapplo.Jira.Converters;
using System.Net.Cache;
#endif

#endregion

namespace Dapplo.Jira
{
    /// <summary>
    ///     A client for accessing the Atlassian JIRA Api via REST, using Dapplo.HttpExtensions
    /// </summary>
    public class JiraClient : IProjectDomain, IWorkDomain, IUserDomain, ISessionDomain, IIssueDomain, IFilterDomain, IAttachmentDomain, IServerDomain, IAgileDomain, IGreenhopperDomain
    {
        private string _password;
        private string _user;

        /// <summary>
        ///     Store the specific HttpBehaviour, which contains a IHttpSettings and also some additional logic for making a
        ///     HttpClient which works with Jira
        /// </summary>
        public IHttpBehaviour Behaviour { get; set; }

        /// <summary>
        ///     Factory method to create the jira client
        /// </summary>
        public static IJiraClient Create(Uri baseUri, IHttpSettings httpSettings = null)
        {
            return new JiraClient(baseUri, httpSettings);
        }

        /// <summary>
        ///     Create the JiraApi object, here the HttpClient is configured
        /// </summary>
        /// <param name="baseUri">Base URL, e.g. https://yourjiraserver</param>
        /// <param name="httpSettings">IHttpSettings or null for default</param>
        private JiraClient(Uri baseUri, IHttpSettings httpSettings = null)
        {
            Behaviour = ConfigureBehaviour(new HttpBehaviour(), httpSettings);

            JiraBaseUri = baseUri;
            JiraRestUri = baseUri.AppendSegments("rest", "api", "2");
            JiraAuthUri = baseUri.AppendSegments("rest", "auth", "1");
            JiraAgileRestUri = baseUri.AppendSegments("rest", "agile", "1.0");
            JiraGreenhopperRestUri = baseUri.AppendSegments("rest", "greenhopper", "1.0");
        }

#if NET471 || NETCOREAPP3_0
        /// <summary>
        ///     Create the JiraApi, using OAuth 1 for the communication, here the HttpClient is configured
        /// </summary>
        /// <param name="baseUri">Base URL, e.g. https://yourjiraserver</param>
        /// <param name="jiraOAuthSettings">JiraOAuthSettings</param>
        /// <param name="httpSettings">IHttpSettings or null for default</param>
        public static IJiraClient Create(Uri baseUri, JiraOAuthSettings jiraOAuthSettings, IHttpSettings httpSettings = null)
        {
            JiraClient client = new JiraClient(baseUri, httpSettings);
            var jiraOAuthUri = client.JiraBaseUri.AppendSegments("plugins", "servlet", "oauth");

            var oAuthSettings = new OAuth1Settings
            {
                TokenUrl = jiraOAuthUri.AppendSegments("request-token"),
                TokenMethod = HttpMethod.Post,
                AccessTokenUrl = jiraOAuthUri.AppendSegments("access-token"),
                AccessTokenMethod = HttpMethod.Post,
                CheckVerifier = false,
                SignatureType = OAuth1SignatureTypes.RsaSha1,
                Token = jiraOAuthSettings.Token,
                ClientId = jiraOAuthSettings.ConsumerKey,
                CloudServiceName = jiraOAuthSettings.CloudServiceName,
                RsaSha1Provider = jiraOAuthSettings.RsaSha1Provider,
                AuthorizeMode = jiraOAuthSettings.AuthorizeMode,
                AuthorizationUri = jiraOAuthUri.AppendSegments("authorize")
                    .ExtendQuery(new Dictionary<string, string>
                    {
                        {OAuth1Parameters.Token.EnumValueOf(), "{RequestToken}"},
                        {OAuth1Parameters.Callback.EnumValueOf(), "{RedirectUrl}"}
                    })
            };

            // Configure the OAuth1Settings

            client.Behaviour = client.ConfigureBehaviour(OAuth1HttpBehaviourFactory.Create(oAuthSettings), httpSettings);
            return client;
        }
#endif

        /// <summary>
        ///     Helper method to configure the IChangeableHttpBehaviour
        /// </summary>
        /// <param name="behaviour">IChangeableHttpBehaviour</param>
        /// <param name="httpSettings">IHttpSettings</param>
        /// <returns>the behaviour, but configured as IHttpBehaviour </returns>
        private IHttpBehaviour ConfigureBehaviour(IChangeableHttpBehaviour behaviour, IHttpSettings httpSettings = null)
        {

#if NET471 || NETCOREAPP3_0
            // Add SvgBitmapHttpContentConverter if it was not yet added 
            if (HttpExtensionsGlobals.HttpContentConverters.All(x => x.GetType() != typeof(SvgBitmapHttpContentConverter)))
            {
                HttpExtensionsGlobals.HttpContentConverters.Add(SvgBitmapHttpContentConverter.Instance.Value);
            }
#endif
            behaviour.HttpSettings = httpSettings ?? HttpExtensionsGlobals.HttpSettings.ShallowClone();
#if NET471
            // Disable caching, if no HTTP settings were provided.
            if (httpSettings == null)
            {
                behaviour.HttpSettings.RequestCacheLevel = RequestCacheLevel.NoCacheNoStore;
            }
#endif

            // Using our own Json Serializer, implemented with Json.NET
            behaviour.JsonSerializer = new JsonNetJsonSerializer();

            behaviour.OnHttpRequestMessageCreated = httpMessage =>
            {
                httpMessage?.Headers.TryAddWithoutValidation("X-Atlassian-Token", "nocheck");
                if (!string.IsNullOrEmpty(_user) && _password != null)
                {
                    httpMessage?.SetBasicAuthorization(_user, _password);
                }
                return httpMessage;
            };
            return behaviour;
        }

        /// <summary>
        ///     The base URI for your JIRA server
        /// </summary>
        public Uri JiraBaseUri { get; }

        /// <summary>
        ///     The rest URI for your JIRA server
        /// </summary>
        public Uri JiraRestUri { get; }


        /// <summary>
        ///     The agile rest URI for your JIRA server
        /// </summary>
        public Uri JiraAgileRestUri { get; }

        /// <summary>
        ///     The greenhopper rest URI for your JIRA server
        /// </summary>
        public Uri JiraGreenhopperRestUri { get; }

        /// <summary>
        ///     The base URI for JIRA auth api
        /// </summary>
        public Uri JiraAuthUri { get; }

        /// <summary>
        ///     Set Basic Authentication for the current client
        /// </summary>
        /// <param name="user">username</param>
        /// <param name="password">password</param>
        public void SetBasicAuthentication(string user, string password)
        {
            _user = user;
            _password = password;
        }

        /// <summary>
        ///     Issue domain
        /// </summary>
        public IIssueDomain Issue => this;

        /// <summary>
        ///     Attachment domain
        /// </summary>
        public IAttachmentDomain Attachment => this;

        /// <summary>
        ///     Project domain
        /// </summary>
        public IProjectDomain Project => this;

        /// <summary>
        ///     User domain
        /// </summary>
        public IUserDomain User => this;

        /// <summary>
        ///     Session domain
        /// </summary>
        public ISessionDomain Session => this;

        /// <summary>
        ///     Filter domain
        /// </summary>
        public IFilterDomain Filter => this;

        /// <summary>
        ///     Work domain
        /// </summary>
        public IWorkDomain Work => this;

        /// <summary>
        ///     Server domain
        /// </summary>
        public IServerDomain Server => this;

        /// <summary>
        ///     Agile domain
        /// </summary>
        public IAgileDomain Agile => this;

        /// <summary>
        ///     Greenhopper domain
        /// </summary>
        public IGreenhopperDomain Greenhopper => this;
    }
}