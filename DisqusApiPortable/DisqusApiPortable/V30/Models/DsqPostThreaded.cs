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
        public DsqPostThreaded()
        {
#if WINDOWS_CLIENT
            this.UpvotingUsers = new ObservableCollection<DsqUser>();
#endif
        }

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

        #region JSON ignored properties

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

#if WINDOWS_CLIENT
        [JsonIgnore]
        public ObservableCollection<DsqUser> UpvotingUsers { get; set; }
        
        [JsonIgnore]
        private int _queuedReplyCount;
        /// <summary>
        /// Number of reply comments waiting to be added to collection
        /// </summary>
        public int QueuedReplyCount
        {
            get { return _queuedReplyCount; }
            set
            {
                if (value != _queuedReplyCount)
                {
                    _queuedReplyCount = value;
                    this.NotifyPropertyChanged("QueuedReplyCount");
                }
            }
        }

        private ICommand _releaseQueuedReplies;
        public ICommand ReleaseQueuedReplies
        {
            get { return _releaseQueuedReplies; }
            set
            {
                if (value != _releaseQueuedReplies)
                {
                    _releaseQueuedReplies = value;
                    this.NotifyPropertyChanged("ReleaseQueuedReplies");
                }
            }
        }

#endif

        #endregion
    }
}
