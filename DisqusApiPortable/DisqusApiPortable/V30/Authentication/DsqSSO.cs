/*using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Disqus.Api.V30.Authentication
{
    public static class DsqSSO
    {
        private const string _apiSecret = "DISQUS_SECRET_KEY"; // TODO enter your API secret key

        /// <summary>
        /// Gets the Disqus SSO payload to authenticate users
        /// </summary>
        /// <param name="user_id">The unique ID to associate with the user</param>
        /// <param name="user_name">Non-unique name shown next to comments.</param>
        /// <param name="user_email">User's email address, defined by RFC 5322</param>
        /// <param name="avatar_url">URL of the avatar image</param>
        /// <param name="website_url">Website, blog or custom profile URL for the user, defined by RFC 3986</param>
        /// <returns>A string containing the signed payload</returns>
        public static string GetPayload(string user_id, string user_name, string user_email, string avatar_url = "", string website_url = "")
        {
            var userdata = new
            {
                id = user_id,
                username = user_name,
                email = user_email,
                avatar = avatar_url,
                url = website_url
            };

            string serializedUserData = JsonConvert.SerializeObject(userdata);
            return GeneratePayload(serializedUserData);
        }

        /// <summary>
        /// Method to log out a user from SSO
        /// </summary>
        /// <returns>A signed, empty payload string</returns>
        public static string LogoutUser()
        {
            var userdata = new { };
            string serializedUserData = JsonConvert.SerializeObject(userdata);
            return GeneratePayload(serializedUserData);
        }

        private static string GeneratePayload(string serializedUserData)
        {
            byte[] userDataAsBytes = StringToAscii(serializedUserData);

            // Base64 Encode the message
            string Message = System.Convert.ToBase64String(userDataAsBytes);

            // Get the proper timestamp
            TimeSpan ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            string Timestamp = Convert.ToInt32(ts.TotalSeconds).ToString();

            // Convert the message + timestamp to bytes
            byte[] messageAndTimestampBytes = StringToAscii(Message + " " + Timestamp);

            // Convert Disqus API key to HMAC-SHA1 signature
            byte[] apiBytes = StringToAscii(_apiSecret);
            HMACSHA1 hmac = new HMACSHA1(apiBytes);
            byte[] hashedMessage = hmac.ComputeHash(messageAndTimestampBytes);

            // Put it all together into the final payload
            return Message + " " + ByteToString(hashedMessage) + " " + Timestamp;
        }

        private static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }

        private static byte[] StringToAscii(string s) {
            byte[] retval = new byte[s.Length];
            for (int ix = 0; ix < s.Length; ++ix) {
                char ch = s[ix];
                if (ch <= 0x7f) retval[ix] = (byte)ch;
                else retval[ix] = (byte)'?';
            }
            return retval;
        }
    }
}
*/