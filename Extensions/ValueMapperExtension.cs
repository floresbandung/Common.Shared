using System;
using System.Collections.Generic;
using System.Linq;
using Omu.ValueInjecter;

namespace Tata.Buku.Common.Shared.Extensions
{
    public static class ValueMapperExtension
    {
        public static T Mapped<T>(this object source, object value)
        {
            if (value == null) return default;
            source.InjectFrom(value);
            return (T)source;
        }

        public static T MappedTo<T>(this object source)
        {
            var res = Activator.CreateInstance(typeof(T));
            res.InjectFrom(source);

            return (T)res;
        }

        public static IList<T> MappedToList<T>(this IList<object> source)
        {
            return source.AsEnumerable().MappedToList<T>();
        }

        public static IList<T> MappedToList<T>(this List<object> source)
        {
            return source.AsEnumerable().MappedToList<T>();
        }

        public static IList<T> MappedToList<T>(this IEnumerable<object> source)
        {
            var result = new List<T>();
            foreach (var item in source)
            {
                var res = Activator.CreateInstance(typeof(T));
                res.InjectFrom(item);
                result.Add((T)res);
            }
            return result;
        }

        public static IList<T> MappedList<T>(this object source, IEnumerable<object> values)
        {
            var result = new List<T>();
            foreach (var value in values)
            {
                var val = source;
                //val.InjectFrom(value);
                Mapper.Map<T>(val, value);
                result.Add((T)val);

            }
            return result;
        }
    }
}