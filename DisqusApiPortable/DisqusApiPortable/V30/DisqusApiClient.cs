using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disqus.Api.V30.Authentication;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Disqus.Api.V30
{
    public class DisqusApiClient : INotifyPropertyChanged
    {
        public DisqusApiClient(DsqAuth auth)
        {
            this.DisqusAuthentication = auth;


            _httpClient = new HttpClient();
        }

        public DsqAuth DisqusAuthentication { get; set; }

        private HttpClient _httpClient { get; set; }
    }
}
