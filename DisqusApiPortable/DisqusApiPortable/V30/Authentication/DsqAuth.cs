using System;

namespace Disqus.Api.V30.Authentication
{
    public class DsqAuth
    {
        public DsqAuth(string apiKey, string accessToken = "")
        {
            this.ApiKey = apiKey;
            this.AccessToken = accessToken;
        }

        public DsqAuth(string apiKey, string secretKey, string remoteAuth = "")
        {
            this.ApiKey = apiKey;
            this.ApiSecret = secretKey;
            this.RemoteAuth = remoteAuth;
        }

        public DsqAuthType GetAuthType()
        {
            if (!String.IsNullOrEmpty(AccessToken))
                return DsqAuthType.Disqus;

            if (!String.IsNullOrEmpty(RemoteAuth))
                return DsqAuthType.SSO;

            return DsqAuthType.None;
        }

        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string AccessToken { get; set; }
        public string RemoteAuth { get; set; }
    }
}
