using System;

namespace Disqus.Api.V30.Models
{
    public interface IDsqUser
    {
        string Username { get; }

        string About { get; }

        string Name { get; }

        string Url { get; }

        bool IsAnonymous { get; }

        bool IsFollowing { get; }

        bool IsFollowedBy { get; }

        string ProfileUrl { get; }

        double Reputation { get; }

        string Id { get; }

        string Location { get; }

        bool IsPrivate { get; }

        DateTime JoinedAt { get; }

        string Email { get; }

        bool IsVerified { get; }

        DsqAvatar Avatar { get; }
    }
}
