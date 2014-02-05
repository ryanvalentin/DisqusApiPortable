using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disqus.Api.V30.Authentication;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Disqus.Api.V30.Models;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.NetworkInformation;
using Newtonsoft.Json;

namespace Disqus.Api.V30
{
    public class DisqusApiClient : INotifyPropertyChanged
    {
        public DisqusApiClient(DsqAuth auth, Uri referrer)
        {
            this.DisqusAuthentication = auth;
            this._referrer = referrer;
            this._host = referrer.Host;
            this._httpClient = new HttpClient();
        }

        public DsqAuth DisqusAuthentication { get; set; }

        private HttpClient _httpClient { get; set; }

        private string _currentClientMethod { get; set; }

        private Uri _referrer { get; set; }

        private string _host { get; set; }

        #region Applications endpoints

        // TODO

        #endregion

        #region Blacklists endpoints

        // TODO

        #endregion

        #region Forums endpoints

        /// <summary>
        /// Adds a moderator to a forum.
        /// </summary>
        /// <param name="user">Looks up a user by ID. You may look up a user by username using the 'username' query type.</param>
        /// <param name="forum">Looks up a forum by ID (aka short name). Authenticated user must be a moderator on the selected forum, and have "admin" permission scope.</param>
        /// <returns>Object containing the ID of the user that was added</returns>
        /// <exception cref="System.ArgumentException">A required argument was missing or invalid</exception>
        public async Task<DsqObjectResponse<Dictionary<string, int>>> AddModeratorToForumAsync(string user, string forum)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication();
            arguments = PostArgument(arguments, "user", user, true);
            arguments = PostArgument(arguments, "forum", forum, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<Dictionary<string, int>>>(await PostDataStreamAsync(Constants.Endpoints.Forums.Details, arguments));
        }

        /// <summary>
        /// Creates a new forum.
        /// </summary>
        /// <param name="website">URL (defined by RFC 3986)</param>
        /// <param name="name">Display name of the forum</param>
        /// <param name="shortname">Unique shortname of the site</param>
        /// <returns>Newly created forum object, or an error if the shortname is taken</returns>
        /// <exception cref="System.ArgumentException">A required argument was missing or invalid</exception>
        public async Task<DsqObjectResponse<DsqForum>> CreateForumAsync(Uri website, string name, string shortname)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication();
            arguments = PostArgument(arguments, "website", website.OriginalString, true);
            arguments = PostArgument(arguments, "name", name, true);
            arguments = PostArgument(arguments, "short_name", shortname, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqForum>>(await PostDataStreamAsync(Constants.Endpoints.Forums.Create, arguments));
        }

        /// <summary>
        /// Returns forum details.
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <returns>An object containing the forum details</returns>
        /// <exception cref="System.ArgumentException">A required argument was missing or invalid</exception>
        public async Task<DsqObjectResponse<DsqForum>> GetForumDetailsAsync(string forum)
        {
            string endpoint = Constants.Endpoints.Forums.Details
                + GetAuthentication()
                + GetArgument("forum", forum, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqForum>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns true if forum has one or more views.
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <returns>True if at least one pageview, otherwise false</returns>
        /// <exception cref="System.ArgumentException">A required argument was missing or invalid</exception>
        public async Task<DsqObjectResponse<bool>> GetIsForumInstalledAsync(string forum)
        {
            string endpoint = Constants.Endpoints.Forums.Installed
                + GetAuthentication()
                + GetArgument("forum", forum, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<bool>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of all moderators on a forum.
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <returns>Array of user accounts who are listed as a moderator</returns>
        /// <exception cref="System.ArgumentException">A required argument was missing or invalid</exception>
        public async Task<DsqListResponse<DsqUser>> ListForumModeratorsAsync(string forum)
        {
            string endpoint = Constants.Endpoints.Forums.ListModerators
                + GetAuthentication()
                + GetArgument("forum", forum, true);

            return DeserializeStreamToObjectAsync<DsqListResponse<DsqUser>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of users active within a forum ordered by most comments made.
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <returns>List containing most active users on a forum</returns>
        /// <exception cref="System.ArgumentException">A required argument was missing or invalid</exception>
        public async Task<DsqListCursorResponse<DsqUserForumActive>> ListMostActiveForumUsersAsync(string forum, string cursor = "")
        {
            string endpoint = Constants.Endpoints.Forums.ListMostActiveUsers
                + GetAuthentication()
                + GetArgument("forum", forum, true)
                + GetArgument("cursor", cursor, false);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqUserForumActive>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of users active within a forum ordered by most comments made.
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Defaults to 25. Maximum value of 100</param>
        /// <param name="order">Defaults to "asc" Choices: desc</param>
        /// <returns>List containing most active users on a forum</returns>
        /// <exception cref="System.ArgumentException">A required argument was missing or invalid</exception>
        public async Task<DsqListCursorResponse<DsqUserForumActive>> ListMostActiveForumUsersAsync(string forum, string cursor = "", int limit = 25, string order = "asc")
        {
            string endpoint = Constants.Endpoints.Forums.ListMostActiveUsers
                + GetAuthentication()
                + GetArgument("forum", forum, true)
                + GetArgument("cursor", cursor, false)
                + GetArgument("limit", ClampLimit(limit).ToString(), false)
                + GetArgument("order", order, false);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqUserForumActive>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of users active within a forum ordered by most likes received.
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <returns>List containing most liked (upvoted) users on a forum</returns>
        /// <exception cref="System.ArgumentException">A required argument was missing or invalid</exception>
        public async Task<DsqListCursorResponse<DsqUser>> ListMostLikedForumUsersAsync(string forum, string cursor = "")
        {
            string endpoint = Constants.Endpoints.Forums.ListMostLikedUsers
                + GetAuthentication()
                + GetArgument("forum", forum, true)
                + GetArgument("cursor", cursor, false);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqUser>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of users active within a forum ordered by most likes received.
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Defaults to 25. Maximum value of 100</param>
        /// <param name="order">Defaults to "asc" Choices: desc</param>
        /// <returns>List containing most liked (upvoted) users on a forum</returns>
        /// <exception cref="System.ArgumentException">A required argument was missing or invalid</exception>
        public async Task<DsqListCursorResponse<DsqUser>> ListMostLikedForumUsersAsync(string forum, string cursor = "", int limit = 25, string order = "asc")
        {
            string endpoint = Constants.Endpoints.Forums.ListMostLikedUsers
                + GetAuthentication()
                + GetArgument("forum", forum, true)
                + GetArgument("cursor", cursor, false)
                + GetArgument("limit", ClampLimit(limit).ToString(), false)
                + GetArgument("order", order, false);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqUser>>(await GetDataStreamAsync(endpoint));
        }

        #endregion

        #region Posts endpoints

        #endregion

        #region Threads endpoints

        #endregion

        #region Users endpoints

        #endregion

        #region Whitelists endpoints

        // TODO

        #endregion

        #region HTTP Client methods
        
        private async Task<StreamReader> GetDataStreamAsync(string endpoint)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                throw new DsqApiException("No internet connection was available");

            if (_httpClient == null || _currentClientMethod == "POST")
            {
                _httpClient.Dispose();
                _httpClient = BuildHttpClient();
            }

            var response = await _httpClient.GetAsync(new Uri(endpoint, UriKind.Absolute));

            if (response.IsSuccessStatusCode)
            {
                return new StreamReader(await response.Content.ReadAsStreamAsync());
            }
            else
            {
                string raw = await response.Content.ReadAsStringAsync();

                try
                {
                    JObject json = JObject.Parse(raw);

                    throw new DsqApiException((string)json["response"], (int)json["code"]);
                }
                catch (Exception ex)
                {
                    throw new DsqApiException("There was an error connecting to the Disqus servers: " + ex.Message + "; " + response.Content.ReadAsStringAsync());
                }
            }
        }

        private async Task<StreamReader> PostDataStreamAsync(string endpoint, List<KeyValuePair<string, string>> postArguments)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                throw new DsqApiException("No internet connection was available");

            if (_httpClient == null || _currentClientMethod == "GET")
            {
                _httpClient.Dispose();
                _httpClient = BuildHttpClient(true);
            }

            var response = await _httpClient.PostAsync(new Uri(endpoint, UriKind.Absolute), new FormUrlEncodedContent(postArguments));

            if (response.IsSuccessStatusCode)
            {
                return new StreamReader(await response.Content.ReadAsStreamAsync());
            }
            else
            {
                string raw = await response.Content.ReadAsStringAsync();

                try
                {
                    JObject json = JObject.Parse(raw);

                    throw new DsqApiException((string)json["response"], (int)json["code"]);
                }
                catch (Exception ex)
                {
                    throw new DsqApiException("There was an error connecting to the Disqus servers: " + ex.Message + "; " + response.Content.ReadAsStringAsync());
                }
            }
        }

        private HttpClient BuildHttpClient(bool isPostRequest = false)
        {
            HttpClientHandler gzipHandler = new HttpClientHandler();

            if (gzipHandler.SupportsAutomaticDecompression)
                gzipHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            HttpClient client = new HttpClient(gzipHandler);

            //
            // Build headers
            client.DefaultRequestHeaders.Referrer = this._referrer;
            client.DefaultRequestHeaders.Host = this._host;
            client.DefaultRequestHeaders.Add("User-Agent", String.Format("Disqus SDK for .NET"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (isPostRequest)
            {
                _currentClientMethod = "POST";
                client.DefaultRequestHeaders.Add("ContentType", "application/x-www-form-urlencoded");
            }
            else
            {
                _currentClientMethod = "GET";
            }

            return client;
        }

        private T DeserializeStreamToObjectAsync<T>(StreamReader stream)
        {
            using (JsonReader reader = new JsonTextReader(stream))
            {
                JsonSerializer serializer = new JsonSerializer();

                //
                // Return serialized JSON
                return serializer.Deserialize<T>(reader);
            }
        }

        #endregion

        private string GetAuthentication()
        {
            string authQuery = "?api_key=" + DisqusAuthentication.ApiKey;

            authQuery += GetArgument("api_secret", DisqusAuthentication.ApiSecret);

            if (DisqusAuthentication.GetAuthType() == DsqAuthType.Disqus)
                authQuery += GetArgument("access_token", DisqusAuthentication.AccessToken);

            return authQuery;
        }

        private string GetArgument(string key, string value, bool required = false)
        {
            bool hasValue = String.IsNullOrWhiteSpace(value);

            //
            // Validate the arguments to make sure they're populated if required, or a valid value
            if (required && !hasValue)
                throw new ArgumentException(String.Format("Argument {0} is required. Was: {1}", key, value));

            if (key == "order" && (value != "asc" || value != "desc"))
                throw new ArgumentException("Invalid value for argument 'order'. Must be 'asc' or 'desc'");

            //
            // Return final value if there is one
            if (hasValue)
                return String.Format("&{0}={1}", key, value);

            else
                return "";
        }

        private List<KeyValuePair<string, string>> PostAuthentication()
        {
            List<KeyValuePair<string, string>> arguments = new List<KeyValuePair<string, string>>();
            arguments.Add(new KeyValuePair<string, string>("api_key", DisqusAuthentication.ApiKey));

            if (!String.IsNullOrEmpty(DisqusAuthentication.ApiSecret))
                arguments.Add(new KeyValuePair<string, string>("api_secret", DisqusAuthentication.ApiSecret));

            if (DisqusAuthentication.GetAuthType() == DsqAuthType.Disqus)
                arguments.Add(new KeyValuePair<string, string>("access_token", DisqusAuthentication.AccessToken));

            return arguments;
        }

        private List<KeyValuePair<string, string>> PostArgument(List<KeyValuePair<string, string>> existingList, string key, string value, bool required = false)
        {
            bool hasValue = String.IsNullOrWhiteSpace(value);

            if (required && !hasValue)
                throw new ArgumentException(String.Format("Argument {0} is required. Was: {1}", key, value));

            if (hasValue)
                existingList.Add(new KeyValuePair<string, string>(key, value));

            return existingList;
        }

        #region Utilities

        private int ClampLimit(int limit)
        {
            if (limit <= 0)
                return 1;
            else if (limit >= 100)
                return 100;

            return limit;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
