using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLINQ.Internals
{
    internal class CollectionElementsPicker<TSource>
    {
        private readonly IEnumerable<TSource> _source;
        private readonly Random _randomNumberGenerator;

        public CollectionElementsPicker(IEnumerable<TSource> source)
        {
            _source = source;
            _randomNumberGenerator = new Random();
        }

        public TSource PickRandomElement()
        {
            return PickRandomElement(_randomNumberGenerator);
        }

        public TSource PickRandomElement(Random randomNumberGenerator)
        {
            int itemCount = _source.Count();
            int randomIndex = randomNumberGenerator.Next(itemCount);
            TSource randomItem = _source.ElementAt(randomIndex);

            return randomItem;
        }

        public IEnumerable<TSource> PickRandomElements(int randomElementsCount)
        {
            CollectionShuffler<TSource> shuffler = new CollectionShuffler<TSource>(_source);
            IEnumerable<TSource> shuffledCollection = shuffler.ShuffleCollection();
            IEnumerable<TSource> randomElements = shuffledCollection.Take(randomElementsCount);

            return randomElements;
        }
    }
}
