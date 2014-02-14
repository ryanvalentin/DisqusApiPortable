using System;
using System.Collections.Generic;

namespace Disqus.Api.V30.Models
{
    public interface IDsqPost
    {
        int Dislikes { get; }

        int? NumReports { get; }

        int Likes { get; }

        string Message { get; }

        string Id { get; }

        bool IsDeleted { get; }

        DsqUser Author { get; }

        List<DsqMedia> Media { get; }

        int UserScore { get; }

        bool IsSpam { get; }

        DateTime CreatedAt { get; }

        bool IsApproved { get; }

        bool IsFlagged { get; }

        bool IsHighlighted { get; }

        string RawMessage { get; }

        DsqApproxLoc ApproxLoc { get; }

        int Points { get; }

        bool IsEdited { get; }

        string IpAddress { get; }

        string ThreadId { get; }

        string ForumId { get; }
    }
}
