using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        public static PartitionedSequence<TSource> Partition<TSource>(this IEnumerable<TSource> values, Func<TSource, bool> predicate)
        {
            ThrowIf.Argument.IsNull(values, "values");
            ThrowIf.Argument.IsNull(predicate, "predicate");

            var matches = new List<TSource>();
            var mismatches = new List<TSource>();

            foreach (TSource value in values)
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
