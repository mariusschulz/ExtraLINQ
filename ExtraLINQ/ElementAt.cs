using System;
using System.Collections.Generic;
using System.Linq;
using ExtraLinq.Internals;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Returns the item at a specified index in a specified collection using a specified indexing strategy.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/>containing the item.</param>
        /// <param name="index">The item's index.</param>
        /// <param name="indexingStrategy">The <see cref="IndexingStrategy"/> to use.</param>
        /// <returns>The item at the specified index.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> is empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
        public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, int index, IndexingStrategy indexingStrategy)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            // We copy source to sourceList to avoid multiple enumerations of source.
            IList<TSource> sourceList = source.ToList();

            if (sourceList.IsEmpty())
            {
                throw new ArgumentException("source must not be empty.", "source");
            }

            switch (indexingStrategy)
            {
                case IndexingStrategy.Cyclic:
                    index = CollectionIndexCalculator.CalculateCyclicIndex(index, sourceList.Count);
                    break;

                case IndexingStrategy.Clamp:
                    index = CollectionIndexCalculator.CalculateClampIndex(index, sourceList.Count);
                    break;
            }

            return sourceList.ElementAt(index);
        }
    }
}
