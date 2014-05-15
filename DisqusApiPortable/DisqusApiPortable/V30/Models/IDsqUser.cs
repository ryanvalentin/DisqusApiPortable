using System;

namespace Disqus.Api.V30.Models
{
    public interface IDsqUser
    {
        string Username { get; set; }

        string About { get; set; }

        string Name { get; set; }

        string Url { get; set; }

        bool IsAnonymous { get; }

        bool IsFollowing { get; set; }

        bool IsFollowedBy { get; }

        string ProfileUrl { get; }

        double Reputation { get; }

        string Id { get; }

        string Location { get; set; }

        bool IsPrivate { get; set; }

        DateTime JoinedAt { get; }

        string Email { get; }

        bool IsVerified { get; }

        DsqAvatar Avatar { get; }

        string ToTimelineKey();
    }
}
