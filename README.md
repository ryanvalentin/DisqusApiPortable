# Portable Disqus API wrapper

This is a wrapper for the Disqus public API, documented here: http://disqus.com/api/docs/

Note: This is a work-in-progress

## What you need before using this

- Disqus API account, register here: http://disqus.com/api/applications/
- A server to host an OAuth callback page (if you're authenticating users)

## Supported frameworks

- .NET Framework 4.0.3+
- Silverlight 5
- Windows Phone 8
- Windows Store (8.0+)

## Installation

Use NuGet:
`PM> Install-Package DisqusApiPortable`

## Basic terminology

### Forum
Generally a website's shortname. A single forum is not exclusive to any one website, it depends on how it's been segemented. Corresponds to the 'var disqus_shortname' configuration variable.

A forum is a parent container for all: Settings, permissions and comment threads. This means that a thread and a comment may only belong to a single forum, and how the owner has configured it governs what can be done. Additionally some API methods require moderator permission for the authenticated user on that forum

### Thread
A comment thread, which all posts (comments) must belong to. These are identified either by the unique Disqus thread ID, but in some cases you can query by the URL and the custom identifier (if there is one) if you also know the forum shortname.

### Post
This is a comment, and it belongs exclusively to a thread.

### User
A user who has authenticated with Disqus or any of the social logins. API data will be tailored to the authenticated user's permissions on a given forum, thread or post. For example, attempting to post a comment on a thread the user has been blacklisted on will return a permission error, while for an un-blacklisted user it would be successful.

## License

MIT License
