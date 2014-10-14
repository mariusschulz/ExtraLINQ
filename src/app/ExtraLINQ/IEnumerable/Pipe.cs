using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<TSource> Pipe<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsNull(action, "action");

            return PipeImplementation(source, action);
        }

        private static IEnumerable<TSource> PipeImplementation<TSource>(IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (var element in source)
            {
                action(element);
                yield return element;
            }
        }
    }
}
