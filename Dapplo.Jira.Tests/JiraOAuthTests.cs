using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Dapplo.HttpExtensions.OAuth;
using Dapplo.Log.Facade;
using Dapplo.Log.XUnit;
using DevDefined.OAuth.KeyInterop;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Jira.Tests
{
	public class JiraOAuthTests
	{
		// Test against a well known JIRA
		private static readonly Uri TestJiraUri = new Uri("https://greenshot.atlassian.net");
		private readonly JiraApi _jiraApi;

		public JiraOAuthTests(ITestOutputHelper testOutputHelper)
		{
			// Retrieve the Private key from Demo Certificate.
			var parser = new AsnKeyParser(Convert.FromBase64String(
				@"MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBALRiMLAh9iimur8V
A7qVvdqxevEuUkW4K+2KdMXmnQbG9Aa7k7eBjK1S+0LYmVjPKlJGNXHDGuy5Fw/d
7rjVJ0BLB+ubPK8iA/Tw3hLQgXMRRGRXXCn8ikfuQfjUS1uZSatdLB81mydBETlJ
hI6GH4twrbDJCR2Bwy/XWXgqgGRzAgMBAAECgYBYWVtleUzavkbrPjy0T5FMou8H
X9u2AC2ry8vD/l7cqedtwMPp9k7TubgNFo+NGvKsl2ynyprOZR1xjQ7WgrgVB+mm
uScOM/5HVceFuGRDhYTCObE+y1kxRloNYXnx3ei1zbeYLPCHdhxRYW7T0qcynNmw
rn05/KO2RLjgQNalsQJBANeA3Q4Nugqy4QBUCEC09SqylT2K9FrrItqL2QKc9v0Z
zO2uwllCbg0dwpVuYPYXYvikNHHg+aCWF+VXsb9rpPsCQQDWR9TT4ORdzoj+Nccn
qkMsDmzt0EfNaAOwHOmVJ2RVBspPcxt5iN4HI7HNeG6U5YsFBb+/GZbgfBT3kpNG
WPTpAkBI+gFhjfJvRw38n3g/+UeAkwMI2TJQS4n8+hid0uus3/zOjDySH3XHCUno
cn1xOJAyZODBo47E+67R4jV1/gzbAkEAklJaspRPXP877NssM5nAZMU0/O/NGCZ+
3jPgDUno6WbJn5cqm8MqWhW1xGkImgRk+fkDBquiq4gPiT898jusgQJAd5Zrr6Q8
AO/0isr/3aa6O6NLQxISLKcPDk2NOccAfS/xOtfOz4sJYM3+Bs4Io9+dZGSDCA54
Lw03eHTNQghS0A=="));
			var provider = new RSACryptoServiceProvider();

			provider.ImportParameters(parser.ParseRSAPrivateKey());

			LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);

			var oAuthSettings = new OAuth1Settings
			{
				ClientId = "lInXLgx6HbF9FFq1ZQN8iSEnhzO3JVuf",
				RsaSha1Provider = provider,
				AuthorizeMode = AuthorizeModes.LocalhostServer,
				CloudServiceName = "Greenshot Jira",
			};
			_jiraApi = new JiraApi(TestJiraUri, oAuthSettings);
		}


		/// <summary>
		///     This will test Oauth with a LocalServer "code" receiver
		/// </summary>
		/// <returns>Task</returns>
		//[Fact]
		public async Task TestOauth2Request()
		{
			var user = await _jiraApi.WhoAmIAsync();
			Assert.NotNull(user);
		}
	}
}
