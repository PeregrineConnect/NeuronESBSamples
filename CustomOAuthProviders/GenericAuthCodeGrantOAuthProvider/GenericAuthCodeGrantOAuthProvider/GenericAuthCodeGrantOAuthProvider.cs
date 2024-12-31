using Nemiro.OAuth;
using Neuron.NetX.Adapters;
using Neuron.NetX.OAuth;
using System.ComponentModel;
using Neuron.NetX;
using System.Collections.Generic;
using System;

namespace Neuron.NetX.Samples
{
	[DisplayName("Sample Generic Authorization Code Grant OAuth Provider")]
	public class GenericAuthCodeGrantOAuthProvider : OAuthProvider
	{
		private string clientId;
		private string clientSecret;
		private string authorizationUrl;
		private string tokenUrl;
		private string redirectUri;
		private string scope;
		private string grantType = Nemiro.OAuth.GrantType.AuthorizationCode;

		[Browsable(false)]
		[Bindable(false)]
		public string GrantType
		{
			get { return this.grantType; }
			set { this.grantType = value; }
		}

		[DisplayName("Client ID")]
		[Description("The identifier of the application that was provided by the OAuth provider.")]
		[PropertyOrder(3)]
		[OAuthCacheKey]
		public string ClientId
		{
			get { return this.clientId; }
			set
			{
				this.clientId = value;
			}
		}

		[DisplayName("Client Secret")]
		[PasswordPropertyText(true)]
		[Description("The shared secret value that is used to authenticate requests between Neuron ESB and the remote OAuth provider.")]
		[PropertyOrder(4)]
		[EncryptValue]
		[OAuthCacheKey]
		public string ClientSecret
		{
			get { return this.clientSecret; }
			set
			{
				this.clientSecret = value;
			}
		}

		[DisplayName("Authorization Url")]
		[Description("The authorization Url for the OAuth provider")]
		[PropertyOrder(5)]
		public string AuthorizationUrl
		{
			get { return this.authorizationUrl; }
			set
			{
				this.authorizationUrl = value;
			}
		}

		[DisplayName("Token Url")]
		[Description("The Url used to retrieve an access token.")]
		[PropertyOrder(6)]
		public string TokenUrl
		{
			get { return this.tokenUrl; }
			set
			{
				this.tokenUrl = value;
			}
		}

		[DisplayName("Redirect Url")]
		[Description("The redirect Url that was registered for the application with the OAuth provider.")]
		[PropertyOrder(7)]
		[OAuthCacheKey]
		public string RedirectUri
		{
			get { return this.redirectUri; }
			set
			{
				this.redirectUri = value;
			}
		}

		[DisplayName("Scope")]
		[Description("A list of scope values, separated by spaces. For more information about scopes, please refer to the OAuth provider's documentation.")]
		[PropertyOrder(9)]
		[OAuthCacheKey]
		public string Scope
		{
			get { return this.scope; }
			set
			{
				this.scope = value;
			}
		}

		public override OAuthBase GetClient()
		{
			return new GenericAuthCodeGrantOAuth2Client(this.authorizationUrl, this.tokenUrl, this.redirectUri, this.clientId, this.clientSecret, this.scope, this.GrantType);
		}

		public override AccessToken GetAndValidateAccessToken(OAuthBase client, ref List<NameValuePair> nameValuePairs)
		{
			bool success = false;

			try
			{
				var token = client.AccessTokenValue;
				if (!String.IsNullOrEmpty(token))
					success = true;

				if (success)
				{
					return token;
				}
				else
				{
					if (client.AccessToken.ContainsKey("error"))
					{
						string error = client.AccessToken["error"].ToString();
						string errorDesc = client.AccessToken.ContainsKey("error_description") ? client.AccessToken["error_description"].ToString() : "No error description provided";
						string errorUri = client.AccessToken.ContainsKey("error_uri") ? client.AccessToken["error_uri"].ToString() : "No error URI provided";
						throw new Exception(String.Format("Unable to obtain an access token from the OAuth Provider:{0}  Error: {1}{0}  Error Description: {2}{0}  Error URI: {3}", Environment.NewLine, error, errorDesc, errorUri));
					}
					else
					{
						throw new Exception("Unable to obtain an access token - unknown error");
					}

					return null;
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Unable to obtain an access token - {ex.Message}", ex);
			}

			return null;
		}
	}

	public class GenericAuthCodeGrantOAuth2Client : OAuth2Client
	{
		public override string ProviderName
		{
			get { return "Sample Generic Authorization Code Grant OAuth Provider"; }
		}

		public GenericAuthCodeGrantOAuth2Client(string authUri, string tokenUri, string redirectUri, string clientId, string clientSecret, string scope, string grantType)
			: base(authUri, tokenUri, clientId, clientSecret)
		{
			base.ReturnUrl = redirectUri;
			base.SupportRefreshToken = true;
			base.Scope = scope;
			this.GrantType = grantType;
		}
	}

	//public class GenericAuthCodeGrantLogin : LoginForm
	//{
	//	public GenericAuthCodeGrantLogin(OAuthBase client) :
	//		base(client, responseType: "code")
	//	{
	//	}
	//}
}
