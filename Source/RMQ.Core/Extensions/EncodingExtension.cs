using System;
using System.Text;


namespace RMQ.Core.Extensions
{
    public static class EncodingExtension
    {
        public static byte[] GetBody(this string message)
        {
            return Encoding.UTF8.GetBytes(message);
        }


        public static string GetText(this byte[] body)
        {
            return Encoding.UTF8.GetString(body);
        }
    }
}
