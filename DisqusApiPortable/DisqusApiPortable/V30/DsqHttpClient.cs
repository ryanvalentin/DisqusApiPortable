using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

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
            if (!NetworkInterface.GetIsNetworkAvailable())
                throw new DsqApiException("Not connected to the internet", null, FaultType.ClientNetworkConnection);

            // Remove urlencoded header if present
            if (this.DefaultRequestHeaders.Contains("ContentType"))
                this.DefaultRequestHeaders.Remove("ContentType");

            DsqApiException apiException = null;

            try
            {
                var response = await this.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    return DeserializeStreamToObjectAsync<T>(new StreamReader(await response.Content.ReadAsStreamAsync()));
                }
                else
                {
                    apiException = await GetExceptionFromResponse(response);
                }
            }
            catch (OperationCanceledException ex)
            {
                apiException = GetTaskCancelledException(ex);
            }
            catch (Exception ex)
            {
                apiException = new DsqApiException(
                    ex.Message,
                    null,
                    FaultType.Undetermined);
            }

            throw apiException;
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
            if (!NetworkInterface.GetIsNetworkAvailable())
                throw new DsqApiException("Not connected to the internet", null, FaultType.ClientNetworkConnection);

            // Add header if needed
            if (!this.DefaultRequestHeaders.Contains("ContentType"))
                this.DefaultRequestHeaders.Add("ContentType", "application/x-www-form-urlencoded");


            DsqApiException apiException = null;

            try
            {
                var response = await this.PostAsync(requestUri, new FormUrlEncodedContent(postArguments));

                if (response.IsSuccessStatusCode)
                {
                    return DeserializeStreamToObjectAsync<T>(new StreamReader(await response.Content.ReadAsStreamAsync()));
                }
                else
                {
                    apiException = await GetExceptionFromResponse(response);
                }
            }
            catch (OperationCanceledException ex)
            {
                apiException = GetTaskCancelledException(ex);
            }
            catch (Exception ex)
            {
                apiException = new DsqApiException(
                    ex.Message, 
                    null, 
                    FaultType.Undetermined);
            }

            throw apiException;
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

        private async Task<DsqApiException> GetExceptionFromResponse(HttpResponseMessage response)
        {
            DsqApiException apiException = null;

            try
            {
                string raw = await response.Content.ReadAsStringAsync();

                JObject json = JObject.Parse(raw);

                apiException = new DsqApiException(
                    (string)json["response"],
                    (int)json["code"],
                    GetFaultType((int)json["code"]));
            }
            catch (Exception ex)
            {
                apiException = new DsqApiException(
                    response.Content.ReadAsStringAsync().Result + ";" + ex.Message,
                    (int)response.StatusCode,
                    GetFaultType((int)response.StatusCode));
            }

            return apiException;
        }

        private DsqApiException GetTaskCancelledException(OperationCanceledException ex)
        {
            DsqApiException apiException = null;

            if (!ex.CancellationToken.IsCancellationRequested)
            {
                apiException = new DsqApiException(
                    ex.Message,
                    16,
                    FaultType.Timeout);
            }
            else
            {
                apiException = new DsqApiException(
                    ex.Message,
                    null,
                    FaultType.ClientRequest);
            }

            return apiException;
        }

        private FaultType GetFaultType(int errorOrStatusCode)
        {
            switch (errorOrStatusCode)
            {
                case 1:
                case 2:
                case 3:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 19:
                case 400:
                case 404:
                case 405:
                case 413:
                    return FaultType.ClientRequest;
                case 4:
                case 11:
                case 12:
                case 13:
                case 14:
                case 17:
                case 18:
                case 22:
                case 23:
                case 401:
                case 403:
                    return FaultType.InsufficientAccess;
                case 15:
                case 20:
                case 21:
                case 500:
                case 502:
                case 503:
                case 504:
                    return FaultType.Disqus;
                case 16:
                case 408:
                    return FaultType.Timeout;
                case 0: 
                case 200:
                default:
                    return FaultType.Undetermined;
            }
        }
    }
}
