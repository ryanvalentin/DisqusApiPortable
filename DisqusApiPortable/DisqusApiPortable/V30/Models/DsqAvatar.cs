using Newtonsoft.Json;
using System.ComponentModel;

namespace Disqus.Api.V30.Models
{
    /// <summary>
    /// Base avatar class for a user
    /// </summary>
    public class DsqAvatar
    {
        [JsonProperty(PropertyName = "small")]
        public DsqSmall Small { get; set; }

        [JsonProperty(PropertyName = "isCustom")]
        public bool IsCustom { get; set; }

        [JsonProperty(PropertyName = "permalink")]
        public string Permalink { get; set; }

        private string _cache;
        [JsonProperty(PropertyName = "cache", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string Cache
        {
            get
            {
                return _cache;
            }
            set
            {
                if (_cache != value)
                {
                    if (value.StartsWith("//"))
                        _cache = "http:" + value;

                    else
                        _cache = value;
                }
            }
        }

        [JsonProperty(PropertyName = "large")]
        public DsqLarge Large { get; set; }

        public class DsqSmall
        {
            [JsonProperty(PropertyName = "permalink")]
            public string Permalink { get; set; }


            private string _cache;
            [JsonProperty(PropertyName = "cache")]
            public string Cache
            {
                get
                {
                    return _cache;
                }
                set
                {
                    if (_cache != value)
                    {
                        if (value.StartsWith("//"))
                            _cache = "http:" + value;

                        else
                            _cache = value;
                    }
                }
            }
        }

        public class DsqLarge
        {
            [JsonProperty(PropertyName = "permalink")]
            public string Permalink { get; set; }

            private string _cache;
            [JsonProperty(PropertyName = "cache")]
            public string Cache
            {
                get
                {
                    return _cache;
                }
                set
                {
                    if (_cache != value)
                    {
                        if (value.StartsWith("//"))
                            _cache = "http:" + value;

                        else
                            _cache = value;
                    }
                }
            }
        }
    }
}
