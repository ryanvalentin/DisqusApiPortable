using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Disqus.Api.V30.Models
{
    /// <summary>
    /// A Post class with the 'thread' and 'forum' arguments expanded
    /// </summary>
    public class DsqPostExpanded : DsqPost
    {
        private DsqForum _forum;
        [JsonProperty(PropertyName = "forum")]
        public DsqForum Forum
        {
            get { return _forum; }
            set
            {
                if (value != _forum)
                {
                    _forum = value;
                    this.NotifyPropertyChanged("Forum");

                    //
                    // Populate forum ID of base class for interface consistency
                    this.ForumId = _forum.Id;
                }
            }
        }

        private DsqThreadCollapsed _thread;
        [JsonProperty(PropertyName = "thread")]
        public DsqThreadCollapsed Thread
        {
            get { return _thread; }
            set
            {
                if (value != _thread)
                {
                    _thread = value;
                    this.NotifyPropertyChanged("Thread");

                    //
                    // Populate thread ID of base class for interface consistency
                    this.ThreadId = _thread.Id;
                }
            }
        }

        private string _parent;
        [JsonProperty(PropertyName = "parent")]
        public string Parent
        {
            get { return _parent; }
            set
            {
                if (value != _parent)
                {
                    _parent = value;
                    this.NotifyPropertyChanged("Parent");
                }
            }
        }
    }
}
