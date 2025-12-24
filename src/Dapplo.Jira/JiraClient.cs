// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using RestSharp;
using System.Text.Json;

namespace Dapplo.Jira
{
    /// <summary>
    ///     A client for accessing the Atlassian JIRA Api via REST
    ///     Modernized to support both RestSharp and legacy Dapplo.HttpExtensions patterns
    /// </summary>
    public class JiraClient : IProjectDomain, IWorkLogDomain, IUserDomain, ISessionDomain, IIssueDomain, IFilterDomain, IAttachmentDomain, IServerDomain, IAgileDomain,
        IGreenhopperDomain
    {
        private string password;
        private string user;
        private string bearer;
        private readonly RestClient _restClient;

        /// <summary>
        ///     Factory method to create the jira client (legacy API)
        /// </summary>
        [Obsolete("Use Create(Uri) instead. IHttpSettings parameter is no longer used.")]
        public static IJiraClient Create(Uri baseUri, object httpSettings) => new JiraClient(baseUri);

        /// <summary>
        ///     Factory method to create the jira client
        /// </summary>
        public static IJiraClient Create(Uri baseUri) => new JiraClient(baseUri);

        /// <summary>
        ///     Create the JiraApi object, here the RestClient is configured
        /// </summary>
        /// <param name="baseUri">Base URL, e.g. https://yourjiraserver</param>
        private JiraClient(Uri baseUri)
        {
            JiraBaseUri = baseUri;
            JiraRestUri = baseUri.AppendSegments("rest", "api", "2");
            JiraAuthUri = baseUri.AppendSegments("rest", "auth", "1");
            JiraAgileRestUri = baseUri.AppendSegments("rest", "agile", "1.0");
            JiraGreenhopperRestUri = baseUri.AppendSegments("rest", "greenhopper", "1.0");

            // Configure RestClient
            var options = new RestClientOptions(baseUri)
            {
                ThrowOnAnyError = false,
                MaxTimeout = 30000
            };

            _restClient = new RestClient(options, configureSerialization: s => 
            {
                s.UseSystemTextJson(new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                    Converters = { new Json.JiraDateTimeOffsetConverter() }
                });
            });

            // Set up default request interceptor
            _restClient.AddDefaultHeader("X-Atlassian-Token", "nocheck");
            
            // Store the client for extension methods
            RestSharpExtensions.SetRestClient(_restClient);
        }

        /// <summary>
        ///     Configure authentication on requests
        /// </summary>
        internal void ConfigureAuthentication(RestRequest request)
        {
            if (!string.IsNullOrEmpty(user) && password != null)
            {
                var authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user}:{password}"));
                request.AddHeader("Authorization", $"Basic {authValue}");
            }
            else if (!string.IsNullOrEmpty(bearer))
            {
                request.AddHeader("Authorization", $"Bearer {bearer}");
            }
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
    }
}
