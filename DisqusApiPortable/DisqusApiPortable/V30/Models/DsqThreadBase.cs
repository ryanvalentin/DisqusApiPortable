using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Disqus.Api.V30.Models
{
    /// <summary>
    /// Generic Disqus Thread class
    /// </summary>
    public class DsqThreadBase : IDsqThread, INotifyPropertyChanged
    {
        private string _feed;
        [JsonProperty(PropertyName = "feed")]
        public string Feed
        {
            get { return _feed; }
            set
            {
                if (value != _feed)
                {
                    _feed = value;
                    this.NotifyPropertyChanged("Feed");
                }
            }
        }

        private string _category;
        [JsonProperty(PropertyName = "category")]
        public string Category
        {
            get { return _category; }
            set
            {
                if (value != _category)
                {
                    _category = value;
                    this.NotifyPropertyChanged("Category");
                }
            }
        }

        private string _title;
        [JsonProperty(PropertyName = "title")]
        public string Title
        {
            get { return _title; }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    this.NotifyPropertyChanged("Title");
                }
            }
        }

        private string _cleanTitle;
        [JsonProperty(PropertyName = "clean_title")]
        public string CleanTitle
        {
            get { return _cleanTitle; }
            set
            {
                if (value != _cleanTitle)
                {
                    _cleanTitle = value;
                    this.NotifyPropertyChanged("CleanTitle");
                }
            }
        }

        private int _userScore;
        [JsonProperty(PropertyName = "userScore")]
        public int UserScore
        {
            get { return _userScore; }
            set
            {
                if (value != _userScore)
                {
                    _userScore = value;
                    this.NotifyPropertyChanged("UserScore");
                }
            }
        }

        private bool _canPost;
        [JsonProperty(PropertyName = "canPost")]
        public bool CanPost
        {
            get { return _canPost; }
            set
            {
                if (value != _canPost)
                {
                    _canPost = value;
                    this.NotifyPropertyChanged("CanPost");
                }
            }
        }

        private bool _canModerate;
        [JsonProperty(PropertyName = "canModerate")]
        public bool CanModerate
        {
            get { return _canModerate; }
            set
            {
                if (value != _canModerate)
                {
                    _canModerate = value;
                    this.NotifyPropertyChanged("CanModerate");
                }
            }
        }

        [JsonProperty(PropertyName = "identifiers")]
        public List<string> Identifiers { get; set; }

        private int _dislikes;
        [JsonProperty(PropertyName = "dislikes")]
        public int Dislikes
        {
            get { return _dislikes; }
            set
            {
                if (value != _dislikes)
                {
                    _dislikes = value;
                    this.NotifyPropertyChanged("Dislikes");
                }
            }
        }

        private DateTime _createdAt;
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set
            {
                if (value != _createdAt)
                {
                    _createdAt = value;
                    this.NotifyPropertyChanged("CreatedAt");
                }
            }
        }

        private string _slug;
        [JsonProperty(PropertyName = "slug")]
        public string Slug
        {
            get { return _slug; }
            set
            {
                if (value != _slug)
                {
                    _slug = value;
                    this.NotifyPropertyChanged("Slug");
                }
            }
        }

        private bool _isClosed;
        [JsonProperty(PropertyName = "isClosed")]
        public bool IsClosed
        {
            get { return _isClosed; }
            set
            {
                if (value != _isClosed)
                {
                    _isClosed = value;
                    this.NotifyPropertyChanged("IsClosed");
                }
            }
        }

        private int _posts;
        [JsonProperty(PropertyName = "posts")]
        public int Posts
        {
            get { return _posts; }
            set
            {
                if (value != _posts)
                {
                    _posts = value;
                    this.NotifyPropertyChanged("Posts");
                }
            }
        }

        private bool _userSubscription;
        [JsonProperty(PropertyName = "userSubscription")]
        public bool UserSubscription
        {
            get { return _userSubscription; }
            set
            {
                if (value != _userSubscription)
                {
                    _userSubscription = value;
                    this.NotifyPropertyChanged("UserSubscription");
                }
            }
        }

        private string _link;
        [JsonProperty(PropertyName = "link")]
        public string Link
        {
            get { return _link; }
            set
            {
                if (value != _link)
                {
                    _link = value;
                    this.NotifyPropertyChanged("Link");
                }
            }
        }

        private int _likes;
        [JsonProperty(PropertyName = "likes")]
        public int Likes
        {
            get { return _likes; }
            set
            {
                if (value != _likes)
                {
                    _likes = value;
                    this.NotifyPropertyChanged("Likes");
                }
            }
        }

        private string _message;
        [JsonProperty(PropertyName = "message")]
        public string Message
        {
            get { return _message; }
            set
            {
                if (value != _message)
                {
                    _message = value;
                    this.NotifyPropertyChanged("Message");
                }
            }
        }

        private string _id;
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    this.NotifyPropertyChanged("Id");
                }
            }
        }

        private bool _isDeleted;
        [JsonProperty(PropertyName = "isDeleted")]
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set
            {
                if (value != _isDeleted)
                {
                    _isDeleted = value;
                    this.NotifyPropertyChanged("IsDeleted");
                }
            }
        }

        private DsqPostThreaded _highlightedPost;
        [JsonProperty(PropertyName = "highlightedPost")]
        public DsqPostThreaded HighlightedPost
        {
            get { return _highlightedPost; }
            set
            {
                if (value != _highlightedPost)
                {
                    _highlightedPost = value;
                    this.NotifyPropertyChanged("HighlightedPost");
                }
            }
        }

        #region Json-ignored properties

        #region JSON-ignored

        private string _thumbnailUrl;
        [JsonIgnore]
        public string ThumbnailUrl
        {
            get { return _thumbnailUrl; }
            set
            {
                if (value != _thumbnailUrl)
                {
                    _thumbnailUrl = value;

                    if (_thumbnailUrl.StartsWith("//"))
                        _thumbnailUrl = "http:" + _thumbnailUrl;

                    this.NotifyPropertyChanged("ThumbnailUrl");
                }
            }
        }

        [JsonIgnore]
        public long ThumbnailSize { get; set; }

        #endregion

        [JsonIgnore]
        public string ForumId { get; set; }

        [JsonIgnore]
        public string AuthorId { get; set; }

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

        #region Methods

        #region Overrides

        public override string ToString()
        {
            return this.Id;
        }

        #endregion

        /// <summary>
        /// Converts the object into a timeline key
        /// </summary>
        /// <returns>Timeline key, e.g. 'auth.User?id=xxxx'</returns>
        public string ToTimelineKey()
        {
            return "forums.Thread?id=" + this.Id;
        }

        #endregion
    }
}
