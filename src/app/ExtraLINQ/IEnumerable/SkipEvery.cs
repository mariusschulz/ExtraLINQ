using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Skips every N-th element of a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The sequence.</param>
        /// <param name="step">Specifies N, that is, the number of elements to encounter before skipping the current one.</param>
        /// <returns>The given sequence without the skipped elements.</returns>
        public static IEnumerable<TSource> SkipEvery<TSource>(this IEnumerable<TSource> source, int step)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsZeroOrNegative(step, "step");

            return source.Where((item, index) => (index + 1) % step != 0);
        }
    }
}
