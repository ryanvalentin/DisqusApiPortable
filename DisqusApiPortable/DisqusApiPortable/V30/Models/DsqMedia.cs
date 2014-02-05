using Newtonsoft.Json;
using System.ComponentModel;

namespace Disqus.Api.V30.Models
{
    public class DsqMedia : INotifyPropertyChanged
    {
        private string _forum;
        [JsonProperty(PropertyName = "forum")]
        public string Forum
        {
            get { return _forum; }
            set
            {
                if (_forum != value)
                {
                    _forum = value;
                    this.NotifyPropertyChanged("Forum");
                }
            }
        }

        private string _thumbnailURL;
        [JsonProperty(PropertyName = "thumbnailURL")]
        public string ThumbnailURL
        {
            get
            {
                return _thumbnailURL;
            }
            set
            {
                if (_thumbnailURL != value)
                {
                    if (value.StartsWith("//"))
                        _thumbnailURL = "http:" + value;

                    else
                        _thumbnailURL = value;

                    this.NotifyPropertyChanged("ThumbnailURL");
                }
            }
        }

        private string _description;
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    this.NotifyPropertyChanged("Description");
                }
            }
        }

        [JsonProperty(PropertyName = "thread")]
        public string Thread { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "mediaType")]
        public string MediaType { get; set; }

        [JsonProperty(PropertyName = "html")]
        public string Html { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "resolvedUrl")]
        public string ResolvedUrl { get; set; }

        [JsonProperty(PropertyName = "post")]
        public string Post { get; set; }

        private string _thumbnailUrl;
        [JsonProperty(PropertyName = "thumbnailUrl")]
        public string ThumbnailUrl
        {
            get { return _thumbnailUrl;  }
            set
            {
                if (_thumbnailUrl != value)
                {
                    if (value.StartsWith("//"))
                        _thumbnailUrl = "http:" + value;

                    else
                        _thumbnailUrl = value;

                    this.NotifyPropertyChanged("ThumbnailUrl");
                }
            }
        }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public Metadata Metadata { get; set; }

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

    public class Metadata
    {
        private string _createMethod;
        [JsonProperty(PropertyName = "create_method")]
        public string CreateMethod
        {
            get { return _createMethod; }
            set
            {
                if (_createMethod != value)
                {
                    _createMethod = value;
                    this.NotifyPropertyChanged("CreateMethod");
                }
            }
        }

        private string _thumbnail;
        [JsonProperty(PropertyName = "thumbnail")]
        public string Thumbnail
        {
            get { return _thumbnail; }
            set
            {
                if (_thumbnail != value)
                {
                    _thumbnail = value;
                    this.NotifyPropertyChanged("Thumbnail");
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
