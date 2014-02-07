using System.Collections.Generic;

namespace Disqus.Api.V30.Models
{
    public interface IDsqListResponse<T>
    {
        int Code { get; }

        List<T> Response { get; }
    }
}
