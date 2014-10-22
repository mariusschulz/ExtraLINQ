using System.Collections.Generic;

namespace ExtraLinq
{
    public class PartitionedSequence<TSource>
    {
        public IEnumerable<TSource> MatchingElements { get; private set; }
        public IEnumerable<TSource> RejectedElements { get; private set; }

        public PartitionedSequence(IEnumerable<TSource> matchingElements, IEnumerable<TSource> rejectedElements)
        {
            MatchingElements = matchingElements;
            RejectedElements = rejectedElements;
        }
    }
}
