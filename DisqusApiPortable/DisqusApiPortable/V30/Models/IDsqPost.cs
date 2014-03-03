using System;
using System.Collections.Generic;

namespace Disqus.Api.V30.Models
{
    public interface IDsqPost
    {
        int Dislikes { get; set; }

        int? NumReports { get; set; }

        int Likes { get; set; }

        string Message { get; set; }

        string Id { get; }

        bool IsDeleted { get; set; }

        DsqUser Author { get; }

        List<DsqMedia> Media { get; }

        int UserScore { get; set; }

        bool IsSpam { get; set; }

        DateTime CreatedAt { get; }

        bool IsApproved { get; set; }

        bool IsFlagged { get; set; }

        bool? IsHighlighted { get; set; }

        string RawMessage { get; set; }

        DsqApproxLoc ApproxLoc { get; }

        int Points { get; set; }

        bool IsEdited { get; set; }

        string IpAddress { get; }

        string ThreadId { get; }

        string ForumId { get; }

        string CurrentState { get; }
    }
}
