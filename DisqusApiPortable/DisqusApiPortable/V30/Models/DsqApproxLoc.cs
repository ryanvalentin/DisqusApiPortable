using Newtonsoft.Json;

namespace Disqus.Api.V30.Models
{
    public class DsqApproxLoc
    {
        [JsonProperty(PropertyName = "lat")]
        public double Lat { get; set; }

        [JsonProperty(PropertyName = "lng")]
        public double Lng { get; set; }
    }
}
