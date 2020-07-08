using System;
using System.Linq;

namespace Tata.Buku.Common.Shared.Extensions
{
    public static class StringExtensions
    {
        public static int ToInteger(this string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        public static DateTime ToDateTime(this string value)
        {
            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
                return DateTime.UtcNow;
            }
        }

        public static bool ToBoolean(this string value, bool defaultValue = false)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return defaultValue;
            }
        }
        
        public static decimal ToDecimal(this string value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
                return 0;
            }
        }
        
        public static short ToShort(this string value)
        {
            try
            {
                return Convert.ToInt16(value);
            }
            catch
            {
                return 0;
            }
        }
        
        public static byte ToByte(this string value)
        {
            try
            {
                return Convert.ToByte(value);
            }
            catch
            {
                return 0;
            }
        }
        
        public static string Between(this string src, string findFrom, string findTo)
        {
            var start = src.IndexOf(findFrom, StringComparison.Ordinal);
            var to = src.IndexOf(findTo, start + findFrom.Length, StringComparison.Ordinal);
            if (start < 0 || to < 0) return "";
            var s = src.Substring(
                start + findFrom.Length,
                to - start - findFrom.Length);
            return s;
        }

        public static bool Compare(this string src, string destination)
        {
            return string.Equals(src, destination, StringComparison.InvariantCultureIgnoreCase);
        }
        
        public static bool IsNullOrEmpty(this string src)
        {
            return string.IsNullOrEmpty(src);
        }

        public static string FirstCharToLower(this string text)
        {
            return text.First().ToString().ToLower() + String.Join("", text.Skip(1));
        }

        public static int GetEventIdNumber(this string text)
        {
            if (!text.Contains("_")) return 0;
            var split = text.Split("_");
            try
            {
                return Convert.ToInt32(split[1]);
            }
            catch
            {
                return 0;
            }
        }
    }
}