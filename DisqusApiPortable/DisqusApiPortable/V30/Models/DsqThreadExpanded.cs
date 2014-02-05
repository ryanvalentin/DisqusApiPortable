using Newtonsoft.Json;

namespace Disqus.Api.V30.Models
{
    public class DsqThreadExpanded : DsqThreadBase
    {
        private DsqUser _author;
        [JsonProperty(PropertyName = "author")]
        public DsqUser Author
        {
            get { return _author; }
            set
            {
                if (value != _author)
                {
                    _author = value;
                    this.NotifyPropertyChanged("Author");

                    //
                    // Set forum ID manually for interface consistency
                    this.DisqusAuthorId = _forum.Id;
                }
            }
        }

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
                    // Set forum ID manually for interface consistency
                    this.DisqusForumId = _forum.Id;
                }
            }
        }
    }
}
