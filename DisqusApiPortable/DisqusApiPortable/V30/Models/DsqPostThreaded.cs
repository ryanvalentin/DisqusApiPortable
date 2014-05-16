using Newtonsoft.Json;
using System.Collections.Generic;

#if WINDOWS_CLIENT
using System.Collections.ObjectModel;
using System.Windows.Input;
#endif

namespace Disqus.Api.V30.Models
{
    public class DsqPostThreaded : DsqPost
    {
        /// <summary>
        /// Threaded comments can expand a forum, but we don't really need to
        /// </summary>
        private string _forum;
        [JsonProperty(PropertyName = "forum")]
        public string Forum
        {
            get { return _forum; }
            set
            {
                if (value.Contains("="))
                    value = value.Split('=')[1];

                if (value != _forum)
                {
                    _forum = value;
                    this.NotifyPropertyChanged("Forum");

                    //
                    // Mirror to base class for interface consistency
                    this.ForumId = _forum;
                }
            }
        }

        /// <summary>
        /// Threaded comments use a thread ID with no option to expand it
        /// </summary>
        private string _thread;
        [JsonProperty(PropertyName = "thread")]
        public string Thread
        {
            get { return _thread; }
            set
            {
                if (value.Contains("="))
                    value = value.Split('=')[1];

                if (value != _thread)
                {
                    _thread = value;
                    this.NotifyPropertyChanged("Thread");

                    //
                    // Mirror to base class for interface consistency
                    this.ThreadId = _thread;
                }
            }
        }

        private int _depth;
        [JsonProperty(PropertyName = "depth")]
        public int Depth
        {
            get { return _depth; }
            set
            {
                if (value != _depth)
                {
                    _depth = value;
                    this.NotifyPropertyChanged("Depth");
                }
            }
        }

        [JsonProperty(PropertyName = "parent")]
        public string Parent { get; set; }

        private IDsqPost _parentPost;
        [JsonIgnore]
        public IDsqPost ParentPost
        {
            get { return _parentPost; }
            set
            {
                if (value != _parentPost)
                {
                    _parentPost = value;
                    this.NotifyPropertyChanged("ParentPost");
                }
            }
        }
    }
}
