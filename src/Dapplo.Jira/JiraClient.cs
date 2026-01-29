// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dapplo.HttpExtensions.JsonNet;

namespace Dapplo.Jira
{
    /// <summary>
    ///     A client for accessing the Atlassian JIRA Api via REST
    ///     NOTE: Modernization in progress - currently using Dapplo.HttpExtensions, will migrate to RestSharp
    /// </summary>
    public class JiraClient : IProjectDomain, IWorkLogDomain, IUserDomain, ISessionDomain, IIssueDomain, IFilterDomain, IAttachmentDomain, IServerDomain, IAgileDomain,
        IGreenhopperDomain, IGroupDomain
    {
        private string password;
        private string user;
        private string bearer;

        /// <summary>
        ///     Store the specific HttpBehaviour, which contains a IHttpSettings and also some additional logic for making a
        ///     HttpClient which works with Jira
        /// </summary>
        public IHttpBehaviour Behaviour { get; set; }

        /// <summary>
        ///     Factory method to create the jira client
        /// </summary>
        public static IJiraClient Create(Uri baseUri, IHttpSettings httpSettings = null) => new JiraClient(baseUri, httpSettings);

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
            JiraV3RestUri = baseUri.AppendSegments("rest", "api", "3");
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

            // Using our own Json Serializer, implemented with Json.NET
            behaviour.JsonSerializer = new JsonNetJsonSerializer();

            behaviour.OnHttpRequestMessageCreated = httpMessage =>
            {
                httpMessage?.Headers.TryAddWithoutValidation("X-Atlassian-Token", "nocheck");
                if (!string.IsNullOrEmpty(this.user) && this.password != null)
                {
                    httpMessage?.SetBasicAuthorization(this.user, this.password);
                }
                if (!string.IsNullOrEmpty(this.bearer))
                {
                    httpMessage?.SetBearerAuthorization(this.bearer);
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
        ///     The rest V3 URI for your JIRA server
        /// </summary>
        public Uri JiraV3RestUri { get; }
        
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
            this.user = user;
            this.password = password;
            return this;
        }

        /// <summary>
        ///     Set Bearer Authentication for the current client
        /// </summary>
        /// <param name="bearer">bearer</param>
        /// <returns>IJiraClient for using it in a more fluent way</returns>
        public IJiraClient SetBearerAuthentication(string bearer)
        {
            this.bearer = bearer;
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
        ///     WorkLog domain
        /// </summary>
        public IWorkLogDomain WorkLog => this;

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

        /// <summary>
        ///     Group domain
        /// </summary>
        public IGroupDomain Group => this;
    }
}
