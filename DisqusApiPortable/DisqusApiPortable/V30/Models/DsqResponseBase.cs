using Newtonsoft.Json;
using System.Collections.Generic;

namespace Disqus.Api.V30.Models
{
    public class DsqObjectResponse<T>
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "response")]
        public T Response { get; set; }
    }

    public class DsqListResponse<T> : IDsqListResponse<T>
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "response")]
        public List<T> Response { get; set; }
    }

    public class DsqListCursorResponse<T> : IDsqListResponse<T>
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "cursor")]
        public DsqCursor Cursor { get; set; }

        [JsonProperty(PropertyName = "response")]
        public List<T> Response { get; set; }
    }
}
