using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disqus.Api.V30.Authentication;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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

        #region Applications endpoints

        public async Task<string> ListUsageAsync(int days = 30)
        {
        }

        #endregion

        #region Blacklists endpoints

        #endregion

        #region Forums endpoints

        #endregion

        #region Posts endpoints

        #endregion

        #region Threads endpoints

        #endregion

        #region Users endpoints

        #endregion

        #region Whitelists endpoints

        #endregion

        #region HTTP Client methods
        
        private async Task GetDataAsync()
        {

        }

        private async Task PostDataAsync()
        {

        }

        #endregion
    }
}
