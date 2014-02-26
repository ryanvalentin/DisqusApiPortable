using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disqus.Api.Extensions
{
    public static class StringExtensions
    {
        public static string Truncate(this string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input.Length <= maxLength ? input : input.Substring(0, maxLength);
        }

        public static string TruncateWithEllipses(this string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input.Length <= maxLength ? input : input.Substring(0, maxLength) + "...";
        }
    }
}
