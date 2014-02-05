using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Disqus.Api.V30.Models
{
    /// <summary>
    /// Detailed version of a Disqus user, available in users/details
    /// </summary>
    public class DsqUserDetails : DsqUser
    {
        [JsonProperty(PropertyName = "connections")]
        public DsqConnections Connections { get; set; }

        [JsonProperty(PropertyName = "numFollowers")]
        public int NumFollowers { get; set; }

        [JsonProperty(PropertyName = "numPosts")]
        public int NumPosts { get; set; }

        [JsonProperty(PropertyName = "numFollowing")]
        public int NumFollowing { get; set; }

        [JsonProperty(PropertyName = "numLikesReceived")]
        public int NumLikesReceived { get; set; }
    }

    public class DsqConnections
    {
        [JsonProperty(PropertyName = "twitter")]
        public DsqConnection Twitter { get; set; }

        [JsonProperty(PropertyName = "google")]
        public DsqConnection Google { get; set; }

        [JsonProperty(PropertyName = "facebook")]
        public DsqConnection Facebook { get; set; }
    }

    public class DsqConnection
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
