using System.Collections.Generic;
using System.Linq;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// Returns every N-th element of a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The sequence.</param>
        /// <param name="step">Specifies N, that is, the number of elements to encounter before returning the current one.</param>
        /// <remarks>The first item of the sequence will be returned, and after that all items offset by a multiple of step.</remarks>
        /// <returns>Every N-th element of the given sequence.</returns>
        public static IEnumerable<TSource> TakeEvery<TSource>(this IEnumerable<TSource> source, int step)
        {
            ThrowIf.Argument.IsNull(source, "source");
            ThrowIf.Argument.IsZeroOrNegative(step, "step");

            return source.Where((item, index) => index % step == 0);
        }
    }
}
