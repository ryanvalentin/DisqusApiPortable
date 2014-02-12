using Newtonsoft.Json;

namespace Disqus.Api.V30.Models
{
    public class DsqTopic
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
