using System;
using System.Collections.Generic;

namespace Disqus.Api.V30.Models
{
    public interface IDsqThread
    {
        string Feed { get; }

        string Category { get; }

        string Title { get; }

        int UserScore { get; }

        bool CanPost { get; }

        bool CanModerate { get; }
        
        List<string> Identifiers { get; }
        
        int Dislikes { get; }
        
        DateTime CreatedAt { get; }
        
        string Slug { get; }
        
        bool IsClosed { get; }
        
        int Posts { get; }

        bool UserSubscription { get; }
        
        string Link { get; }
        
        int Likes { get; }

        string Message { get; }

        string Id { get; }

        bool IsDeleted { get; }

        string ForumId { get; }

        string AuthorId { get; }
    }
}
