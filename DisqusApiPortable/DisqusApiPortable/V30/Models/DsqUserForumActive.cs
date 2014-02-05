using Newtonsoft.Json;

namespace Disqus.Api.V30.Models
{
    public class DsqUserForumActive : DsqUser
    {
        [JsonProperty(PropertyName = "numPosts")]
        public int NumPosts { get; set; }
    }
}
