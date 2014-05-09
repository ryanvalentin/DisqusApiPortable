using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Disqus.Api.V30
{
    public class DsqHttpClient : HttpClient
    {
        public static HttpClientHandler GetHandler()
        {
            HttpClientHandler gzipHandler = new HttpClientHandler();

            if (gzipHandler.SupportsAutomaticDecompression)
                gzipHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            return gzipHandler;
        }

        public DsqHttpClient(HttpMessageHandler handler, Uri referrer) : base(handler)
        {
            this.DefaultRequestHeaders.Referrer = referrer;
            this.DefaultRequestHeaders.Host = referrer.Host;
            this.DefaultRequestHeaders.Add("User-Agent", String.Format("Disqus SDK for .NET"));
            this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Gets data from Disqus API
        /// </summary>
        /// <typeparam name="T">The object type to return</typeparam>
        /// <param name="requestUri"></param>
        /// <returns>Deserialized object from response</returns>
        public async Task<T> GetDsqDataAsync<T>(string requestUri)
        {
            // Remove urlencoded header if present
            if (this.DefaultRequestHeaders.Contains("ContentType"))
                this.DefaultRequestHeaders.Remove("ContentType");

            try
            {
                var response = await this.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    return DeserializeStreamToObjectAsync<T>(new StreamReader(await response.Content.ReadAsStreamAsync()));
                }
                else
                {
                    try
                    {
                        string raw = await response.Content.ReadAsStringAsync();

                        JObject json = JObject.Parse(raw);

                        throw new DsqApiException((string)json["response"], (int)json["code"]);
                    }
                    catch (Exception ex)
                    {
                        throw new DsqApiException(ex.Message + "; " + response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DsqApiException(ex.Message, null);
            }
        }

        /// <summary>
        /// Posts data to Disqus API
        /// </summary>
        /// <typeparam name="T">The object type to return</typeparam>
        /// <param name="requestUri"></param>
        /// <param name="content"></param>
        /// <returns>Deserialized object from response</returns>
        public async Task<T> PostDsqDataAsync<T>(string requestUri, List<KeyValuePair<string, string>> postArguments)
        {
            // Add header if needed
            if (!this.DefaultRequestHeaders.Contains("ContentType"))
                this.DefaultRequestHeaders.Add("ContentType", "application/x-www-form-urlencoded");

            try
            {
                var response = await this.PostAsync(requestUri, new FormUrlEncodedContent(postArguments));

                if (response.IsSuccessStatusCode)
                {
                    return DeserializeStreamToObjectAsync<T>(new StreamReader(await response.Content.ReadAsStreamAsync()));
                }
                else
                {
                    try
                    {
                        string raw = await response.Content.ReadAsStringAsync();

                        JObject json = JObject.Parse(raw);

                        throw new DsqApiException((string)json["response"], (int)json["code"]);
                    }
                    catch (Exception ex)
                    {
                        throw new DsqApiException(ex.Message + "; " + response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DsqApiException(ex.Message, null);
            }
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
    }
}
