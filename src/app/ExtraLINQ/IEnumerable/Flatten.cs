using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<TSource> Flatten<TSource>(this IEnumerable<IEnumerable<TSource>> source)
        {
            ThrowIf.Argument.IsNull(source, "source");

            return FlattenIterator(source);
        }

        private static IEnumerable<TSource> FlattenIterator<TSource>(IEnumerable<IEnumerable<TSource>> source)
        {
            throw new NotImplementedException();
        }
    }
}
