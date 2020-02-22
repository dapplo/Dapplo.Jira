// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dapplo.HttpExtensions;
using Dapplo.HttpExtensions.OAuth;
using Dapplo.HttpExtensions.Extensions;
using System.Collections.Generic;
using System.Net.Http;

namespace Dapplo.Jira.OAuth
{
    /// <summary>
    ///     Just a factory for creating client which supports OAuth
    /// </summary>
    public static class OAuthJiraClient
    {
        /// <summary>
        ///     Create the JiraApi, using OAuth 1 for the communication, here the HttpClient is configured
        /// </summary>
        /// <param name="baseUri">Base URL, e.g. https://yourjiraserver</param>
        /// <param name="jiraOAuthSettings">JiraOAuthSettings</param>
        /// <param name="httpSettings">IHttpSettings or null for default</param>
        public static IJiraClient Create(Uri baseUri, JiraOAuthSettings jiraOAuthSettings, IHttpSettings httpSettings = null)
        {
            JiraClient client = JiraClient.Create(baseUri, httpSettings) as JiraClient;
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
    }
}