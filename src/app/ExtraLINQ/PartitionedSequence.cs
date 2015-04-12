using System.Collections.Generic;

namespace ExtraLinq
{
    public class PartitionedSequence<TSource>
    {
        public IEnumerable<TSource> Matches { get; private set; }
        public IEnumerable<TSource> Mismatches { get; private set; }

        public PartitionedSequence(IEnumerable<TSource> matches, IEnumerable<TSource> mismatches)
        {
            Matches = matches;
            Mismatches = mismatches;
        }
    }
}
