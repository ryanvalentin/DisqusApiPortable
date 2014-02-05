using Newtonsoft.Json;

namespace Disqus.Api.V30.Models
{
    /// <summary>
    /// Thread with a top post, used in threads/listPopular
    /// </summary>
    public class DsqThreadTopPost : DsqThreadExpanded
    {
        [JsonProperty(PropertyName = "topPost")]
        public DsqPostThreaded TopPost { get; set; }

        [JsonProperty(PropertyName = "postsInInterval")]
        public int PostsInInterval { get; set; }
    }
}
