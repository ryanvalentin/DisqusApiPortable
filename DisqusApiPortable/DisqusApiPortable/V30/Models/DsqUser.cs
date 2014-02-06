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

        [JsonProperty(PropertyName = "reputation")]
        public double Reputation { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "isPrivate")]
        public bool IsPrivate { get; set; }

        [JsonProperty(PropertyName = "joinedAt")]
        public DateTime JoinedAt { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "isVerified")]
        public bool IsVerified { get; set; }

        [JsonProperty(PropertyName = "avatar")]
        public DsqAvatar Avatar { get; set; }

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
