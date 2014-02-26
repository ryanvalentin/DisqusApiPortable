using Disqus.Api.V30;

namespace Disqus.Api.Extensions
{
    /// <summary>
    /// Formats error string into something more readable for frontend use
    /// </summary>
    public static class ErrorHelper
    {
        public static string ToErrorString(this DsqApiException exception)
        {
            return exception.Message.Split(';')[0]; // Splits the "System.Threading.Tasks" task exception
        }
    }
}
