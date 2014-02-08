using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disqus.Api.V30;
using Disqus.Api.V30.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DisqusApiPortable.Tests
{
    [TestClass]
    public class ApiMethodTests
    {
        // **********************************
        // Test Data
        // TODO fill this out and un-comment
        // **********************************

        /*
        // TODO: Add your keys here
        // Note: All keys and access tokens used in conjunction should be part of the same API application
        string _validApiKey = "<YOUR_API_KEY>";
        string _validApiSecret = "<YOUR_API_SECRET>";
        string _validAccessToken = "<YOUR_ACCESS_TOKEN>";
        string _ownedForum = "<FORUM_YOU_OWN>";
        
        //
        // Targets for commenter functions (voting, reporting, etc.) 
        string _targetUserToFollow = "<USERNAME>";
        string _targetPost = "<COMMENT_ID>";
        string _targetThread = "<THREAD_ID>"; // Random thread used for GET requests of public data. Cannot be posted to.

        //
        // Targets for moderation functions (approving, closing, etc.)
        // Must belong to a forum the access token moderates
        string _targetUserToAddModerator = "<SOME_USER>"; // This can't be someone who is already a moderator
        string _targetModerationPost = "<COMMENT_ID>";
        string _targetModerationThread = "<THREAD_ID>"; // The forum this belongs to should allow guest comments
        */

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
            catch (AggregateException ex)
            {
                foreach (var e in ex.Flatten().InnerExceptions)
                {
                    if (e is DsqApiException)
                    {
                        DsqApiException dex = e as DsqApiException;

                        Assert.AreEqual(dex.Code, 5, "The code in the response should be 5, indicating invalid API key. Was: " + dex.Message);
                    }
                    else if (e is JsonReaderException)
                    {
                        JsonReaderException jex = e as JsonReaderException;

                        Assert.Fail(jex.Message + "; " + jex);
                    }
                    else
                    {
                        Assert.Fail(e.Message);
                    }
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

        #region Other tests

        public void Pagination_Cursor()
        {
            //
            // Get initial results

            //
            // Get next page and test that it is the next set of results

            //
            // Go back to the previous page
        }

        #endregion

        #region Individual endpoint tests

        [TestMethod]
        public void Forums_Add_Remove_Moderator()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey, _validAccessToken), new Uri("http://disqus.com/", UriKind.Absolute));

            //
            // Add a moderator and immediately remove them
            var add = client.AddModeratorToForumAsync("username:" + _targetUserToAddModerator, _ownedForum).Result;
            Assert.AreEqual(0, add.Code);

            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(add.Response));

            var remove = client.RemoveModeratorFromForumAsync(add.Response.Values.FirstOrDefault().ToString()).Result;
            Assert.AreEqual(0, remove.Code);
        }

        public void Forums_Create()
        {

        }

        [TestMethod]
        public void Forums_Details()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.GetForumDetailsAsync(_ownedForum).Result;
            Assert.AreEqual(0, response.Code);
        }

        [TestMethod]
        public void Forums_Installed()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey, _validAccessToken), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.GetIsForumInstalledAsync(_ownedForum).Result;
            Assert.AreEqual(0, response.Code);
        }

        [TestMethod]
        public void Forums_ListModerators()
        {
            //
            // Try authenticated as forum owner
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey, _validAccessToken), new Uri("http://disqus.com/", UriKind.Absolute));
            var forumModeratorsAuthenticated = client.ListForumModeratorsAsync(_ownedForum).Result;
            Assert.AreEqual(0, forumModeratorsAuthenticated.Code);

            //
            // Try as unauthenticated user
            client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            try
            {
                var unauthenticated = client.ListForumModeratorsAsync(_ownedForum).Result;
            }
            catch (Exception)
            {
                // Should fail
            }

            //
            // Try as authenticated, but another forum
            client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey, _validAccessToken), new Uri("http://disqus.com/", UriKind.Absolute));
            var forumModeratorsAuthenticatedOther = client.ListForumModeratorsAsync("disqus").Result;
            Assert.AreEqual(0, forumModeratorsAuthenticatedOther.Code);
        }

        [TestMethod]
        public void Forums_MostActiveUsers()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.ListMostActiveForumUsersAsync(_ownedForum).Result;
            Assert.AreEqual(0, response.Code);
        }

        [TestMethod]
        public void Forums_MostLikedUsers()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.ListMostLikedForumUsersAsync(_ownedForum).Result;
            Assert.AreEqual(0, response.Code);
        }

        [TestMethod]
        public void Forums_ListUsers()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.ListForumUsersAsync(_ownedForum).Result;
            Assert.AreEqual(0, response.Code);
        }

        public void Posts_Approve()
        {

        }

        /*
        [TestMethod]
        public void Posts_Create_Guest()
        {
            //
            // Test on forum that allows guest posts
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.CreatePostAsync(Utilities.GetRandomText(), _targetModerationThread, "Mr. Tester", "mrtester@gamdlfajs.com").Result;
            Assert.AreEqual(0, response.Code);

            //
            // Test replying to the new comment
            var replyResponse = client.CreatePostAsync(Utilities.GetRandomText(), _targetModerationThread, "Mr. Replyer", "mrreplyer@gamdlfajs.com", response.Response.Id).Result;
            Assert.AreEqual(0, replyResponse.Code);
        }*/

        [TestMethod]
        public void Posts_Create_Authenticated()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey, _validAccessToken), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.CreatePostAsync(Utilities.GetRandomText(), _targetModerationThread).Result;
            Assert.AreEqual(0, response.Code);

            //
            // Test replying to the new comment
            var replyResponse = client.CreatePostAsync(Utilities.GetRandomText(), _targetModerationThread, response.Response.Id).Result;
            Assert.AreEqual(0, replyResponse.Code);
        }

        [TestMethod]
        public void Posts_Details()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.GetPostDetailsAsync(_targetPost).Result;
            Assert.AreEqual(0, response.Code);
        }

        [TestMethod]
        public void Posts_Context()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.GetPostContextAsync(_targetPost).Result;
            Assert.AreEqual(0, response.Code);
        }

        [TestMethod]
        public void Posts_List()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.ListPostsAsync(new List<string>(), new List<string>(new string[] { _ownedForum })).Result;
            Assert.AreEqual(0, response.Code);
        }

        [TestMethod]
        public void Posts_ListPopular()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.ListPopularPostsAsync(new List<string>(), new List<string>(new string[] { _ownedForum })).Result;
            Assert.AreEqual(0, response.Code);
        }

        public void Posts_Remove()
        {

        }

        public void Posts_Report()
        {

        }

        public void Posts_Restore()
        {

        }

        public void Posts_Spam()
        {

        }

        public void Posts_Update()
        {

        }

        public void Posts_Vote()
        {
            // Test upvoting

            // Test downvoting

            // Test removing vote
        }

        public void Threads_Close()
        {

        }

        public void Threads_Create()
        {

        }

        [TestMethod]
        public void Threads_Details()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));

            var threadsDetailsById = client.GetThreadDetailsAsync(_targetThread).Result;
            Assert.AreEqual(0, threadsDetailsById.Code);

            var threadsDetailsByIdentifier = client.GetThreadDetailsAsync("unit-test-thread-2", "unittestsite1").Result;
            Assert.AreEqual(0, threadsDetailsByIdentifier.Code);

            var threadsDetailsByUrl = client.GetThreadDetailsAsync(new Uri("http://unittestsite1.com/new-thread-2/", UriKind.Absolute), "unittestsite1").Result;
            Assert.AreEqual(0, threadsDetailsByUrl.Code);

            //
            // Make sure all these threads are the same
            Assert.AreEqual(threadsDetailsById.Response.Id, threadsDetailsByIdentifier.Response.Id);
            Assert.AreEqual(threadsDetailsById.Response.Id, threadsDetailsByUrl.Response.Id);
        }

        [TestMethod]
        public void Threads_List()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.ListThreadsAsync(new List<string>(new string[] { _ownedForum }), new List<string>(), new List<string>(), new List<string>(), "", 1).Result;
            Assert.AreEqual(0, response.Code);
        }

        [TestMethod]
        public void Threads_ListHot()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.ListHotThreadsAsync(new List<string>(new string[] { _targetModerationThread }), new List<string>(), new List<string>(), 1).Result;
            Assert.AreEqual(0, response.Code);
        }

        [TestMethod]
        public void Threads_ListPopular()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.ListPopularThreadsAsync(_targetModerationThread, "7d", 1).Result;
            Assert.AreEqual(0, response.Code);
        }

        [TestMethod]
        public void Threads_ListPosts()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.ListThreadPostsAsync(_targetModerationThread, new List<string>(), "desc", "", 1).Result;
            Assert.AreEqual(0, response.Code);
        }

        public void Threads_Open()
        {

        }

        public void Threads_Remove()
        {

        }

        public void Threads_Restore()
        {

        }

        [TestMethod]
        public void Threads_Set()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.GetThreadSetAsync(new List<string>(new string[] { _targetModerationThread })).Result;
            Assert.AreEqual(0, response.Code);
        }

        public void Threads_Vote()
        {
            // Test favoriting

            // Test un-favoriting
        }

        public void Users_UpdateUsername()
        {

        }

        [TestMethod]
        public void Users_Details()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.GetUserDetailsAsync(_targetUserToFollow).Result;
            Assert.AreEqual(0, response.Code);
        }

        public void Users_Follow()
        {

        }

        public void Users_Unfollow()
        {

        }

        [TestMethod]
        public void Users_ListActiveForums()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.ListUsersActiveForumsAsync("", 1, "asc", _targetUserToFollow).Result;
            Assert.AreEqual(0, response.Code);
        }

        [TestMethod]
        public void Users_ListMostActiveForums()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey, _validAccessToken), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.ListUsersMostActiveForumsAsync().Result;
            Assert.AreEqual(0, response.Code);
        }

        [TestMethod]
        public void Users_ListActiveThreads()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.ListUsersActiveThreadsAsync(new List<string>(), new List<string>(), "", 1, _targetUserToFollow).Result;
            Assert.AreEqual(0, response.Code);
        }

        [TestMethod]
        public void Users_ListFollowing()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.ListUsersFollowingAsync("", 1, "asc", _targetUserToFollow).Result;
            Assert.AreEqual(0, response.Code);
        }

        [TestMethod]
        public void Users_ListFollowers()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.ListUsersFollowersAsync("", 1, "asc", _targetUserToFollow).Result;
            Assert.AreEqual(0, response.Code);
        }

        [TestMethod]
        public void Users_ListForums()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey), new Uri("http://disqus.com/", UriKind.Absolute));
            var response = client.ListUsersForumsAsync("", 1, "asc", _targetUserToFollow).Result;
            Assert.AreEqual(0, response.Code);
        }

        public void Users_UpdateProfile()
        {

        }

        [TestMethod]
        public void Whitelists_All()
        {
            DisqusApiClient client = new DisqusApiClient(new Disqus.Api.V30.Authentication.DsqAuth(_validApiKey, _validAccessToken), new Uri("http://disqus.com/", UriKind.Absolute));

            //
            // Add to whitelist
            var response = client.AddToWhitelistAsync(_ownedForum, "fake@example.com", _targetUserToAddModerator, "Test notes 1").Result;
            Assert.AreEqual(0, response.Code);

            //
            // List the whitelist
            var responseList = client.ListForumWhitelistAsync(_ownedForum, "", 1).Result;
            Assert.AreEqual(0, responseList.Code);

            //
            // Remove from whitelist
            var responseRemove = client.RemoveFromWhitelistAsync(_ownedForum, "fake@example.com", _targetUserToAddModerator).Result;
            Assert.AreEqual(0, responseRemove.Code);
        }

        #endregion
    }
}