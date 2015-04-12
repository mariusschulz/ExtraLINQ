using System.Collections.Generic;

namespace ExtraLinq
{
    public class PartitionedSequence<TSource>
    {
        public IList<TSource> Matches { get; private set; }
        public IList<TSource> Mismatches { get; private set; }

        public PartitionedSequence(IList<TSource> matches, IList<TSource> mismatches)
        {
            Matches = matches;
            Mismatches = mismatches;
        }
    }
}
