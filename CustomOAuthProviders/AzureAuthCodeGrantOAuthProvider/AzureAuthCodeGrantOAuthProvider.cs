﻿using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;
using Neuron.NetX.OAuth;
using Nemiro.OAuth;
using Neuron.NetX.Adapters;
using System;
using System.Collections.Generic;

namespace Neuron.NetX.Samples
{
	[DisplayName("Sample Azure Authorization Code Grant OAuth Provider")]
	public class AzureAuthCodeGrantOAuthProvider : OAuthProvider
	{
		private string clientId;
		private string clientSecret;
		private string tenant;
		private string redirectUri;
		private string resource;
		private string grantType = Nemiro.OAuth.GrantType.AuthorizationCode;

		[Browsable(false)]
		[Bindable(false)]
		public string GrantType
		{
			get { return this.grantType; }
			set { this.grantType = value; }
		}

		[DisplayName("Client ID")]
		[Description("The Application Id assigned to your app when you registered it with Azure AD.")]
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
		[Description("The application secret that you created in the app registration portal for your app.")]
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

		[DisplayName("Tenant")]
		[Description("The {tenant} value in the path of the request can be used to control who can sign into the application.  The Tenant can be found by logging into the Azure Active Directory Portal as an administrator, click on Active Directory, click the directory you want to authenticate with, and the tenant will be displayed as part of the URL: https://manage.windowsazure.com/tenantname#Workspaces/ActiveDirectoryExtension/Directory/<TenantID>/directoryQuickStart")]
		[PropertyOrder(4)]
		[OAuthCacheKey]
		public string Tenant
		{
			get { return this.tenant; }

			set
			{
				this.tenant = value;
			}
		}

		[DisplayName("Redirect Uri")]
		[Description("The redirect_uri of your app, where authentication responses can be sent and received by your app. It must exactly match one of the redirect_uris you registered in the portal.")]
		[PropertyOrder(5)]
		[OAuthCacheKey]
		public string RedirectUri
		{
			get { return this.redirectUri; }

			set
			{
				this.redirectUri = value;
			}
		}

		[DisplayName("Resource")]
		[Description("The URL of the resource you want to access.")]
		[PropertyOrder(6)]
		[OAuthCacheKey]
		public string Resource
		{
			get { return this.resource; }

			set
			{
				this.resource = value;
			}
		}

		public override OAuthBase GetClient()
		{
			var authorizeUrl = string.Format("https://login.windows.net/{0}/oauth2/authorize", this.tenant);
			var accessTokenUrl = string.Format("https://login.windows.net/{0}/oauth2/token", this.tenant);
			return new AzureAuthCodeGrantOAuth2Client(authorizeUrl, accessTokenUrl, this.clientId, this.clientSecret, this.resource, this.redirectUri, this.GrantType);
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

						throw new Exception(String.Format("Unable to obtain an access token from Azure:{0}  Error: {1}{0}  Error Description: {2}{0}  Error URI: {3}", Environment.NewLine, error, errorDesc, errorUri));
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

	public class AzureAuthCodeGrantOAuth2Client : OAuth2Client
	{
		private string resource;
		private string redirectUri;

		private string responseMode = "query";

		public override string ProviderName
		{
			get { return "Sample Azure Authorization Code Grant OAuth Provider"; }
		}

		public AzureAuthCodeGrantOAuth2Client(string authorizeUrl, string accessTokenUrl, string clientId, string clientSecret, string resource, string redirectUri, string grantType)
			: base(authorizeUrl, accessTokenUrl, clientId, clientSecret)
		{
			this.resource = resource;
			this.redirectUri = redirectUri;
			base.ResponseType = ResponseType.Code;
			base.SupportRefreshToken = true;
			this.GrantType = grantType;
		}

		public override string AuthorizationUrl
		{
			get
			{
				var result = new StringBuilder();

				result.Append(this.AuthorizeUrl);
				result.Append(this.AuthorizeUrl.Contains("?") ? "&" : "?");
				result.AppendFormat("client_id={0}", OAuthUtility.UrlEncode(base.ApplicationId));
				result.AppendFormat("&response_type={0}", base.ResponseType);
				result.AppendFormat("&redirect_uri={0}", this.redirectUri);
				result.AppendFormat("&response_mode={0}", responseMode);
				result.AppendFormat("&resource={0}", this.resource);
				result.AppendFormat("&state={0}", OAuthUtility.UrlEncode(base.State));

				return result.ToString();
			}
		}

		protected override void GetAccessToken()
		{
			if (String.IsNullOrEmpty(this.AuthorizationCode))
			{
				throw new ArgumentNullException("AuthorizationCode");
			}

			var parameters = new NameValueCollection
			{
				{ "grant_type", this.GrantType },
				{ "client_id", this.ApplicationId },
				{ "code", this.AuthorizationCode },
				{ "redirect_uri", this.redirectUri },
				{ "resource", this.resource },
			};

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

		public override AccessToken RefreshToken(AccessToken accessToken = null)
		{
			var token = (OAuth2AccessToken)base.GetSpecifiedTokenOrCurrent(accessToken, refreshTokenRequired: true);

			var parameters = new NameValueCollection
			{
				{ "client_id", this.ApplicationId },
				{ "refresh_token", token.RefreshToken },
				{ "grant_type", GrantType.RefreshToken },
				{ "resource", this.resource }
                //{ "client_secret", this.ApplicationSecret },
            };

			var result = OAuthUtility.Post
			(
			  this.AccessTokenUrl,
			  parameters: parameters,
			  accessToken: token
			);

			return new OAuth2AccessToken(result);
		}
	}

	//public class AzureAuthCodeGrantLogin : LoginForm
	//{
	//    public AzureAuthCodeGrantLogin(OAuthBase client) :
	//        base(client, responseType: "code")
	//    {
	//    }
	//}
}
