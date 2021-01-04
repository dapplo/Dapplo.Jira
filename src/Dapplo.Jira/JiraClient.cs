// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dapplo.HttpExtensions;
using Dapplo.HttpExtensions.JsonNet;
using Dapplo.Jira.Domains;

#if NET461
using System.Net.Cache;

#endif

namespace Dapplo.Jira
{
    /// <summary>
    ///     A client for accessing the Atlassian JIRA Api via REST, using Dapplo.HttpExtensions
    /// </summary>
    public class JiraClient : IProjectDomain, IWorkDomain, IUserDomain, ISessionDomain, IIssueDomain, IFilterDomain, IAttachmentDomain, IServerDomain, IAgileDomain,
        IGreenhopperDomain
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

        /// <summary>
        ///     Helper method to configure the IChangeableHttpBehaviour
        /// </summary>
        /// <param name="behaviour">IChangeableHttpBehaviour</param>
        /// <param name="httpSettings">IHttpSettings</param>
        /// <returns>the behaviour, but configured as IHttpBehaviour </returns>
        public IHttpBehaviour ConfigureBehaviour(IChangeableHttpBehaviour behaviour, IHttpSettings httpSettings = null)
        {
            behaviour.HttpSettings = httpSettings ?? HttpExtensionsGlobals.HttpSettings.ShallowClone();
#if NET461
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
        /// <returns>IJiraClient for using it in a more fluent way</returns>
        public IJiraClient SetBasicAuthentication(string user, string password)
        {
            _user = user;
            _password = password;
            return this;
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
