using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Disqus.Api.V30.Models
{
    /// <summary>
    /// Objects returned when either blacklisting or whitelisting a user
    /// </summary>
    public class DsqFilter
    {
        [JsonProperty(PropertyName = "forum")]
        public string Forum { get; set; }

        [JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }

        /// Value is either a string (if just an email), or an object of type DsqUser
        [JsonProperty(PropertyName = "value")]
        public object Value { get; set; }

        [JsonProperty(PropertyName = "conflictingBlacklistRemoved")]
        public bool ConflictingBlacklistRemoved { get; set; }

        /// Value is either "email" or "user"
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "createdAt")]
        public DateTime? CreatedAt { get; set; }
    }
}
