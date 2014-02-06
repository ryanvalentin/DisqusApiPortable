using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Disqus.Api.V30;
using Disqus.Api.V30.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DisqusApiPortableTests
{
    [TestClass]
    public class ApiMethodTests
    {
        //
        // TODO: Add you keys here
        // Note: All keys and access tokens used in conjunction should be part of the same API application
        //string _validApiKey = "VALID_API_KEY";
        //string _validApiSecret = "VALID_SECRET_KEY";
        //string _validAccessToken = "VALID_ACCESS_TOKEN";
        //string _ownedForum = "FORUM_YOU_OWN";
        

        #region Base functionality tests

        [TestMethod]
        public void Try_Valid_Request()
        {
            try
            {
                //
                // Create client
                DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));

                //
                // Make request
                var response = client.GetUserDetailsAsync("username:danielha").Result;

                //
                // Test response integrity
                Assert.AreEqual(response.Code, 0, "The code in the response should be 0, indicating success. Was: " + response.Code.ToString());
                Assert.IsInstanceOfType(response.Response, typeof(DsqUserDetails), "The response provided should be of type Disqus.Api.V30.Models.DsqUserDetails.");
                Assert.AreEqual(response.Response.Id, "3", "Daniel's user ID is '3', so the response user is someone else");
            }
            catch (AggregateException ex)
            {
                    foreach (var e in ex.Flatten().InnerExceptions)
                    {
                        if (e is DsqApiException)
                        {
                            DsqApiException dex = e as DsqApiException;

                            Assert.Fail("Code " + dex.Code + ": " + dex.Message);
                        }

                        if (e is JsonReaderException)
                        {
                            JsonReaderException jex = e as JsonReaderException;

                            Assert.Fail(jex.Message + jex);
                        }
                    }
            }
        }

        #endregion

        #region Authentication tests

        [TestMethod]
        public void Try_Invalid_ApiKey()
        {
            try
            {
                //
                // Create client
                string invalidApiKey = "sdaf3927fnwo3022";
                DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(invalidApiKey), new Uri("http://disqus.com/", UriKind.Absolute));

                //
                // Make request that should fail
                var response = client.ListPostsAsync(new List<string>(), new List<string>(new string[] { "cnn" })).Result;
            }
            catch (DsqApiException ex)
            {
                Assert.AreEqual(ex.Code, 5, "The code in the response should be 5, indicating invalid API key. Was: " + ex.Message.ToString() + ": ");
            }
            catch (AggregateException ex)
            {
                foreach (Exception e in ex.InnerExceptions)
                {
                    System.Diagnostics.Debug.WriteLine(e.GetType().FullName + "; " + e.Message);
                }
            }
        }

       /* [TestMethod]
        public void Try_Invalid_ApiSecret()
        {

        }

        [TestMethod]
        public void Try_Authenticated_Request_No_Access_Token()
        {

        }

        #endregion

        #region API requests

        [TestMethod]
        public void Create_Guest_Post()
        {

        }

        [TestMethod]
        public void Create_Authenticated_Post()
        {

        }*/

        #endregion
    }
}
