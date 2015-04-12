using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        public static PartitionedSequence<TSource> Partition<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            ThrowIf.Argument.IsNull(source, "values");
            ThrowIf.Argument.IsNull(predicate, "predicate");

            var matches = new List<TSource>();
            var mismatches = new List<TSource>();

            foreach (TSource value in source)
            {
                if (predicate(value))
                {
                    matches.Add(value);
                }
                else
                {
                    mismatches.Add(value);
                }
            }

            return new PartitionedSequence<TSource>(matches, mismatches);
        }
    }
}
