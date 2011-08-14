using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLINQ.Internals
{
    internal class CollectionShuffler<TSource>
    {
        private readonly IEnumerable<TSource> _source;
        private readonly Random _randomNumberGenerator;

        public CollectionShuffler(IEnumerable<TSource> source)
        {
            _source = source;
            _randomNumberGenerator = new Random();
        }

        public IEnumerable<TSource> ShuffleCollection()
        {
            return ShuffleCollection(_randomNumberGenerator);
        }

        public IEnumerable<TSource> ShuffleCollection(Random randomNumberGenerator)
        {
            TSource[] items = _source.ToArray();

            for (int n = 0; n < items.Length; n++)
            {
                int k = randomNumberGenerator.Next(n, items.Length);
                yield return items[k];

                items[k] = items[n];
            }
        }
    }
}
