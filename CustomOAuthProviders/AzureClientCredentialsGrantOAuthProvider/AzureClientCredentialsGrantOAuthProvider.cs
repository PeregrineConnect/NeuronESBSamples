using System;
using System.ComponentModel;
using System.Collections.Specialized;
using Nemiro.OAuth;
using Neuron.NetX.OAuth;
using Neuron.NetX.Adapters;

namespace Neuron.NetX.Samples
{
    [DisplayName("Sample Azure Client Credentials Grant OAuth Provider")]
    public class AzureClientCredentialsOAuthProvider : OAuthProvider
    {
        private string clientId;
        private string clientSecret;
        private string tenant;
        private string resource;
		private string grantType = Nemiro.OAuth.GrantType.ClientCredentials;

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
            return new AzureClientCredentialsGrantOAuth2Client(authorizeUrl, accessTokenUrl, this.clientId, this.clientSecret, this.resource, this.GrantType);
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

    public class AzureClientCredentialsGrantOAuth2Client : OAuth2Client
    {
        private string resource;

        public override string ProviderName
        {
            get { return "Sample Azure Client Credentials Grant OAuth Provider"; }
        }

        public AzureClientCredentialsGrantOAuth2Client(string authorizeUrl, string accessTokenUrl, string clientId, string clientSecret, string resource, string grantType)
            : base(authorizeUrl, accessTokenUrl, clientId, clientSecret)
        {
            this.resource = resource;
            base.SupportRefreshToken = false;
			this.GrantType = grantType;
		}

		protected override void GetAccessToken()
        {
            var parameters = new NameValueCollection
            {
                { "grant_type", this.GrantType },
                { "client_id", this.ApplicationId },
                { "client_secret", this.ApplicationSecret },
                { "resource", this.resource },
            };

            var result = OAuthUtility.Post
            (
              endpoint: this.AccessTokenUrl,
              parameters: parameters
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
