using Newtonsoft.Json;

namespace Disqus.Api.V30.Models
{
    public class DsqVoteObject
    {
        [JsonProperty(PropertyName = "vote")]
        public int Vote { get; set; }

        [JsonProperty(PropertyName = "likesDelta")]
        public int LikesDelta { get; set; }

        [JsonProperty(PropertyName = "dislikesDelta")]
        public int DislikesDelta { get; set; }

        [JsonProperty(PropertyName = "delta")]
        public int Delta { get; set; }

        [JsonProperty(PropertyName = "post")]
        public DsqPostThreaded Post { get; set; }
    }
}
