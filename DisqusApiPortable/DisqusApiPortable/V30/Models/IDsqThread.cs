using System;
using System.Collections.Generic;

namespace Disqus.Api.V30.Models
{
    public interface IDsqThread
    {
        string Feed { get; }

        string Category { get; set; }

        string Title { get; set; }

        string CleanTitle { get; set; }

        int UserScore { get; set; }

        bool CanPost { get; }

        bool CanModerate { get; }
        
        List<string> Identifiers { get; }

        int Dislikes { get; set; }
        
        DateTime CreatedAt { get; }

        string Slug { get; set; }

        bool IsClosed { get; set; }

        int Posts { get; set; }

        bool UserSubscription { get; set; }

        string Link { get; set; }

        int Likes { get; set; }

        string Message { get; set; }

        string Id { get; }

        bool IsDeleted { get; set; }

        string ForumId { get; }

        string AuthorId { get; }
    }
}
