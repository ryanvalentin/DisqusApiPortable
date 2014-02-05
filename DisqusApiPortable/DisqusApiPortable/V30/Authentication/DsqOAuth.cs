using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace Disqus.Api.V30.Authentication
{
    public class DsqOAuth
    {
        public DsqOAuth(string apiKey, Uri callbackUrl, Scopes scopes)
        {
            this._apiKey = apiKey;
            this._callbackUrl = Uri.EscapeUriString(callbackUrl.OriginalString);
            this._scope = GetScope(scopes);
        }

        private string _apiKey { get; set; }
        private string _scope { get; set; }
        private string _callbackUrl { get; set; }

        private const string _codeAuthorizationUrl = "https://disqus.com/api/oauth/2.0/authorize/?client_id={0}&scope={1}&response_type=code&redirect_uri={2}";

        public string GetAuthorizationUrl()
        {
            return String.Format(_codeAuthorizationUrl, this._apiKey, this._scope, this._callbackUrl);
        }

        public async Task<DsqAuth> RequestAccessTokenAsync(string code, string apiSecret)
        {
            return new DsqAuth("");
        }

        private string GetScope(Scopes scope)
        {
            switch (scope)
            {
                case Scopes.ReadWrite:
                    return "read,write";
                case Scopes.Moderator:
                    return "read,write,admin";
                default:
                    return "read";
            }
        }
    }
}
