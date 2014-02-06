using Newtonsoft.Json;
using System.ComponentModel;

namespace Disqus.Api.V30.Models
{
    /// <summary>
    /// Generic cursor class
    /// </summary>
    public class DsqCursor
    {
        [JsonProperty(PropertyName = "prev")]
        public string Prev { get; set; }

        [JsonProperty(PropertyName = "hasNext")]
        public bool HasNext { get; set; }

        [JsonProperty(PropertyName = "next")]
        public string Next { get; set; }

        [JsonProperty(PropertyName = "hasPrev")]
        public bool HasPrev { get; set; }

        [JsonProperty(PropertyName = "total")]
        public int? Total { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "more")]
        public bool More { get; set; }
    }
}
