using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtraLINQ.Internals
{
    internal class CollectionItemPicker<TSource>
    {
        private readonly Random _randomNumberGenerator;
        private readonly IEnumerable<TSource> _source;

        public CollectionItemPicker(IEnumerable<TSource> source)
        {
            _source = source;
            _randomNumberGenerator = new Random();
        }

        public TSource PickRandomItem()
        {
            return PickRandomItem(_randomNumberGenerator);
        }

        public TSource PickRandomItem(Random randomNumberGenerator)
        {
            int itemCount = _source.Count();
            int randomIndex = _randomNumberGenerator.Next(itemCount);
            TSource randomItem = _source.ElementAt(randomIndex);

            return randomItem;
        }
    }
}
