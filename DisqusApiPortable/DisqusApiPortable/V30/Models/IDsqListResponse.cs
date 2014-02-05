using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disqus.Api.V30.Models
{
    public interface IDsqListResponse<T>
    {
        int Code { get; }

        List<T> Response { get; }
    }
}
