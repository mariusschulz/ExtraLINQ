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

            var matchingElements = new List<TSource>();
            var rejectedElements = new List<TSource>();

            foreach (TSource value in values)
            {
                if (predicate(value))
                {
                    matchingElements.Add(value);
                }
                else
                {
                    rejectedElements.Add(value);
                }
            }

            return new PartitionedSequence<TSource>(matchingElements, rejectedElements);
        }
    }
}
