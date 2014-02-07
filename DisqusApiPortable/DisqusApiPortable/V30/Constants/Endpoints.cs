namespace Disqus.Api.V30.Constants
{
    public static class Endpoints
    {
        private const string _apiBase = "https://disqus.com/api/";
        private const string _apiVersion = "3.0/";
        private const string _resourceType = ".json";

        public static class Applications
        {
            private const string _category = "applications/";
            public const string ListUsage = _apiBase + _apiVersion + _category + "listUsage" + _resourceType;
        }

        public static class Blacklists
        {
            private const string _category = "blacklists/";
            public const string Add = _apiBase + _apiVersion + _category + "add" + _resourceType;
            public const string List = _apiBase + _apiVersion + _category + "list" + _resourceType;
            public const string Remove = _apiBase + _apiVersion + _category + "remove" + _resourceType;
        }

        public static class Forums
        {
            private const string _category = "forums/";
            public const string AddModerator = _apiBase + _apiVersion + _category + "addModerator" + _resourceType;
            public const string Create = _apiBase + _apiVersion + _category + "create" + _resourceType;
            public const string Details = _apiBase + _apiVersion + _category + "details" + _resourceType;
            public const string Installed = _apiBase + _apiVersion + _category + "installed" + _resourceType;
            public const string ListModerators = _apiBase + _apiVersion + _category + "listModerators" + _resourceType;
            public const string ListMostActiveUsers = _apiBase + _apiVersion + _category + "listMostActiveUsers" + _resourceType;
            public const string ListMostLikedUsers = _apiBase + _apiVersion + _category + "listMostLikedUsers" + _resourceType;
            public const string ListPosts = _apiBase + _apiVersion + _category + "listPosts" + _resourceType;
            public const string ListThreads = _apiBase + _apiVersion + _category + "listThreads" + _resourceType;
            public const string ListUsers = _apiBase + _apiVersion + _category + "listUsers" + _resourceType;
            public const string RemoveModerator = _apiBase + _apiVersion + _category + "removeModerator" + _resourceType;
        }

        public static class Posts
        {
            private const string _category = "posts/";
            public const string Approve = _apiBase + _apiVersion + _category + "approve" + _resourceType;
            public const string Create = _apiBase + _apiVersion + _category + "create" + _resourceType;
            public const string Details = _apiBase + _apiVersion + _category + "details" + _resourceType;
            public const string GetContext = _apiBase + _apiVersion + _category + "getContext" + _resourceType;
            public const string List = _apiBase + _apiVersion + _category + "list" + _resourceType;
            public const string ListPopular = _apiBase + _apiVersion + _category + "listPopular" + _resourceType;
            public const string Remove = _apiBase + _apiVersion + _category + "remove" + _resourceType;
            public const string Report = _apiBase + _apiVersion + _category + "report" + _resourceType;
            public const string Restore = _apiBase + _apiVersion + _category + "restore" + _resourceType;
            public const string Spam = _apiBase + _apiVersion + _category + "spam" + _resourceType;
            public const string Update = _apiBase + _apiVersion + _category + "update" + _resourceType;
            public const string Vote = _apiBase + _apiVersion + _category + "vote" + _resourceType;
        }

        public static class Threads
        {
            private const string _category = "threads/";
            public const string Close = _apiBase + _apiVersion + _category + "close" + _resourceType;
            public const string Create = _apiBase + _apiVersion + _category + "create" + _resourceType;
            public const string Details = _apiBase + _apiVersion + _category + "details" + _resourceType;
            public const string List = _apiBase + _apiVersion + _category + "list" + _resourceType;
            public const string ListHot = _apiBase + _apiVersion + _category + "listHot" + _resourceType;
            public const string ListPopular = _apiBase + _apiVersion + _category + "listPopular" + _resourceType;
            public const string ListPosts = _apiBase + _apiVersion + _category + "listPosts" + _resourceType;
            public const string Open = _apiBase + _apiVersion + _category + "open" + _resourceType;
            public const string Remove = _apiBase + _apiVersion + _category + "remove" + _resourceType;
            public const string Restore = _apiBase + _apiVersion + _category + "restore" + _resourceType;
            public const string Set = _apiBase + _apiVersion + _category + "set" + _resourceType;
            public const string Subscribe = _apiBase + _apiVersion + _category + "subscribe" + _resourceType;
            public const string Unsubscribe = _apiBase + _apiVersion + _category + "unsubscribe" + _resourceType;
            public const string Update = _apiBase + _apiVersion + _category + "update" + _resourceType;
            public const string Vote = _apiBase + _apiVersion + _category + "vote" + _resourceType;
        }

        public static class Users
        {
            private const string _category = "users/";
            public const string CheckUsername = _apiBase + _apiVersion + _category + "checkUsername" + _resourceType;
            public const string Details = _apiBase + _apiVersion + _category + "details" + _resourceType;
            public const string Follow = _apiBase + _apiVersion + _category + "follow" + _resourceType;
            public const string ListActiveForums = _apiBase + _apiVersion + _category + "listActiveForums" + _resourceType;
            public const string ListActiveThreads = _apiBase + _apiVersion + _category + "listActiveThreads" + _resourceType;
            public const string ListActivity = _apiBase + _apiVersion + _category + "listActivity" + _resourceType;
            public const string ListFollowers = _apiBase + _apiVersion + _category + "listFollowers" + _resourceType;
            public const string ListFollowing = _apiBase + _apiVersion + _category + "listFollowing" + _resourceType;
            public const string ListForums = _apiBase + _apiVersion + _category + "listForums" + _resourceType;
            public const string ListMostActiveForums = _apiBase + _apiVersion + _category + "listMostActiveForums" + _resourceType;
            public const string ListPosts = _apiBase + _apiVersion + _category + "listPosts" + _resourceType;
            public const string Unfollow = _apiBase + _apiVersion + _category + "unfollow" + _resourceType;
            public const string UpdateProfile = _apiBase + _apiVersion + _category + "updateProfile" + _resourceType;
        }

        public static class Whitelists
        {
            private const string _category = "whitelists/";
            public const string Add = _apiBase + _apiVersion + _category + "add" + _resourceType;
            public const string List = _apiBase + _apiVersion + _category + "list" + _resourceType;
            public const string Remove = _apiBase + _apiVersion + _category + "remove" + _resourceType;
        }
    }
}
