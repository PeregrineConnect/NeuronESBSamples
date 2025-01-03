﻿using Nemiro.OAuth;
using Neuron.NetX.OAuth;
using Neuron.NetX;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Collections.Generic;
using Neuron.NetX.Adapters;

namespace Neuron.NetX.Samples
{
	[DisplayName("Sample Generic Client Credentials Grant OAuth Provider")]
	public class GenericClientCredentialsOAuthProvider : OAuthProvider
	{
		private string clientId;
		private string clientSecret;
		private string tokenUrl;
		private string scope;
		private string grantType = Nemiro.OAuth.GrantType.ClientCredentials;

		[Browsable(false)]
		[Bindable(false)]
		public string GrantType
		{
			get { return this.grantType; }
			set { this.grantType = value; }
		}

		[DisplayName("Client ID")]
		[Description("The Application Id assigned to your app.")]
		[PropertyOrder(2)]
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
		[Description("The application secret assigned to your app.")]
		[PropertyOrder(3)]
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

		[DisplayName("Token Url")]
		[Description("The Url used to retrieve an access token.")]
		[PropertyOrder(4)]
		public string TokenUrl
		{
			get { return this.tokenUrl; }
			set
			{
				this.tokenUrl = value;
			}
		}

		[DisplayName("Scope")]
		[Description("A list of scope values, separated by spaces. For more information about scopes, please refer to the OAuth provider's documentation.")]
		[PropertyOrder(5)]
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
			return new GenericClientCredentialsGrantOAuth2Client(tokenUrl, this.clientId, this.clientSecret, this.scope, this.GrantType);
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
					return client.AccessToken;
				}
				else
				{
					if (client.AccessToken.ContainsKey("error"))
					{
						string error = client.AccessToken["error"].ToString();
						string errorDesc = client.AccessToken.ContainsKey("error_description") ? client.AccessToken["error_description"].ToString() : "No error description provided";
						string errorUri = client.AccessToken.ContainsKey("error_uri") ? client.AccessToken["error_uri"].ToString() : "No error URI provided";
						throw new Exception(String.Format("Unable to obtain an access token from the OAuth provider:{0}  Error: {1}{0}  Error Description: {2}{0}  Error URI: {3}", Environment.NewLine, error, errorDesc, errorUri));
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

	public class GenericClientCredentialsGrantOAuth2Client : OAuth2Client
	{
		public override string ProviderName
		{
			get { return "Sample Generic Client Credentials Grant OAuth Provider"; }
		}

		public GenericClientCredentialsGrantOAuth2Client(string tokenUrl, string clientId, string clientSecret, string scope, string grantType)
			: base(tokenUrl, tokenUrl, clientId, clientSecret)
		{
			base.Scope = scope;
			base.SupportRefreshToken = false;
			this.GrantType = grantType;
		}

		protected override void GetAccessToken()
		{
			var parameters = new NameValueCollection
			{
				{ "grant_type", this.GrantType },
				{ "client_id", this.ApplicationId },
				{ "client_secret", this.ApplicationSecret }
			};

			if (base.Scope != null)
				parameters.Add("scope", base.Scope);

			var result = OAuthUtility.Post
			(
			  endpoint: this.AccessTokenUrl,
			  parameters: parameters,
			  authorization: new HttpAuthorization(AuthorizationType.Basic, OAuthUtility.ToBase64String("{0}:{1}", this.ApplicationId, this.ApplicationSecret))
			);

			if (result.ContainsKey("error"))
			{
				this.AccessToken = new OAuth2AccessToken(new ErrorResult(result));
			}
			else
			{
				this.AccessToken = new OAuth2AccessToken(result);
			}
		}
	}
}
