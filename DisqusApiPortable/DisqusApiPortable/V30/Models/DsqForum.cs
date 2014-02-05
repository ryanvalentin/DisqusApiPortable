using Newtonsoft.Json;
using System.ComponentModel;

namespace Disqus.Api.V30.Models
{
    public class DsqForum : INotifyPropertyChanged
    {
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

        [JsonProperty(PropertyName = "founder")]
        public string Founder { get; set; }

        [JsonProperty(PropertyName = "settings")]
        public DsqSettings Settings { get; set; }

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

        [JsonProperty(PropertyName = "favicon")]
        public DsqFavicon Favicon { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

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

    public class DsqFavicon : INotifyPropertyChanged
    {
        [JsonProperty(PropertyName = "permalink")]
        public string Permalink { get; set; }

        private string _cache;
        [JsonProperty(PropertyName = "cache")]
        public string Cache
        {
            get
            {
                return _cache;
            }
            set
            {
                if (_cache != value)
                {
                    if (value.StartsWith("//"))
                        _cache = "http:" + value;

                    else
                        _cache = value;

                    this.NotifyPropertyChanged("Cache");
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

    public class DsqSettings : INotifyPropertyChanged
    {
        [JsonProperty(PropertyName = "backplaneEnabled")]
        public bool BackplaneEnabled { get; set; }

        [JsonProperty(PropertyName = "ssoRequired")]
        public bool SsoRequired { get; set; }

        [JsonProperty(PropertyName = "allowAnonPost")]
        public bool AllowAnonPost { get; set; }

        [JsonProperty(PropertyName = "allowMedia")]
        public bool AllowMedia { get; set; }

        [JsonProperty(PropertyName = "audienceSyncEnabled")]
        public bool AudienceSyncEnabled { get; set; }

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
