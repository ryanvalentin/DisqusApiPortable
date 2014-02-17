using System;
using System.Collections.Generic;

namespace Disqus.Api.V30.Models
{
    public interface IDsqPost
    {
        int Dislikes { get; set; }

        int? NumReports { get; set; }

        int Likes { get; set; }

        string Message { get; }

        string Id { get; }

        bool IsDeleted { get; }

        DsqUser Author { get; }

        List<DsqMedia> Media { get; }

        int UserScore { get; set; }

        bool IsSpam { get; }

        DateTime CreatedAt { get; }

        bool IsApproved { get; }

        bool IsFlagged { get; }

        bool IsHighlighted { get; }

        string RawMessage { get; }

        DsqApproxLoc ApproxLoc { get; }

        int Points { get; set; }

        bool IsEdited { get; set; }

        string IpAddress { get; }

        string ThreadId { get; }

        string ForumId { get; }
    }
}
