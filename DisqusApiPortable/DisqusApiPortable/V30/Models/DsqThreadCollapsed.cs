using Newtonsoft.Json;

namespace Disqus.Api.V30.Models
{
    /// <summary>
    /// A thread with the 'forum' and 'author' values collapsed
    /// </summary>
    public class DsqThreadCollapsed : DsqThreadBase
    {
        private string _author;
        [JsonProperty(PropertyName = "author")]
        public string Author
        {
            get { return _author; }
            set
            {
                if (value != _author)
                {
                    _author = value;
                    this.NotifyPropertyChanged("Author");

                    //
                    // Mirror to base class for interface consistency
                    this.AuthorId = _author;
                }
            }
        }

        private string _forum;
        [JsonProperty(PropertyName = "forum")]
        public string Forum
        {
            get { return _forum; }
            set
            {
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
    }
}
