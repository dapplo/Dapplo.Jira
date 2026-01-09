// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Dapplo.HttpExtensions.OAuth;
using Dapplo.Jira.OAuth;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit;

namespace Dapplo.Jira.Tests;

/// <summary>
///     Test a OAuth connection to the JIRA system.
///     The process would be as follows:
///     1) Create a public/private key pair once for your application
///     2) setup an application link in Jira, as incoming connection. Use the public key created in 1. Give a "consumer
///     key", this can be anything.
///     3) In your code, create a RSACryptoServiceProvider from the private key (however you want) and fill the
///     OAuth1Settings (if you have token information, pass this)
///     4) Create the JiraApi instance with the Uri and OAuth1Settings -> start using it.
///     5) At the first connect, the authentication challenge is started if there wasn't any token information
///     6) Store the OAuthToken / OAuthTokenSecret / OAuthTokenVerifier for later usage...
/// </summary>
public class OAuthTests : IOAuth1Token
{
    // Test against a well known JIRA
    private static readonly Uri TestJiraUri = new Uri("https://greenshot.atlassian.net");
    private readonly IJiraClient jiraApi;

    public OAuthTests(ITestOutputHelper testOutputHelper)
    {
        // A demo Private key, to create a RSACryptoServiceProvider.
        // This was created from a .pem via a tool here http://www.jensign.com/opensslkey/index.html
        const string privateKeyXml = @"<RSAKeyValue><Modulus>tGIwsCH2KKa6vxUDupW92rF68S5SRbgr7Yp0xeadBsb0BruTt4GMrVL7QtiZWM8qUkY1ccMa7LkXD93uuNUnQEsH65s8ryID9P
DeEtCBcxFEZFdcKfyKR+5B+NRLW5lJq10sHzWbJ0EROUmEjoYfi3CtsMkJHYHDL9dZeCqAZHM=</Modulus><Exponent>AQAB</Exponent><P>14DdDg26
CrLhAFQIQLT1KrKVPYr0Wusi2ovZApz2/RnM7a7CWUJuDR3ClW5g9hdi+KQ0ceD5oJYX5Vexv2uk+w==</P><Q>1kfU0+DkXc6I/jXHJ6pDLA5s7dBHzWgDs
BzplSdkVQbKT3MbeYjeByOxzXhulOWLBQW/vxmW4HwU95KTRlj06Q==</Q><DP>SPoBYY3yb0cN/J94P/lHgJMDCNkyUEuJ/PoYndLrrN/8zow8kh91xwlJ6
HJ9cTiQMmTgwaOOxPuu0eI1df4M2w==</DP><DQ>klJaspRPXP877NssM5nAZMU0/O/NGCZ+3jPgDUno6WbJn5cqm8MqWhW1xGkImgRk+fkDBquiq4gPiT89
8jusgQ==</DQ><InverseQ>d5Zrr6Q8AO/0isr/3aa6O6NLQxISLKcPDk2NOccAfS/xOtfOz4sJYM3+Bs4Io9+dZGSDCA54Lw03eHTNQghS0A==</Inverse
Q><D>WFlbZXlM2r5G6z48tE+RTKLvB1/btgAtq8vLw/5e3KnnbcDD6fZO07m4DRaPjRryrJdsp8qazmUdcY0O1oK4FQfpprknDjP+R1XHhbhkQ4WEwjmxPst
ZMUZaDWF58d3otc23mCzwh3YcUWFu09KnMpzZsK59OfyjtkS44EDWpbE=</D></RSAKeyValue>";

        // Create the RSACryptoServiceProvider for the XML above
        var rsaCryptoServiceProvider = new RSACryptoServiceProvider();
        rsaCryptoServiceProvider.FromXmlString(privateKeyXml);

        // Configure the XUnitLogger for logging
        LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);

        // Only a few settings for the Jira OAuth are important
        var oAuthSettings = new JiraOAuthSettings
        {
            // Is specified on the linked-applications as consumer key
            ConsumerKey = "lInXLgx6HbF9FFq1ZQN8iSEnhzO3JVuf",
            // This needs to have the private key, the represented public key is set in the linked-applications
            RsaSha1Provider = rsaCryptoServiceProvider,
            // Use a server at Localhost to redirect to, alternative an embedded browser can be used
            AuthorizeMode = AuthorizeModes.LocalhostServer,
            // When using the embbed browser this is directly visible, with the LocalhostServer it's in the info notice after a redirect
            CloudServiceName = "Greenshot Jira",
            // the IOAuth1Token implementation, here it's this, gets the tokens to store & retrieve for later
            Token = this
        };
        // Create the JiraApi for the Uri and the settings
        jiraApi = OAuthJiraClient.Create(TestJiraUri, oAuthSettings);
    }

    public string OAuthToken { get; set; }
    public string OAuthTokenSecret { get; set; }
    public string OAuthTokenVerifier { get; set; }


    /// <summary>
    ///     This will test Oauth with a LocalServer "code" receiver
    /// </summary>
    /// <returns>Task</returns>
    //[Fact]
    public async Task TestOauthRequest()
    {
        // Check "who am I" so we can see that the user who authenticated is really logged in
        var user = await jiraApi.User.GetMyselfAsync();
        Assert.NotNull(user);
        Assert.NotEmpty(OAuthToken);
        Assert.NotEmpty(OAuthTokenSecret);
    }
}
