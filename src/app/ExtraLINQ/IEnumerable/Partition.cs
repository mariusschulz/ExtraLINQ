using System;
using System.Collections.Generic;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Uses the given predicate to partition the given sequence into two sequences,
        /// one with all the matches and one with all the mismatches.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The sequence to partition.</param>
        /// <param name="predicate">The predicate that determines whether an element is a match.</param>
        /// <returns>An object holding the two partitions (matches and mismatches).</returns>
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
