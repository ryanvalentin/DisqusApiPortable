using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace Disqus.Api.V30.Models
{
    /// <summary>
    /// Base Disqus user class which doesn't include everything in users/details
    /// </summary>
    public class DsqUser : INotifyPropertyChanged, IDsqUser
    {
        private string _username;
        [JsonProperty(PropertyName = "username")]
        public string Username
        {
            get { return _username; }
            set
            {
                if (value != _username)
                {
                    _username = value;
                    this.NotifyPropertyChanged("Username");
                }
            }
        }

        private string _about;
        [JsonProperty(PropertyName = "about")]
        public string About
        {
            get { return _about; }
            set
            {
                if (value != _about)
                {
                    _about = value;
                    this.NotifyPropertyChanged("About");
                }
            }
        }

        private string _name;
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }

        private string _url;
        [JsonProperty(PropertyName = "url")]
        public string Url
        {
            get { return _url; }
            set
            {
                if (value != _url)
                {
                    _url = value;
                    this.NotifyPropertyChanged("Url");
                }
            }
        }

        private bool _isAnonymous;
        [JsonProperty(PropertyName = "isAnonymous")]
        public bool IsAnonymous
        {
            get { return _isAnonymous; }
            set
            {
                if (value != _isAnonymous)
                {
                    _isAnonymous = value;
                    this.NotifyPropertyChanged("IsAnonymous");
                }
            }
        }

        private bool _isFollowing;
        [JsonProperty(PropertyName = "isFollowing")]
        public bool IsFollowing
        {
            get { return _isFollowing; }
            set
            {
                if (value != _isFollowing)
                {
                    _isFollowing = value;
                    this.NotifyPropertyChanged("IsFollowing");
                }
            }
        }

        private bool _isFollowedBy;
        [JsonProperty(PropertyName = "isFollowedBy")]
        public bool IsFollowedBy
        {
            get { return _isFollowedBy; }
            set
            {
                if (value != _isFollowedBy)
                {
                    _isFollowedBy = value;
                    this.NotifyPropertyChanged("IsFollowedBy");
                }
            }
        }

        private string _profileUrl;
        [JsonProperty(PropertyName = "profileUrl")]
        public string ProfileUrl
        {
            get { return _profileUrl; }
            set
            {
                if (value != _profileUrl)
                {
                    _profileUrl = value;
                    this.NotifyPropertyChanged("ProfileUrl");
                }
            }
        }

        private double _reputation;
        [JsonProperty(PropertyName = "reputation")]
        public double Reputation
        {
            get { return _reputation; }
            set
            {
                if (value != _reputation)
                {
                    _reputation = value;
                    this.NotifyPropertyChanged("Reputation");
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

        private string _location;
        [JsonProperty(PropertyName = "location")]
        public string Location
        {
            get { return _location; }
            set
            {
                if (value != _location)
                {
                    _location = value;
                    this.NotifyPropertyChanged("Location");
                }
            }
        }

        private bool _isPrivate;
        [JsonProperty(PropertyName = "isPrivate")]
        public bool IsPrivate
        {
            get { return _isPrivate; }
            set
            {
                if (value != _isPrivate)
                {
                    _isPrivate = value;
                    this.NotifyPropertyChanged("IsPrivate");
                }
            }
        }

        private DateTime _joinedAt;
        [JsonProperty(PropertyName = "joinedAt")]
        public DateTime JoinedAt
        {
            get { return _joinedAt; }
            set
            {
                if (value != _joinedAt)
                {
                    _joinedAt = value;
                    this.NotifyPropertyChanged("JoinedAt");
                }
            }
        }

        private string _email;
        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get { return _email; }
            set
            {
                if (value != _email)
                {
                    _email = value;
                    this.NotifyPropertyChanged("Email");
                }
            }
        }

        private bool _isVerified;
        [JsonProperty(PropertyName = "isVerified")]
        public bool IsVerified
        {
            get { return _isVerified; }
            set
            {
                if (value != _isVerified)
                {
                    _isVerified = value;
                    this.NotifyPropertyChanged("IsVerified");
                }
            }
        }

        private DsqAvatar _avatar;
        [JsonProperty(PropertyName = "avatar")]
        public DsqAvatar Avatar
        {
            get { return _avatar; }
            set
            {
                if (value != _avatar)
                {
                    _avatar = value;
                    this.NotifyPropertyChanged("Avatar");
                }
            }
        }

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
            // Return only the user ID
            return this.Id;
        }

        #endregion

        /// <summary>
        /// Converts the object into a timeline key
        /// </summary>
        /// <returns>Timeline key, e.g. 'auth.User?id=xxxx'</returns>
        public string ToTimelineKey()
        {
            return "auth.User?id=" + this.Id;
        }

        #endregion
    }
}
