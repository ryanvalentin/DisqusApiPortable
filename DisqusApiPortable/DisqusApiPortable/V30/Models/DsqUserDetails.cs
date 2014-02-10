using Newtonsoft.Json;
using System.ComponentModel;

namespace Disqus.Api.V30.Models
{
    /// <summary>
    /// Detailed version of a Disqus user, available in users/details
    /// </summary>
    public class DsqUserDetails : DsqUser
    {
        public DsqUserDetails()
        {
            this.Connections = new DsqConnections();
            this.Connections.Facebook = new DsqConnection();
            this.Connections.Twitter = new DsqConnection();
            this.Connections.Google = new DsqConnection();
            this.Avatar = new DsqAvatar();
            this.Avatar.Large = new DsqAvatar.DsqLarge();
            this.Avatar.Small = new DsqAvatar.DsqSmall();
        }

        private DsqConnections _connections;
        [JsonProperty(PropertyName = "connections")]
        public DsqConnections Connections
        {
            get { return _connections; }
            set
            {
                if (value != _connections)
                {
                    _connections = value;
                    this.NotifyPropertyChanged("Connections");
                }
            }
        }

        private int _numFollowers;
        [JsonProperty(PropertyName = "numFollowers")]
        public int NumFollowers
        {
            get { return _numFollowers; }
            set
            {
                if (value != _numFollowers)
                {
                    _numFollowers = value;
                    this.NotifyPropertyChanged("NumFollowers");
                }
            }
        }

        private int _numPosts;
        [JsonProperty(PropertyName = "numPosts")]
        public int NumPosts
        {
            get { return _numPosts; }
            set
            {
                if (value != _numPosts)
                {
                    _numPosts = value;
                    this.NotifyPropertyChanged("NumPosts");
                }
            }
        }

        private int _numFollowing;
        [JsonProperty(PropertyName = "numFollowing")]
        public int NumFollowing
        {
            get { return _numFollowing; }
            set
            {
                if (value != _numFollowing)
                {
                    _numFollowing = value;
                    this.NotifyPropertyChanged("NumFollowing");
                }
            }
        }

        private int _numLikesReceived;
        [JsonProperty(PropertyName = "numLikesReceived")]
        public int NumLikesReceived
        {
            get { return _numLikesReceived; }
            set
            {
                if (value != _numLikesReceived)
                {
                    _numLikesReceived = value;
                    this.NotifyPropertyChanged("NumLikesReceived");
                }
            }
        }
    }

    public class DsqConnections : INotifyPropertyChanged
    {
        private DsqConnection _twitter;
        [JsonProperty(PropertyName = "twitter")]
        public DsqConnection Twitter
        {
            get { return _twitter; }
            set
            {
                if (value != _twitter)
                {
                    _twitter = value;
                    this.NotifyPropertyChanged("Twitter");
                }
            }
        }

        private DsqConnection _google;
        [JsonProperty(PropertyName = "google")]
        public DsqConnection Google
        {
            get { return _google; }
            set
            {
                if (value != _google)
                {
                    _google = value;
                    this.NotifyPropertyChanged("Google");
                }
            }
        }

        private DsqConnection _facebook;
        [JsonProperty(PropertyName = "facebook")]
        public DsqConnection Facebook
        {
            get { return _facebook; }
            set
            {
                if (value != _facebook)
                {
                    _facebook = value;
                    this.NotifyPropertyChanged("Facebook");
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
    }

    public class DsqConnection
    {
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
