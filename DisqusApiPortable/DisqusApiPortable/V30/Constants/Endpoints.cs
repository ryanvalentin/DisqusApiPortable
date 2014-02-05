using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disqus.Api.V30.Constants
{
    public static class Endpoints
    {
        internal const string apiBase = "https://disqus.com/api/";
        internal const string apiVersion = "3.0/";
        internal const string resourceType = ".json";

        public static class Applications
        {
            private const string _category = "applications/";
            public const string ListUsage = apiBase + apiVersion + _category + "listUsage" + resourceType;
        }

        public static class Blacklists
        {
            private const string _category = "blacklists/";
            public const string Add = apiBase + apiVersion + _category + "add" + resourceType;
            public const string List = apiBase + apiVersion + _category + "list" + resourceType;
            public const string Remove = apiBase + apiVersion + _category + "remove" + resourceType;
        }

        public static class Forums
        {
            private const string _category = "forums/";
            public const string AddModerator = apiBase + apiVersion + _category + "addModerator" + resourceType;
            public const string Create = apiBase + apiVersion + _category + "create" + resourceType;
            public const string Details = apiBase + apiVersion + _category + "details" + resourceType;
            public const string Installed = apiBase + apiVersion + _category + "installed" + resourceType;
            public const string ListModerators = apiBase + apiVersion + _category + "listModerators" + resourceType;
            public const string ListMostActiveUsers = apiBase + apiVersion + _category + "listMostActiveUsers" + resourceType;
            public const string ListMostLikedUsers = apiBase + apiVersion + _category + "listMostLikedUsers" + resourceType;
            public const string ListPosts = apiBase + apiVersion + _category + "listPosts" + resourceType;
            public const string ListThreads = apiBase + apiVersion + _category + "listThreads" + resourceType;
            public const string ListUsers = apiBase + apiVersion + _category + "listUsers" + resourceType;
            public const string RemoveModerator = apiBase + apiVersion + _category + "removeModerator" + resourceType;
        }

        public static class Posts
        {
            private const string _category = "posts/";
            public const string Approve = apiBase + apiVersion + _category + "approve" + resourceType;
            public const string Create = apiBase + apiVersion + _category + "create" + resourceType;
            public const string Details = apiBase + apiVersion + _category + "details" + resourceType;
            public const string GetContext = apiBase + apiVersion + _category + "getContext" + resourceType;
            public const string List = apiBase + apiVersion + _category + "list" + resourceType;
            public const string ListPopular = apiBase + apiVersion + _category + "listPopular" + resourceType;
            public const string Remove = apiBase + apiVersion + _category + "remove" + resourceType;
            public const string Report = apiBase + apiVersion + _category + "report" + resourceType;
            public const string Restore = apiBase + apiVersion + _category + "restore" + resourceType;
            public const string Spam = apiBase + apiVersion + _category + "spam" + resourceType;
            public const string Update = apiBase + apiVersion + _category + "update" + resourceType;
            public const string Vote = apiBase + apiVersion + _category + "vote" + resourceType;
        }

        public static class Threads
        {
            private const string _category = "threads/";
            public const string Close = apiBase + apiVersion + _category + "close" + resourceType;
            public const string Create = apiBase + apiVersion + _category + "create" + resourceType;
            public const string Details = apiBase + apiVersion + _category + "details" + resourceType;
            public const string List = apiBase + apiVersion + _category + "list" + resourceType;
            public const string ListHot = apiBase + apiVersion + _category + "listHot" + resourceType;
            public const string ListPopular = apiBase + apiVersion + _category + "listPopular" + resourceType;
            public const string ListPosts = apiBase + apiVersion + _category + "listPosts" + resourceType;
            public const string Open = apiBase + apiVersion + _category + "open" + resourceType;
            public const string Remove = apiBase + apiVersion + _category + "remove" + resourceType;
            public const string Restore = apiBase + apiVersion + _category + "restore" + resourceType;
            public const string Set = apiBase + apiVersion + _category + "set" + resourceType;
            public const string Subscribe = apiBase + apiVersion + _category + "subscribe" + resourceType;
            public const string Unsubscribe = apiBase + apiVersion + _category + "unsubscribe" + resourceType;
            public const string Update = apiBase + apiVersion + _category + "update" + resourceType;
            public const string Vote = apiBase + apiVersion + _category + "vote" + resourceType;
        }

        public static class Users
        {
            private const string _category = "users/";
            public const string CheckUsername = apiBase + apiVersion + _category + "checkUsername" + resourceType;
            public const string Details = apiBase + apiVersion + _category + "details" + resourceType;
            public const string Follow = apiBase + apiVersion + _category + "follow" + resourceType;
            public const string ListActiveForums = apiBase + apiVersion + _category + "listActiveForums" + resourceType;
            public const string ListActiveThreads = apiBase + apiVersion + _category + "listActiveThreads" + resourceType;
            public const string ListActivity = apiBase + apiVersion + _category + "listActivity" + resourceType;
            public const string ListFollowers = apiBase + apiVersion + _category + "listFollowers" + resourceType;
            public const string ListFollowing = apiBase + apiVersion + _category + "listFollowing" + resourceType;
            public const string ListForums = apiBase + apiVersion + _category + "listForums" + resourceType;
            public const string ListMostActiveForums = apiBase + apiVersion + _category + "listMostActiveForums" + resourceType;
            public const string ListPosts = apiBase + apiVersion + _category + "listPosts" + resourceType;
            public const string Unfollow = apiBase + apiVersion + _category + "unfollow" + resourceType;
            public const string UpdateProfile = apiBase + apiVersion + _category + "updateProfile" + resourceType;
        }

        public static class Whitelists
        {
            private const string _category = "whitelists/";
            public const string Add = apiBase + apiVersion + _category + "add" + resourceType;
            public const string List = apiBase + apiVersion + _category + "list" + resourceType;
            public const string Remove = apiBase + apiVersion + _category + "remove" + resourceType;
        }
    }
}
