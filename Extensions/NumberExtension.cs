using System;
using System.Net;

namespace DD.Tata.Buku.Common.Shared.Extensions
{
    public static class NumberExtension
    {
        public static DateTime ToDateTime(this long value)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(value).DateTime;
        }

        public static HttpStatusCode ToHttpStatusCode(this int value)
        {
            try
            {
                return (HttpStatusCode) value;
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
