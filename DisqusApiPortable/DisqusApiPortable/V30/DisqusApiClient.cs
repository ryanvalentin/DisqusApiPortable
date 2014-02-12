using Disqus.Api.V30.Authentication;
using Disqus.Api.V30.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Disqus.Api.V30
{
    public class DisqusApiClient
    {
        public DisqusApiClient(DsqAuth auth, Uri referrer)
        {
            this.DisqusAuthentication = auth;
            this.referrer = referrer;
            this.host = referrer.Host;
        }

        public DsqAuth DisqusAuthentication { get; set; }

        #region API Methods

        #region Applications endpoints

        // TODO

        #endregion

        #region Blacklists endpoints

        // TODO

        #endregion

        #region Forums endpoints

        /// <summary>
        /// Adds a moderator to a forum
        /// </summary>
        /// <param name="user">Looks up a user by ID. You may look up a user by username by prefixing with 'username:' query.</param>
        /// <param name="forum">Looks up a forum by ID (aka short name). Authenticated user must be a moderator on the selected forum, and have "admin" permission scope.</param>
        /// <returns>Object containing the ID of the moderator that was added. NOTE this is NOT the user ID</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<Dictionary<string, long?>>> AddModeratorToForumAsync(string user, string forum)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "user", user, true);
            arguments = PostArgument(arguments, "forum", forum, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<Dictionary<string, long?>>>(await PostDataStreamAsync(Constants.Endpoints.Forums.AddModerator, arguments));
        }

        /// <summary>
        /// Creates a new forum
        /// </summary>
        /// <param name="website">URL (defined by RFC 3986)</param>
        /// <param name="name">Display name of the forum</param>
        /// <param name="shortname">Unique shortname of the site</param>
        /// <returns>Newly created forum object, or an error if the shortname is taken</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqForum>> CreateForumAsync(Uri website, string name, string shortname)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "website", website.OriginalString, true);
            arguments = PostArgument(arguments, "name", name, true);
            arguments = PostArgument(arguments, "short_name", shortname, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqForum>>(await PostDataStreamAsync(Constants.Endpoints.Forums.Create, arguments));
        }

        /// <summary>
        /// Returns forum details
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <returns>An object containing the forum details</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqForum>> GetForumDetailsAsync(string forum)
        {
            string endpoint = Constants.Endpoints.Forums.Details
                + GetAuthentication()
                + GetArgument("forum", forum, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqForum>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns true if forum has one or more views
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <returns>True if at least one pageview, otherwise false</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<bool>> GetIsForumInstalledAsync(string forum)
        {
            string endpoint = Constants.Endpoints.Forums.Installed
                + GetAuthentication()
                + GetArgument("forum", forum, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<bool>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of all moderators on a forum
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <returns>Array of user accounts who are listed as a moderator</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListResponse<DsqUser>> ListForumModeratorsAsync(string forum)
        {
            string endpoint = Constants.Endpoints.Forums.ListModerators
                + GetAuthentication(true)
                + GetArgument("forum", forum, true);

            return DeserializeStreamToObjectAsync<DsqListResponse<DsqUser>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of users active within a forum ordered by most comments made
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Defaults to 25. Maximum value of 100</param>
        /// <param name="order">Defaults to "asc" Choices: desc</param>
        /// <returns>List containing most active users on a forum</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqUserForumActive>> ListMostActiveForumUsersAsync(string forum, string cursor = "", int limit = 25)
        {
            string endpoint = Constants.Endpoints.Forums.ListMostActiveUsers
                + GetAuthentication()
                + GetArgument("forum", forum, true)
                + GetArgument("cursor", cursor)
                + GetArgument("limit", ClampLimit(limit));

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqUserForumActive>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of users active within a forum ordered by most likes received
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Defaults to 25. Maximum value of 100</param>
        /// <param name="order">Defaults to "asc" Choices: desc</param>
        /// <returns>List containing most liked (upvoted) users on a forum</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqUser>> ListMostLikedForumUsersAsync(string forum, string cursor = "", int limit = 25)
        {
            string endpoint = Constants.Endpoints.Forums.ListMostLikedUsers
                + GetAuthentication()
                + GetArgument("forum", forum, true)
                + GetArgument("cursor", cursor)
                + GetArgument("limit", ClampLimit(limit));

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqUser>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of users active within a forum
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Defaults to 25. Maximum value of 100</param>
        /// <param name="order">Defaults to "asc" Choices: desc</param>
        /// <param name="since_id">The ID number of the user to start returning results from, relative to sort order</param>
        /// <returns>List of users who have participated on a forum, sorted by global ID</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqUser>> ListForumUsersAsync(string forum, string cursor = "", int limit = 25, string order = "asc", string since_id = "")
        {
            string endpoint = Constants.Endpoints.Forums.ListUsers
                + GetAuthentication()
                + GetArgument("forum", forum, true)
                + GetArgument("cursor", cursor)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("order", order)
                + GetArgument("since_id", since_id);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqUser>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Removes a moderator from a forum
        /// </summary>
        /// <param name="moderator_id">The unique moderator ID. NOTE this is NOT the user ID</param>
        /// <returns>Object confirming that the moderator ID is now null</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<Dictionary<string, long?>>> RemoveModeratorFromForumAsync(string moderator_id)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "moderator", moderator_id, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<Dictionary<string, long?>>>(await PostDataStreamAsync(Constants.Endpoints.Forums.RemoveModerator, arguments));
        }

        /// <summary>
        /// Follow a forum.
        /// </summary>
        /// <param name="target">Looks up a forum by ID (aka short name)</param>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task FollowForumAsync(string target)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "target", target, true);

            var response = await PostDataStreamAsync(Constants.Endpoints.Forums.Follow, arguments);
            
            //
            // Nothing in result, so just dispose if successful
            response.Dispose();
        }

        /// <summary>
        /// Unfollow a forum.
        /// </summary>
        /// <param name="target">Looks up a forum by ID (aka short name)</param>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task UnfollowForumAsync(string target)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "target", target, true);

            var response = await PostDataStreamAsync(Constants.Endpoints.Forums.Unfollow, arguments);

            //
            // Nothing in result, so just dispose if successful
            response.Dispose();
        }

        /// <summary>
        /// Returns a list of users following a forum.
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Defaults to 25. Maximum value of 100</param>
        /// <param name="order">Defaults to "asc" Choices: desc</param>
        /// <param name="since_id">The ID number of the user to start returning results from, relative to sort order</param>
        /// <returns>Returns a list of users following a forum.</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqUser>> ListForumFollowersAsync(string forum, string cursor = "", int limit = 25, string order = "asc", string since_id = "")
        {
            string endpoint = Constants.Endpoints.Forums.ListFollowers
                + GetAuthentication()
                + GetArgument("forum", forum, true)
                + GetArgument("cursor", cursor)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("order", order)
                + GetArgument("since_id", since_id);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqUser>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of topics for a forum.
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Defaults to 25. Maximum value of 100</param>
        /// <param name="order">Defaults to "asc" Choices: desc</param>
        /// <returns>Returns a list of topics for a forum.</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqTopic>> ListForumTopicsAsync(string forum, string cursor = "", int limit = 25, string order = "asc")
        {
            string endpoint = Constants.Endpoints.Forums.ListTopics
                + GetAuthentication()
                + GetArgument("forum", forum, true)
                + GetArgument("cursor", cursor)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("order", order);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqTopic>>(await GetDataStreamAsync(endpoint));
        }

        #endregion

        #region Posts endpoints

        /// <summary>
        /// Approves the requested post
        /// </summary>
        /// <param name="post_id">The single post (comment) id to approve. You must be a moderator on the selected post's forum.</param>
        /// <returns>List of post IDs approved</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<List<Dictionary<string, string>>>> ApprovePostAsync(string post_id)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "post", post_id, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<List<Dictionary<string, string>>>>(await PostDataStreamAsync(Constants.Endpoints.Posts.Approve, arguments));
        }

        /// <summary>
        /// Approves the requested posts
        /// </summary>
        /// <param name="post_ids">The post (comment) ids to approve. You must be a moderator on the selected post's forum.</param>
        /// <returns>List of post IDs approved</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<List<Dictionary<string, string>>>> ApprovePostAsync(List<string> post_ids)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);

            foreach (string id in post_ids)
            {
                arguments = PostArgument(arguments, "post", id, true);
            }

            return DeserializeStreamToObjectAsync<DsqObjectResponse<List<Dictionary<string, string>>>>(await PostDataStreamAsync(Constants.Endpoints.Posts.Approve, arguments));
        }

        /// <summary>
        /// Creates a post (comment) as an authenticated user
        /// </summary>
        /// <param name="message">The body of the post</param>
        /// <param name="thread_id">The thread to post the comment to</param>
        /// <param name="parent_id">The parent ID the new comment should be in reply to. Leave null if it's a parent comment</param>
        /// <returns>The newly-created post object</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqPostThreaded>> CreatePostAsync(string message, string thread_id, string parent_id = null)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "message", message, true);
            arguments = PostArgument(arguments, "thread", thread_id, true);
            arguments = PostArgument(arguments, "parent", parent_id);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqPostThreaded>>(await PostDataStreamAsync(Constants.Endpoints.Posts.Create, arguments));
        }

        /// <summary>
        /// Creates a post (comment) as a guest user
        /// </summary>
        /// <param name="message">The body of the post</param>
        /// <param name="thread_id">The thread to post the comment to</param>
        /// <param name="author_name">The display name of the commenter</param>
        /// <param name="author_email">The email address of the commenter (defined by RFC 5322)</param>
        /// <param name="parent_id">The parent ID the new comment should be in reply to. Leave null if it's a parent comment</param>
        /// <param name="author_url">Optional website URL belonging to the author (defined by RFC 3986)</param>
        /// <returns>The newly-created post object</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqPostThreaded>> CreatePostAsync(string message, string thread_id, string author_name, string author_email, string parent_id = null, Uri author_url = null)
        {
            //
            // Intercept authenticated requests
            if (!String.IsNullOrEmpty(DisqusAuthentication.AccessToken))
                throw new DsqApiException("Authenticated users can't create guest comments. Use the other CreatePostAsync overload to post authenticated comments", 17);

            List<KeyValuePair<string, string>> arguments = PostAuthentication();
            arguments = PostArgument(arguments, "message", message, true);
            arguments = PostArgument(arguments, "thread", thread_id, true);
            arguments = PostArgument(arguments, "parent", parent_id);
            arguments = PostArgument(arguments, "author_name", author_name, true);
            arguments = PostArgument(arguments, "author_email", author_email, true);

            if (author_url != null)
                arguments = PostArgument(arguments, "author_url", author_url.OriginalString, false);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqPostThreaded>>(await PostDataStreamAsync(Constants.Endpoints.Posts.Create, arguments));
        }

        /// <summary>
        /// Returns information about a post (comment)
        /// </summary>
        /// <param name="post_id">Looks up a post by ID</param>
        /// <returns>Details about a single comment</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqPostExpanded>> GetPostDetailsAsync(string post_id)
        {
            string endpoint = Constants.Endpoints.Posts.Details
                + GetAuthentication()
                + GetArgument("post", post_id, true)
                + GetArgument("related", "forum")
                + GetArgument("related", "thread");

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqPostExpanded>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns the hierarchal tree of a post (all parents).
        /// </summary>
        /// <param name="post_id">Looks up a post by ID</param>
        /// <param name="depth">The maximum number of parents to return. Minimum 1, maximum 10.</param>
        /// <returns>A list of all parent comments to a post, including the requested one</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListResponse<DsqPostExpanded>> GetPostContextAsync(string post_id, int depth = 10)
        {
            string endpoint = Constants.Endpoints.Posts.GetContext
                + GetAuthentication()
                + GetArgument("post", post_id, true)
                + GetArgument("related", "forum")
                + GetArgument("related", "thread")
                + GetArgument("depth", ClampLimit(depth, 1, 10));

            return DeserializeStreamToObjectAsync<DsqListResponse<DsqPostExpanded>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of posts ordered by the date created.
        /// </summary>
        /// <param name="thread_ids">Looks up a threads by ID</param>
        /// <param name="forums">Defaults to all forums you moderate</param>
        /// <param name="limit">Defaults to 25. Maximum value of 100</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <returns>List of posts ordered by the date created</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqPostExpanded>> ListPostsAsync(List<string> thread_ids, List<string> forums, int limit = 25, string cursor = "")
        {
            string endpoint = Constants.Endpoints.Posts.List
                + GetAuthentication()
                + GetArgument("related", "forum")
                + GetArgument("related", "thread")
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("cursor", cursor)
                + GetArgument("strict", "1");

            foreach(string id in thread_ids)
            {
                endpoint += GetArgument("thread", id);
            }

            foreach (string forum in forums)
            {
                endpoint += GetArgument("forum", forum);
            }

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqPostExpanded>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of posts ordered by the date created.
        /// </summary>
        /// <param name="thread_ids">Looks up a threads by ID</param>
        /// <param name="forums">Defaults to all forums you moderate</param>
        /// <param name="include">Choices: unapproved, approved, spam, deleted, flagged</param>
        /// <param name="limit">Defaults to 25. Maximum value of 100</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="order">Defaults to "desc". Choices: asc, desc</param>
        /// <param name="since">The date/time to start returning results. Relative to 'order' argument</param>
        /// <param name="query">Search term to look for an return in posts. WARNING: This may be very slow or time out frequently</param>
        /// <returns>List of posts ordered by the date created</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqPostExpanded>> ListPostsAsync(List<string> thread_ids, List<string> forums, List<string> include, int limit = 25, string cursor = "", string order = "desc", DateTime? since = null, string query = "")
        {
            string endpoint = Constants.Endpoints.Posts.List
                + GetAuthentication()
                + GetArgument("related", "forum")
                + GetArgument("related", "thread")
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("cursor", cursor)
                + GetArgument("order", order)
                + GetArgument("query", query)
                + GetArgument("strict", "1");

            foreach (string id in thread_ids)
            {
                endpoint += GetArgument("thread", id);
            }

            foreach (string forum in forums)
            {
                endpoint += GetArgument("forum", forum);
            }

            foreach (string i in include)
            {
                endpoint += GetArgument("include", i);
            }

            if (since.HasValue)
                endpoint += ConvertToTimestamp(since.Value);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqPostExpanded>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of posts ordered by the number of likes recently.
        /// </summary>
        /// <param name="thread_ids">Looks up a threads by ID</param>
        /// <param name="forums">Defaults to all forums you moderate</param>
        /// <param name="limit">Defaults to 25. Maximum value of 100</param>
        /// <param name="order">Choices: popular, best</param>
        /// <param name="interval">Choices: 1h, 6h, 12h, 1d, 3d, 7d, 30d, 60d, 90d</param>
        /// <returns>List of popular comments that match the argument requirements</returns>
        public async Task<DsqListResponse<DsqPostExpanded>> ListPopularPostsAsync(List<string> thread_ids, List<string> forums, int limit = 25, string order = "popular", string interval = "7d")
        {
            string endpoint = Constants.Endpoints.Posts.ListPopular
                + GetAuthentication()
                + GetArgument("related", "forum")
                + GetArgument("related", "thread")
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("interval", interval)
                + GetArgument("order", order)
                + GetArgument("strict", "1");

            foreach (string id in thread_ids)
            {
                endpoint += GetArgument("thread", id);
            }

            foreach (string forum in forums)
            {
                endpoint += GetArgument("forum", forum);
            }

            return DeserializeStreamToObjectAsync<DsqListResponse<DsqPostExpanded>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Removes the requested post
        /// </summary>
        /// <param name="post_ids">The post (comment) id to remove. You must be a moderator on the selected post's forum.</param>
        /// <returns>List of post IDs approved</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<List<Dictionary<string, string>>>> RemovePostAsync(string post_id)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "post", post_id, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<List<Dictionary<string, string>>>>(await PostDataStreamAsync(Constants.Endpoints.Posts.Remove, arguments));
        }

        /// <summary>
        /// Removes the requested posts
        /// </summary>
        /// <param name="post_ids">The post (comment) ids to remove. You must be a moderator on the selected post's forum.</param>
        /// <returns>List of post IDs approved</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<List<Dictionary<string, string>>>> RemovePostAsync(List<string> post_ids)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);

            foreach (string id in post_ids)
            {
                arguments = PostArgument(arguments, "post", id, true);
            }

            return DeserializeStreamToObjectAsync<DsqObjectResponse<List<Dictionary<string, string>>>>(await PostDataStreamAsync(Constants.Endpoints.Posts.Remove, arguments));
        }

        /// <summary>
        /// Reports (flags) the requested post. Only one flag is allowed per-comment, per-user
        /// </summary>
        /// <param name="post_ids">The post (comment) id to flag</param>
        /// <returns>The updated comment object</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqPostThreaded>> ReportPostAsync(string post_id)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication();
            arguments = PostArgument(arguments, "post", post_id, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqPostThreaded>>(await PostDataStreamAsync(Constants.Endpoints.Posts.Report, arguments));
        }

        /// <summary>
        /// Undeletes the requested post(s)
        /// </summary>
        /// <param name="post_ids">The post (comment) id to restpre. You must be a moderator on the selected post's forum.</param>
        /// <returns>List of post IDs approved</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<List<Dictionary<string, string>>>> RestorePostAsync(string post_id)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "post", post_id, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<List<Dictionary<string, string>>>>(await PostDataStreamAsync(Constants.Endpoints.Posts.Restore, arguments));
        }

        /// <summary>
        /// Undeletes the requested post(s)
        /// </summary>
        /// <param name="post_ids">The post (comment) ids to restore. You must be a moderator on the selected post's forum.</param>
        /// <returns>List of post IDs approved</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<List<Dictionary<string, string>>>> RestorePostAsync(List<string> post_ids)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);

            foreach (string id in post_ids)
            {
                arguments = PostArgument(arguments, "post", id, true);
            }

            return DeserializeStreamToObjectAsync<DsqObjectResponse<List<Dictionary<string, string>>>>(await PostDataStreamAsync(Constants.Endpoints.Posts.Restore, arguments));
        }

        /// <summary>
        /// Marks the requested post(s) as spam.
        /// </summary>
        /// <param name="post_ids">The post (comment) id to mark spam. You must be a moderator on the selected post's forum.</param>
        /// <returns>List of post IDs approved</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<List<Dictionary<string, string>>>> MarkPostSpamAsync(string post_id)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "post", post_id, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<List<Dictionary<string, string>>>>(await PostDataStreamAsync(Constants.Endpoints.Posts.Spam, arguments));
        }

        /// <summary>
        /// Marks the requested post(s) as spam
        /// </summary>
        /// <param name="post_ids">The post (comment) ids to mark spam. You must be a moderator on the selected post's forum.</param>
        /// <returns>List of post IDs approved</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<List<Dictionary<string, string>>>> MarkPostSpamAsync(List<string> post_ids)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);

            foreach (string id in post_ids)
            {
                arguments = PostArgument(arguments, "post", id, true);
            }

            return DeserializeStreamToObjectAsync<DsqObjectResponse<List<Dictionary<string, string>>>>(await PostDataStreamAsync(Constants.Endpoints.Posts.Spam, arguments));
        }

        /// <summary>
        /// Updates (edits) a post. You must be the author of the post or a moderator on the applicable forum.
        /// </summary>
        /// <param name="message">The new message to be displayed in the comment</param>
        /// <param name="post_id">Post ID to update</param>
        /// <returns>The updated comment object</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqPostThreaded>> UpdatePostAsync(string message, string post_id)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "message", message, true);
            arguments = PostArgument(arguments, "post", post_id, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqPostThreaded>>(await PostDataStreamAsync(Constants.Endpoints.Posts.Update, arguments));
        }

        /// <summary>
        /// Register an upvote on a post
        /// </summary>
        /// <param name="post_id">Looks up a post by ID</param>
        /// <returns>Object containing the new vote delta and the updated post</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqVoteObject>> UpvotePostAsync(string post_id)
        {
            return await VotePostAsync(post_id, "1");
        }

        /// <summary>
        /// Register a downvote on a post
        /// </summary>
        /// <param name="post_id">Looks up a post by ID</param>
        /// <returns>Object containing the new vote delta and the updated post</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqVoteObject>> DownvotePostAsync(string post_id)
        {
            return await VotePostAsync(post_id, "-1");
        }

        /// <summary>
        /// Removes (clears) any vote from user on a post
        /// </summary>
        /// <param name="post_id">Looks up a post by ID</param>
        /// <returns>Object containing the new vote delta and the updated post</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqVoteObject>> RemovePostVoteAsync(string post_id)
        {
            return await VotePostAsync(post_id, "0");
        }

        /// <summary>
        /// Private method to handle upvotes/downvotes/clear
        /// </summary>
        private async Task<DsqObjectResponse<DsqVoteObject>> VotePostAsync(string post_id, string vote)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication();
            arguments = PostArgument(arguments, "vote", vote, true);
            arguments = PostArgument(arguments, "post", post_id, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqVoteObject>>(await PostDataStreamAsync(Constants.Endpoints.Posts.Vote, arguments));
        }

        #endregion

        #region Topics endpoints

        /// <summary>
        /// Follow a topic.
        /// </summary>
        /// <param name="target">Looks up a topic by ID (slug)</param>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task FollowTopicAsync(string target)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "target", target, true);

            var response = await PostDataStreamAsync(Constants.Endpoints.Topics.Follow, arguments);

            //
            // Nothing in result, so just dispose if successful
            response.Dispose();
        }

        /// <summary>
        /// Unfollow a topic.
        /// </summary>
        /// <param name="target">Looks up a topic by ID (slug)</param>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task UnfollowTopicAsync(string target)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "target", target, true);

            var response = await PostDataStreamAsync(Constants.Endpoints.Topics.Unfollow, arguments);

            //
            // Nothing in result, so just dispose if successful
            response.Dispose();
        }

        /// <summary>
        /// Returns a list of all topics.
        /// </summary>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Defaults to 25. Maximum value of 100</param>
        /// <param name="order">Defaults to "asc" Choices: desc</param>
        /// <returns>Returns a list of all topics.</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqTopic>> ListTopicsAsync(string cursor = "", int limit = 25, string order = "asc")
        {
            string endpoint = Constants.Endpoints.Topics.List
                + GetAuthentication()
                + GetArgument("cursor", cursor)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("order", order);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqTopic>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of users following a topic.
        /// </summary>
        /// <param name="topic">Looks up a topic by ID (slug)</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Defaults to 25. Maximum value of 100</param>
        /// <param name="order">Defaults to "asc" Choices: desc</param>
        /// <param name="since_id">The ID number of the user to start returning results from, relative to sort order</param>
        /// <returns>Returns a list of users following a topic.</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqUser>> ListTopicFollowersAsync(string topic, string cursor = "", int limit = 25, string order = "asc", string since_id = "")
        {
            string endpoint = Constants.Endpoints.Topics.ListFollowers
                + GetAuthentication()
                + GetArgument("topic", topic, true)
                + GetArgument("cursor", cursor)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("order", order)
                + GetArgument("since_id", since_id);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqUser>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of forums within a topic.
        /// </summary>
        /// <param name="topic">Looks up a topic by ID (slug)</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Defaults to 25. Maximum value of 100</param>
        /// <param name="order">Defaults to "asc" Choices: desc</param>
        /// <returns>Returns a list of forums within a topic.</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqForum>> ListTopicForumsAsync(string topic, string cursor = "", int limit = 25, string order = "asc")
        {
            string endpoint = Constants.Endpoints.Topics.ListForums
                + GetAuthentication()
                + GetArgument("topic", topic, true)
                + GetArgument("cursor", cursor)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("order", order);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqForum>>(await GetDataStreamAsync(endpoint));
        }

        #endregion

        #region Threads endpoints

        /// <summary>
        /// Closes selected threads
        /// </summary>
        /// <param name="thread_ids">Looks up a thread by ID. You must be a moderator on the selected thread's forum.</param>
        /// <returns>A list of thread IDs that were closed</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListResponse<Dictionary<string, string>>> CloseThreadsAsync(List<string> thread_ids)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);

            foreach (var t in thread_ids)
            {
                arguments = PostArgument(arguments, "thread", t, true);
            }

            return DeserializeStreamToObjectAsync<DsqListResponse<Dictionary<string, string>>>(await PostDataStreamAsync(Constants.Endpoints.Threads.Close, arguments));
        }
        
        /// <summary>
        /// Creates a new thread
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="title">Title of the new thread</param>
        /// <param name="url">URL (defined by RFC 3986) Maximum length of 500</param>
        /// <param name="identifier">Unique identifier for the thread</param>
        /// <param name="content">Content of the article or post</param>
        /// <returns>The newly created thread object</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqThreadCollapsed>> CreateThreadAsync(string forum, string title, Uri url, string identifier = "", string content = "")
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "forum", forum, true);
            arguments = PostArgument(arguments, "title", title, true);
            arguments = PostArgument(arguments, "url", url.OriginalString, true);
            arguments = PostArgument(arguments, "identifier", identifier);
            arguments = PostArgument(arguments, "message", content);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqThreadCollapsed>>(await PostDataStreamAsync(Constants.Endpoints.Threads.Create, arguments));
        }

        /// <summary>
        /// Returns thread details.
        /// </summary>
        /// <param name="thread_id">Looks up a thread by ID</param>
        /// <returns>Object containing the thread details</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqThreadExpanded>> GetThreadDetailsAsync(string thread_id)
        {
            return await GetTDetailsAsync(thread_id);
        }

        /// <summary>
        /// Returns thread details.
        /// </summary>
        /// <param name="threadIdentifier">Looks up a thread by custom identifier of the thread (var disqus_identifier)</param>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <returns>Object containing the thread details</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqThreadExpanded>> GetThreadDetailsAsync(string threadIdentifier, string forum)
        {
            if (String.IsNullOrEmpty(forum))
                throw new DsqApiException("Invalid value for 'forum'. To look up a thread by custom identifier you must also pass a valid 'forum'", 2);

            return await GetTDetailsAsync("ident:" + threadIdentifier, forum);
        }

        /// <summary>
        /// Returns thread details.
        /// </summary>
        /// <param name="url">Looks up a thread by URL stored with the thread. NOTE this isn't necessarily the URL the thread is being loaded on</param>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <returns>Object containing the thread details</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqThreadExpanded>> GetThreadDetailsAsync(Uri url, string forum)
        {
            if (!url.IsAbsoluteUri)
                throw new DsqApiException("Invalid value for 'url', was: '" + url.OriginalString + "'", 2);

            if (String.IsNullOrEmpty(forum))
                throw new DsqApiException("Invalid value for 'forum'. To look up a thread by URL you must also pass a valid 'forum'", 2);

            return await GetTDetailsAsync("link:" + url.OriginalString, forum);
        }

        /// <summary>
        /// Private method to retrieve thread details
        /// </summary>
        private async Task<DsqObjectResponse<DsqThreadExpanded>> GetTDetailsAsync(string thread, string forum = "")
        {
            string endpoint = Constants.Endpoints.Threads.Details
                + GetAuthentication()
                + GetArgument("related", "forum")
                + GetArgument("related", "author")
                + GetArgument("thread", thread, true)
                + GetArgument("forum", forum);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqThreadExpanded>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of threads sorted by the date created
        /// </summary>
        /// <param name="forums">Looks up a forum by ID (aka short name)</param>
        /// <param name="thread_ids">Looks up a thread by ID</param>
        /// <param name="authors">Looks up a user by ID</param>
        /// <param name="include">Choices: open, closed, killed</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Maximum value of 100</param>
        /// <param name="since">The date/time to start returning results. Relative to 'order' argument</param>
        /// <param name="order">Defaults to "desc". Choices: asc, desc</param>
        /// <returns>List of threads that satisfy the arguments</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqThreadExpanded>> ListThreadsAsync(List<string> forums, List<string> thread_ids, List<string> authors, List<string> include, string cursor = "", int limit = 25, DateTime? since = null, string order = "desc")
        {
            string endpoint = Constants.Endpoints.Threads.List
                + GetAuthentication()
                + GetArgument("related", "forum")
                + GetArgument("related", "author")
                + GetArgument("cursor", cursor)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("order", order);

            if (since.HasValue)
                endpoint += ConvertToTimestamp(since.Value);

            foreach (var f in forums)
            {
                endpoint += GetArgument("forum", f);
            }

            foreach (var t in thread_ids)
            {
                endpoint += GetArgument("thread", t);
            }

            foreach (var a in authors)
            {
                endpoint += GetArgument("author", a);
            }

            foreach (var i in include)
            {
                endpoint += GetArgument("include", i);
            }

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqThreadExpanded>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of threads sorted by hotness (date and likes)
        /// </summary>
        /// <param name="forums">Looks up a forum by ID (aka short name)</param>
        /// <param name="authors">Looks up a user by ID</param>
        /// <param name="include">Choices: open, closed, killed</param>
        /// <param name="limit">Maximum value of 100</param>
        /// <returns>List of threads sorted by hotness (what has the most activity recently)</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListResponse<DsqThreadExpanded>> ListHotThreadsAsync(List<string> forums, List<string> authors, List<string> include, int limit = 25)
        {
            string endpoint = Constants.Endpoints.Threads.ListHot
                + GetAuthentication()
                + GetArgument("related", "forum")
                + GetArgument("related", "author")
                + GetArgument("limit", ClampLimit(limit));

            foreach (var f in forums)
            {
                endpoint += GetArgument("forum", f);
            }

            foreach (var a in authors)
            {
                endpoint += GetArgument("author", a);
            }

            foreach (var i in include)
            {
                endpoint += GetArgument("include", i);
            }

            return DeserializeStreamToObjectAsync<DsqListResponse<DsqThreadExpanded>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Gets the most popular threads within a given interval 
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="interval">Choices: 1h, 6h, 12h, 1d, 3d, 7d, 30d, 90d</param>
        /// <param name="limit">Maximum value of 100</param>
        /// <returns>A list of threads sorted by number of posts made since 'interval' and the top comment for each</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListResponse<DsqThreadTopPost>> ListPopularThreadsAsync(string forum, string interval = "7d", int limit = 25)
        {
            string endpoint = Constants.Endpoints.Threads.ListPopular
                + GetAuthentication()
                + GetArgument("related", "forum")
                + GetArgument("related", "author")
                + GetArgument("with_top_post", "1")
                + GetArgument("forum", forum)
                + GetArgument("interval", interval)
                + GetArgument("limit", ClampLimit(limit));

            return DeserializeStreamToObjectAsync<DsqListResponse<DsqThreadTopPost>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of posts within a thread.
        /// </summary>
        /// <param name="thread_id">Looks up a thread by ID</param>
        /// <param name="include">Choices: unapproved, approved, spam, deleted, flagged</param>
        /// <param name="order">Choices: asc, desc</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Maximum value of 100</param>
        /// <returns>List of comments with pagination that satisfies arguments</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqPostThreaded>> ListThreadPostsAsync(string thread_id, List<string> include, string order = "desc", string cursor = "", int limit = 25)
        {
            return await GetThreadPostsAsync(thread_id, include, "", order, cursor, limit);
        }

        /// <summary>
        /// Returns a list of posts within a thread.
        /// </summary>
        /// <param name="threadIdentifier">Looks up a thread by custom identifier (var disqus_identifier)</param>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="include">Choices: unapproved, approved, spam, deleted, flagged</param>
        /// <param name="order">Choices: asc, desc</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Maximum value of 100</param>
        /// <returns>List of comments with pagination that satisfies arguments</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqPostThreaded>> ListThreadPostsAsync(string threadIdentifier, string forum, List<string> include, string order = "desc", string cursor = "", int limit = 25)
        {
            if (String.IsNullOrEmpty(forum))
                throw new DsqApiException("Invalid value for 'forum'. To look up a thread by custom identifier you must also pass a valid 'forum'", 2);

            return await GetThreadPostsAsync("ident:" + threadIdentifier, include, forum, order, cursor, limit);
        }

        /// <summary>
        /// Returns a list of posts within a thread.
        /// </summary>
        /// <param name="url">Looks up a thread by URL stored with the thread. NOTE this isn't necessarily the URL the thread is being loaded on</param>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="include">Choices: unapproved, approved, spam, deleted, flagged</param>
        /// <param name="order">Choices: asc, desc</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Maximum value of 100</param>
        /// <returns>List of comments with pagination that satisfies arguments</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqPostThreaded>> ListThreadPostsAsync(Uri url, string forum, List<string> include, string order = "desc", string cursor = "", int limit = 25)
        {
            if (!url.IsAbsoluteUri)
                throw new DsqApiException("Invalid value for 'url', was: '" + url.OriginalString + "'", 2);

            if (String.IsNullOrEmpty(forum))
                throw new DsqApiException("Invalid value for 'forum'. To look up a thread by URL you must also pass a valid 'forum'", 2);

            return await GetThreadPostsAsync("link:" + url.OriginalString, include, forum, order, cursor, limit);
        }

        /// <summary>
        /// Private method to get thread posts
        /// </summary>
        private async Task<DsqListCursorResponse<DsqPostThreaded>> GetThreadPostsAsync(string thread, List<string> include, string forum, string order, string cursor, int limit)
        {
            string endpoint = Constants.Endpoints.Threads.ListPosts
                + GetAuthentication()
                + GetArgument("thread", thread)
                + GetArgument("forum", forum)
                + GetArgument("cursor", cursor)
                + GetArgument("limit", ClampLimit(limit));

            foreach (var i in include)
            {
                endpoint += GetArgument("include", i);
            }

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqPostThreaded>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Opens selected threads
        /// </summary>
        /// <param name="thread_ids">Looks up a thread by ID. You must be a moderator on the selected thread's forum.</param>
        /// <returns>A list of thread IDs that were opened</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListResponse<Dictionary<string, string>>> OpenThreadsAsync(List<string> thread_ids)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);

            foreach (var t in thread_ids)
            {
                arguments = PostArgument(arguments, "thread", t, true);
            }

            return DeserializeStreamToObjectAsync<DsqListResponse<Dictionary<string, string>>>(await PostDataStreamAsync(Constants.Endpoints.Threads.Open, arguments));
        }

        /// <summary>
        /// Removes a thread (sets its state to "killed"). NOTE this does NOT delete the thread
        /// </summary>
        /// <param name="thread_ids">Looks up a thread by ID</param>
        /// <returns>A list of thread IDs that were removed</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListResponse<Dictionary<string, string>>> RemoveThreadsAsync(List<string> thread_ids)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);

            foreach (var t in thread_ids)
            {
                arguments = PostArgument(arguments, "thread", t, true);
            }

            return DeserializeStreamToObjectAsync<DsqListResponse<Dictionary<string, string>>>(await PostDataStreamAsync(Constants.Endpoints.Threads.Remove, arguments));
        }

        /// <summary>
        /// Opens selected threads
        /// </summary>
        /// <param name="thread_ids">Looks up a thread by ID. You must be a moderator on the selected thread's forum.</param>
        /// <returns>A list of thread IDs that were opened</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListResponse<Dictionary<string, string>>> RestoreThreadsAsync(List<string> thread_ids)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);

            foreach (var t in thread_ids)
            {
                arguments = PostArgument(arguments, "thread", t, true);
            }

            return DeserializeStreamToObjectAsync<DsqListResponse<Dictionary<string, string>>>(await PostDataStreamAsync(Constants.Endpoints.Threads.Restore, arguments));
        }

        /// <summary>
        /// Returns an unsorted set of threads given a list of ids.
        /// </summary>
        /// <param name="thread_ids">Looks up a thread by ID</param>
        /// <returns>List of threads found in the order</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListResponse<DsqThreadExpanded>> GetThreadSetAsync(List<string> thread_ids)
        {
            string endpoint = Constants.Endpoints.Threads.Set
                + GetAuthentication()
                + GetArgument("related", "forum")
                + GetArgument("related", "author");

            foreach (string t in thread_ids)
            {
                endpoint += GetArgument("thread", t);
            }

            return DeserializeStreamToObjectAsync<DsqListResponse<DsqThreadExpanded>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns an unsorted set of threads given a list of ids.
        /// </summary>
        /// <param name="threads">Looks up a thread by ID</param>
        /// <param name="forum">Forum to get threads from</param>
        /// <returns>List of threads found in the order</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListResponse<DsqThreadExpanded>> GetThreadSetAsync(List<string> threads, string forum)
        {
            string endpoint = Constants.Endpoints.Threads.Set
                + GetAuthentication()
                + GetArgument("forum", forum)
                + GetArgument("related", "forum")
                + GetArgument("related", "author");

            foreach (string t in threads)
            {
                endpoint += GetArgument("thread", t);
            }

            return DeserializeStreamToObjectAsync<DsqListResponse<DsqThreadExpanded>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Updates information on a thread. You must be the author of the thread or a moderator on the applicable forum.
        /// </summary>
        /// <param name="thread_id">Looks up by Disqus thread ID</param>
        /// <param name="title">Maximum length of 200</param>
        /// <param name="message">Content of the article or post</param>
        /// <param name="identifier">Custom disqus_identifier. Maximum length of 300</param>
        /// <param name="author_id">You must be a moderator on the applicable forum to change a thread author. You may look up a user by username by prefixing with 'username:' query.</param>
        /// <param name="url">URL (defined by RFC 3986). Maximum length of 500</param>
        /// <returns>An object containing the newly updated thread</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqThreadCollapsed>> UpdateThreadAsync(string thread_id, string title = "", string message = "", string identifier = "", string author_id = "", Uri url = null)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "thread", thread_id, true);
            arguments = PostArgument(arguments, "title", title);
            arguments = PostArgument(arguments, "author", author_id);
            arguments = PostArgument(arguments, "identifier", identifier);
            arguments = PostArgument(arguments, "message", message);

            if (url != null && url.IsAbsoluteUri)
                arguments = PostArgument(arguments, "url", url.OriginalString);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqThreadCollapsed>>(await PostDataStreamAsync(Constants.Endpoints.Threads.Update, arguments));
        }

        /// <summary>
        /// Registers a favorite on a thread
        /// </summary>
        /// <param name="thread_id">Looks up a thread by ID</param>
        /// <returns>Object containing the updated thread</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqThreadCollapsed>> FavoriteThreadAsync(string thread_id)
        {
            return await VoteThreadAsync(thread_id, "1");
        }

        /// <summary>
        /// Unregisters a favorite on a thread
        /// </summary>
        /// <param name="thread_id">Looks up a thread by ID</param>
        /// <returns>Object containing the updated thread</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqThreadCollapsed>> UnfavoriteThreadAsync(string thread_id)
        {
            return await VoteThreadAsync(thread_id, "0");
        }

        /// <summary>
        /// Private method to register a vote on a thread
        /// </summary>
        private async Task<DsqObjectResponse<DsqThreadCollapsed>> VoteThreadAsync(string thread_id, string vote)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication();
            arguments = PostArgument(arguments, "thread", thread_id, true);
            arguments = PostArgument(arguments, "vote", vote);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqThreadCollapsed>>(await PostDataStreamAsync(Constants.Endpoints.Threads.Vote, arguments));
        }

        #endregion

        #region Users endpoints

        /// <summary>
        /// Updates username for the authenticated user, fails if username does not meet requirements, or is taken by another user.
        /// </summary>
        /// <param name="new_username">The Disqus username the user wants to change it to</param>
        /// <returns>The updated user object</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqUser>> UpdateUsernameAsync(string new_username)
        {
            if (new_username.Length < 3 || new_username.Length > 30)
                throw new DsqApiException("New username doesn't satisfy length requirements, must be 3 or more characters and up to 30. Was: " + new_username.Length.ToString(), 2);

            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "username", new_username, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqUser>>(await PostDataStreamAsync(Constants.Endpoints.Users.CheckUsername, arguments));
        }

        /// <summary>
        /// Returns details of a user. If user argument is blank, will get details about the authenticated user
        /// </summary>
        /// <param name="user">Looks up a user by ID. You may look up a user by username by prefixing with 'username:' query</param>
        /// <returns>Object containing extended details of a user</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqUserDetails>> GetUserDetailsAsync(string user = "")
        {
            string endpoint = Constants.Endpoints.Users.Details
                + GetAuthentication()
                + GetArgument("user", user);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqUserDetails>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Follows a target user
        /// </summary>
        /// <param name="target">The target user to follow. You may look up a user by username by prefixing with 'username:' query.</param>
        /// <returns></returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqUser>> FollowUserAsync(string target)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "target", target, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqUser>>(await PostDataStreamAsync(Constants.Endpoints.Users.Follow, arguments));
        }

        /// <summary>
        /// Unfollows a target user
        /// </summary>
        /// <param name="target">The target user to unfollow. You may look up a user by username by prefixing with 'username:' query.</param>
        /// <returns></returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqUser>> UnfollowUserAsync(string target)
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "target", target, true);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqUser>>(await PostDataStreamAsync(Constants.Endpoints.Users.Unfollow, arguments));
        }

        /// <summary>
        /// Returns a list of forums a user has been active on.
        /// </summary>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Maximum value of 100</param>
        /// <param name="order">Choices: asc, desc</param>
        /// <param name="user">Looks up a user by ID. You may look up a user by username by prefixing with 'username:' query.</param>
        /// <returns>A list of forums the user has been active on, sorted by when the forum was created</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqForum>> ListUsersActiveForumsAsync(string cursor = "", int limit = 25, string order = "asc", string user = "")
        {
            string endpoint = Constants.Endpoints.Users.ListActiveForums
                + GetAuthentication()
                + GetArgument("cursor", cursor)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("order", order)
                + GetArgument("user", user);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqForum>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of forums a user has been active on recenty, sorted by the user's activity.
        /// </summary>
        /// <param name="limit">Maximum value of 100</param>
        /// <param name="user">Looks up a user by ID. You may look up a user by username by prefixing with 'username:' query.</param>
        /// <returns>Non-paginated list of user's most active forums</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListResponse<DsqForum>> ListUsersMostActiveForumsAsync(int limit = 25, string user = "")
        {
            string endpoint = Constants.Endpoints.Users.ListMostActiveForums
                + GetAuthentication()
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("user", user);

            return DeserializeStreamToObjectAsync<DsqListResponse<DsqForum>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of threads a user has participated in sorted by last activity.
        /// </summary>
        /// <param name="forums">Looks up a forum by ID (aka short name)</param>
        /// <param name="include">Choices: open, closed, killed</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Maximum value of 100</param>
        /// <param name="user">Looks up a user by ID. You may look up a user by username by prefixing with 'username:' query.</param>
        /// <param name="order">Choices: asc, desc</param>
        /// <returns>List of threads the user has either commented on or favorited</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqThreadExpanded>> ListUsersActiveThreadsAsync(List<string> forums, List<string> include, string cursor = "", int limit = 25, string user = "", string order = "desc")
        {
            string endpoint = Constants.Endpoints.Users.ListActiveThreads
                + GetAuthentication()
                + GetArgument("related", "forum")
                + GetArgument("related", "author")
                + GetArgument("cursor", cursor)
                + GetArgument("order", order)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("user", user);

            foreach (var i in include)
            {
                endpoint += GetArgument("include", i);
            }

            foreach (var f in forums)
            {
                endpoint += GetArgument("forum", f);
            }

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqThreadExpanded>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of users a user is being followed by.
        /// </summary>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Maximum value of 100</param>
        /// <param name="order">Choices: asc, desc</param>
        /// <param name="user">Looks up a user by ID. You may look up a user by username by prefixing with 'username:' query.</param>
        /// <returns>Returns a list of users a user is being followed by.</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqUser>> ListUsersFollowersAsync(string cursor = "", int limit = 25, string order = "asc", string user = "")
        {
            string endpoint = Constants.Endpoints.Users.ListFollowers
                + GetAuthentication()
                + GetArgument("cursor", cursor)
                + GetArgument("order", order)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("user", user);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqUser>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of users a user is following.
        /// </summary>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Maximum value of 100</param>
        /// <param name="order">Choices: asc, desc</param>
        /// <param name="user">Looks up a user by ID. You may look up a user by username by prefixing with 'username:' query.</param>
        /// <returns>Returns a list of users a user is following.</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqUser>> ListUsersFollowingAsync(string cursor = "", int limit = 25, string order = "asc", string user = "")
        {
            string endpoint = Constants.Endpoints.Users.ListFollowing
                + GetAuthentication()
                + GetArgument("cursor", cursor)
                + GetArgument("order", order)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("user", user);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqUser>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of forums a user owns.
        /// </summary>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Maximum value of 100</param>
        /// <param name="order">Choices: asc, desc</param>
        /// <param name="user">Looks up a user by ID. You may look up a user by username by prefixing with 'username:' query.</param>
        /// <returns>Returns a list of forums a user owns.</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqForum>> ListUsersForumsAsync(string cursor = "", int limit = 25, string order = "asc", string user = "")
        {
            string endpoint = Constants.Endpoints.Users.ListForums
                + GetAuthentication()
                + GetArgument("cursor", cursor)
                + GetArgument("order", order)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("user", user);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqForum>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of posts made by the user.
        /// </summary>
        /// <param name="user">Looks up a user by ID. You may look up a user by username by prefixing with 'username:' query.</param>
        /// <param name="limit">Maximum value of 100</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="order">Choices: asc, desc</param>
        /// <returns>Returns a list of posts made by the user.</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqPostExpanded>> ListUsersPostsAsync(string user, int limit = 25, string cursor = "", string order = "desc")
        {
            string endpoint = Constants.Endpoints.Users.ListPosts
                + GetAuthentication()
                + GetArgument("cursor", cursor)
                + GetArgument("order", order)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("user", user)
                + GetArgument("related", "forum")
                + GetArgument("related", "thread");

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqPostExpanded>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Returns a list of posts made by the user.
        /// </summary>
        /// <param name="user">Looks up a user by ID. You may look up a user by username by prefixing with 'username:' query.</param>
        /// <param name="include">Choices: unapproved, approved, spam, deleted, flagged</param>
        /// <param name="limit">Maximum value of 100</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="order">Choices: asc, desc</param>
        /// <param name="since">The date/time to start returning results. Relative to 'order' argument</param>
        /// <returns>Returns a list of posts made by the user.</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqListCursorResponse<DsqPostExpanded>> ListUsersPostsAsync(string user, List<string> include, int limit = 25, string cursor = "", string order = "desc", DateTime? since = null)
        {
            string endpoint = Constants.Endpoints.Users.ListPosts
                + GetAuthentication()
                + GetArgument("cursor", cursor)
                + GetArgument("order", order)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("user", user)
                + GetArgument("related", "forum")
                + GetArgument("related", "thread");

            foreach (var i in include)
            {
                endpoint += GetArgument("include", i);
            }

            if (since.HasValue)
                endpoint += ConvertToTimestamp(since.Value);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqPostExpanded>>(await GetDataStreamAsync(endpoint));
        }

        /// <summary>
        /// Updates user profile. All fields are optional, but any field not present will be updated as blank.
        /// </summary>
        /// <param name="name">Non-unique display name for the user</param>
        /// <param name="about">Short bio about user</param>
        /// <param name="location">User's location. Maximum length of 255</param>
        /// <param name="url">URL (defined by RFC 3986)</param>
        /// <returns>The updated user object</returns>
        /// <exception cref="Disqus.Api.V30.DsqApiException">Error response returned from the Disqus API</exception>
        public async Task<DsqObjectResponse<DsqUser>> UpdateUserProfileAsync(string name, string about, string location, Uri url)
        {
            if (name.Length < 2 || name.Length > 30)
                throw new DsqApiException("New name doesn't satisfy length requirements, must be 2 or more characters and up to 30. Was: " + name.Length.ToString(), 2);

            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "name", name);
            arguments = PostArgument(arguments, "about", about);
            arguments = PostArgument(arguments, "location", location);
            arguments = PostArgument(arguments, "url", url.OriginalString);

            return DeserializeStreamToObjectAsync<DsqObjectResponse<DsqUser>>(await PostDataStreamAsync(Constants.Endpoints.Users.ListForums, arguments));
        }

        /// <summary>
        /// Returns a list of forums a user is following.
        /// </summary>
        /// <param name="user">Looks up a user by ID. You may look up a user by username by prefixing with 'username:' query.</param>
        /// <param name="limit">Maximum value of 100</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="order">Choices: asc, desc</param>
        /// <param name="since_id">The ID number of the user to start returning results from, relative to sort order</param>
        /// <returns>Returns a list of forums a user is following.</returns>
        public async Task<DsqListCursorResponse<DsqForum>> ListUserFollowingForumsAsync(string user, string cursor = "", int limit = 25, string order = "asc", string since_id = "")
        {
            string endpoint = Constants.Endpoints.Users.ListFollowingForums
                + GetAuthentication()
                + GetArgument("cursor", cursor)
                + GetArgument("order", order)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("user", user)
                + GetArgument("since_id", since_id);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqForum>>(await GetDataStreamAsync(endpoint));
        }

        public async Task<DsqListCursorResponse<DsqTopic>> ListUserFollowingTopicsAsync(string user, string cursor = "", int limit = 25, string order = "asc", string since_id = "")
        {
            string endpoint = Constants.Endpoints.Users.ListFollowingTopics
                + GetAuthentication()
                + GetArgument("cursor", cursor)
                + GetArgument("order", order)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("user", user)
                + GetArgument("since_id", since_id);

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqTopic>>(await GetDataStreamAsync(endpoint));
        }

        #endregion

        #region Whitelists endpoints

        /// <summary>
        /// Adds an entry to the whitelist. Authenticated user must have moderator privileges on the forum.
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="email">Email address (defined by RFC 5322)</param>
        /// <param name="notes">Maximum length of 50</param>
        /// <returns>List containing the objects added to the whitelist</returns>
        public async Task<DsqListResponse<DsqFilter>> AddToWhitelistAsync(string forum, string email, string notes = "")
        {
            return await PostToWhitelistAsync("add", forum, email, notes);
        }

        /// <summary>
        /// Adds an entry to the whitelist. Authenticated user must have moderator privileges on the forum.
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="email">Email address (defined by RFC 5322)</param>
        /// <param name="user">Looks up a user by ID. You may look up a user by username by prefixing with 'username:' query.</param>
        /// <param name="notes">Maximum length of 50</param>
        /// <returns>List containing the objects added to the whitelist</returns>
        public async Task<DsqListResponse<DsqFilter>> AddToWhitelistAsync(string forum, string email, string user, string notes = "")
        {
            return await PostToWhitelistAsync("add", forum, email, user, notes);
        }

        /// <summary>
        /// Removes an entry from the whitelist.
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="email">Email address (defined by RFC 5322)</param>
        /// <param name="user">Looks up a user by ID. You may look up a user by username by prefixing with 'username:' query.</param>
        /// <returns>A list of objects removed from the whitelist</returns>
        public async Task<DsqListResponse<DsqFilter>> RemoveFromWhitelistAsync(string forum, string email = "", string user = "")
        {
            return await PostToWhitelistAsync("remove", forum, email, user);
        }

        /// <summary>
        /// Private method to add or remove from whitelist
        /// </summary>
        private async Task<DsqListResponse<DsqFilter>> PostToWhitelistAsync(string method, string forum, string email = "", string user = "", string notes = "")
        {
            List<KeyValuePair<string, string>> arguments = PostAuthentication(true);
            arguments = PostArgument(arguments, "forum", forum, true);
            arguments = PostArgument(arguments, "email", email);
            arguments = PostArgument(arguments, "user", user);

            if (method == "add")
            {
                arguments = PostArgument(arguments, "notes", notes);
                return DeserializeStreamToObjectAsync<DsqListResponse<DsqFilter>>(await PostDataStreamAsync(Constants.Endpoints.Whitelists.Add, arguments));
            }
            else if (method == "remove")
            {
                return DeserializeStreamToObjectAsync<DsqListResponse<DsqFilter>>(await PostDataStreamAsync(Constants.Endpoints.Whitelists.Remove, arguments));
            }

            throw new ArgumentOutOfRangeException("method", "Method must either be 'add' or 'remove'");
        }

        /// <summary>
        /// Returns a list of all whitelist entries.
        /// </summary>
        /// <param name="forum">Looks up a forum by ID (aka short name)</param>
        /// <param name="cursor">The next/previous cursor ID (for pagination)</param>
        /// <param name="limit">Maximum value of 100</param>
        /// <param name="order">Choices: asc, desc</param>
        /// <param name="query">Search term to look up entries</param>
        /// <param name="since_id">ID to start showing entries from</param>
        /// <param name="include_email">Whether to include email whitelist entries</param>
        /// <param name="include_users">Whether to include user whitelist entries</param>
        /// <returns>Returns a list of all whitelist entries.</returns>
        public async Task<DsqListCursorResponse<DsqFilter>> ListForumWhitelistAsync(string forum, string cursor = "", int limit = 25, string order = "asc", string query = "", string since_id = "", bool include_email = true, bool include_users = true)
        {
            string endpoint = Constants.Endpoints.Whitelists.List
                + GetAuthentication(true)
                + GetArgument("forum", forum)
                + GetArgument("cursor", cursor)
                + GetArgument("order", order)
                + GetArgument("query", query)
                + GetArgument("limit", ClampLimit(limit))
                + GetArgument("since_id", since_id);

            if (include_email)
                endpoint += GetArgument("type", "email");

            if (include_users)
                endpoint += GetArgument("type", "user");

            return DeserializeStreamToObjectAsync<DsqListCursorResponse<DsqFilter>>(await GetDataStreamAsync(endpoint));
        }


        #endregion

        #endregion

        #region HTTP Client

        private HttpClient _httpClient { get; set; }
        private string _currentClientMethod { get; set; }
        protected Uri referrer { get; set; }
        protected string host { get; set; }

        protected async Task<StreamReader> GetDataStreamAsync(string endpoint)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                throw new DsqApiException("No internet connection was available");

            if (_httpClient == null || _currentClientMethod == "POST")
            {
                if (_httpClient != null)
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

        protected async Task<StreamReader> PostDataStreamAsync(string endpoint, List<KeyValuePair<string, string>> postArguments)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                throw new DsqApiException("No internet connection was available");

            if (_httpClient == null || _currentClientMethod == "GET")
            {
                if (_httpClient != null)
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

        private HttpClient BuildHttpClient(bool isPostRequest = false)
        {
            HttpClientHandler gzipHandler = new HttpClientHandler();

            if (gzipHandler.SupportsAutomaticDecompression)
                gzipHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            HttpClient client = new HttpClient(gzipHandler);

            //
            // Build headers
            client.DefaultRequestHeaders.Referrer = this.referrer;
            client.DefaultRequestHeaders.Host = this.host;
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

        protected T DeserializeStreamToObjectAsync<T>(StreamReader stream)
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

        #region Argument utilities

        public double ConvertToTimestamp(DateTime value)
        {
            //
            // Create Timespan by subtracting the value provided from the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            // Return the total seconds (which is a UNIX timestamp)
            return (double)span.TotalSeconds;
        }

        protected string GetAuthentication(bool authenticationRequired = false)
        {
            if (authenticationRequired && String.IsNullOrEmpty(DisqusAuthentication.AccessToken))
                throw new DsqApiException("You must be authenticated to perform this action", 4);

            string authQuery = "?api_key=" + DisqusAuthentication.ApiKey;

            authQuery += GetArgument("api_secret", DisqusAuthentication.ApiSecret);

            if (DisqusAuthentication.GetAuthType() == DsqAuthType.Disqus)
                authQuery += GetArgument("access_token", DisqusAuthentication.AccessToken);

            return authQuery;
        }

        private string[] _validIntervals = { "1h", "6h", "12h", "1d", "3d", "7d", "30d", "60d", "90d" };

        private string[] _validOrders = { "asc", "desc", "popular", "best" };

        private string[] _validInclude= { "approved", "unapproved", "flagged", "deleted", "spam", "open", "closed", "killed" };

        protected string GetArgument(string key, string value, bool required = false)
        {
            bool hasValue = !String.IsNullOrWhiteSpace(value);

            //
            // Validate the arguments to make sure they're populated if required, or a valid value
            if (required && !hasValue)
                throw new DsqApiException(String.Format("Argument '{0}' is required. Was: '{1}'", key, value), 2);

            if (hasValue)
            {
                if (key == "order" && !_validOrders.Any(value.Contains))
                    throw new DsqApiException("Invalid value for argument 'order'. Must be 'asc' or 'desc', or if requesting popular posts, must be 'popular' or 'best'", 2);

                if (key == "include" && !_validInclude.Any(value.Contains))
                    throw new DsqApiException("Invalid value for argument 'include'. If requesting posts, the following are valid: 'approved', 'unapproved', 'flagged', 'deleted' and 'spam'. If requesting threads, the following are valid: 'open', 'closed' and 'killed'", 2);

                if (key == "interval" && !_validIntervals.Any(value.Contains))
                    throw new DsqApiException("Invalid value for argument 'include'. Choices are '1h', '6h', '12h', '1d', '3d', '7d', '30d', '60d', '90d'");

                return String.Format("&{0}={1}", key, value);
            }

            return "";
        }

        protected List<KeyValuePair<string, string>> PostAuthentication(bool authenticationRequired = false)
        {
            if (authenticationRequired && String.IsNullOrEmpty(DisqusAuthentication.AccessToken))
                throw new DsqApiException("You must be authenticated to perform this action", 4);

            List<KeyValuePair<string, string>> arguments = new List<KeyValuePair<string, string>>();
            arguments.Add(new KeyValuePair<string, string>("api_key", DisqusAuthentication.ApiKey));

            if (!String.IsNullOrEmpty(DisqusAuthentication.ApiSecret))
                arguments.Add(new KeyValuePair<string, string>("api_secret", DisqusAuthentication.ApiSecret));

            if (DisqusAuthentication.GetAuthType() == DsqAuthType.Disqus)
                arguments.Add(new KeyValuePair<string, string>("access_token", DisqusAuthentication.AccessToken));

            return arguments;
        }

        protected List<KeyValuePair<string, string>> PostArgument(List<KeyValuePair<string, string>> existingList, string key, string value, bool required = false)
        {
            bool hasValue = !String.IsNullOrWhiteSpace(value);

            if (required && !hasValue)
                throw new DsqApiException(String.Format("Argument '{0}' is required. Was: '{1}'", key, value), 2);

            if (hasValue)
                existingList.Add(new KeyValuePair<string, string>(key, value));

            return existingList;
        }

        protected string ClampLimit(int limit, int min = 1, int max = 100)
        {
            if (limit <= min)
                return min.ToString();
            else if (limit >= max)
                return max.ToString();

            return limit.ToString();
        }

        #endregion
    }
}
