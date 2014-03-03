using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Disqus.Api.V30.Models
{
    public class DsqPost : IDsqPost, INotifyPropertyChanged
    {
        public DsqPost()
        {

        }

        public DsqPost(IDsqPost post)
        {
            Dislikes = post.Dislikes;
            NumReports = post.NumReports;
            Likes = post.Likes;
            Message = post.Message;
            Id = post.Id;
            IsDeleted = post.IsDeleted;
            IsHighlighted = post.IsHighlighted;
            Author = post.Author;
            Media = post.Media;
            UserScore = post.UserScore;
            IsSpam = post.IsSpam;
            IsApproved = post.IsApproved;
            IsFlagged = post.IsFlagged;
            RawMessage = post.RawMessage;
            Points = post.Points;
            IsEdited = post.IsEdited;
            IpAddress = post.IpAddress;
        }

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

        private int? _numReports;
        [JsonProperty(PropertyName = "numReports")]
        public int? NumReports
        {
            get { return _numReports; }
            set
            {
                if (value != _numReports)
                {
                    _numReports = value;
                    this.NotifyPropertyChanged("NumReports");
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
                    this.NotifyPropertyChanged("CurrentState");
                }
            }
        }

        private bool? _isHighlighted;
        [JsonProperty(PropertyName = "isHighlighted")]
        public bool? IsHighlighted
        {
            get { return _isHighlighted; }
            set
            {
                if (value != _isHighlighted)
                {
                    _isHighlighted = value;
                    this.NotifyPropertyChanged("IsHighlighted");
                    this.NotifyPropertyChanged("CurrentState");
                }
            }
        }

        [JsonProperty(PropertyName = "author")]
        public DsqUser Author { get; set; }

        private List<DsqMedia> _media;
        [JsonProperty(PropertyName = "media")]
        public List<DsqMedia> Media
        {
            get { return _media; }
            set
            {
                if (value != _media)
                {
                    _media = value;
                    this.NotifyPropertyChanged("Media");
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

        private bool _isSpam;
        [JsonProperty(PropertyName = "isSpam")]
        public bool IsSpam
        {
            get { return _isSpam; }
            set
            {
                if (value != _isSpam)
                {
                    _isSpam = value;
                    this.NotifyPropertyChanged("IsSpam");
                    this.NotifyPropertyChanged("CurrentState");
                }
            }
        }

        [JsonProperty(PropertyName = "createdAt")]
        public DateTime CreatedAt { get; set; }

        private bool _isApproved;
        [JsonProperty(PropertyName = "isApproved")]
        public bool IsApproved
        {
            get { return _isApproved; }
            set
            {
                if (value != _isApproved)
                {
                    _isApproved = value;
                    this.NotifyPropertyChanged("IsApproved");
                    this.NotifyPropertyChanged("CurrentState");
                }
            }
        }

        private bool _isFlagged;
        [JsonProperty(PropertyName = "isFlagged")]
        public bool IsFlagged
        {
            get { return _isFlagged; }
            set
            {
                if (value != _isFlagged)
                {
                    _isFlagged = value;
                    this.NotifyPropertyChanged("IsFlagged");
                }
            }
        }

        private string _rawMessage;
        [JsonProperty(PropertyName = "raw_message")]
        public string RawMessage
        {
            get { return _rawMessage; }
            set
            {
                if (value != _rawMessage)
                {
                    _rawMessage = value;
                    this.NotifyPropertyChanged("RawMessage");
                }
            }
        }

        [JsonProperty(PropertyName = "approxLoc")]
        public DsqApproxLoc ApproxLoc { get; set; }

        private int _points;
        [JsonProperty(PropertyName = "points")]
        public int Points
        {
            get { return _points; }
            set
            {
                if (value != _points)
                {
                    _points = value;
                    this.NotifyPropertyChanged("Points");
                }
            }
        }

        private bool _isEdited;
        [JsonProperty(PropertyName = "isEdited")]
        public bool IsEdited
        {
            get { return _isEdited; }
            set
            {
                if (value != _isEdited)
                {
                    _isEdited = value;
                    this.NotifyPropertyChanged("IsEdited");
                }
            }
        }

        #region Ignored properties that vary between an object and a string

        [JsonIgnore]
        public string CurrentState
        {
            get
            {
                if (this.IsApproved && !this.IsDeleted)
                    return "approved";

                else if (this.IsDeleted)
                    return "deleted";

                else if (!this.IsDeleted && !this.IsSpam && !this.IsApproved)
                    return "unapproved";

                else if (this.IsSpam && !this.IsApproved)
                    return "spam";

                else
                    return "";
            }
        }

        [JsonIgnore]
        public string ThreadId { get; set; }

        [JsonIgnore]
        public string ForumId { get; set; }

        #endregion

        [JsonProperty(PropertyName = "ipAddress")]
        public string IpAddress { get; set; }

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
