using System;

namespace ExtraLinq.Extensions
{
    internal static class IntExtensions
    {
        public static int ClampTo(this int value, int lower, int upper)
        {
            int actualLower = Math.Min(lower, upper);
            int actualUpper = Math.Max(lower, upper);

            if (value < actualLower)
            {
                return actualLower;
            }

            if (value > actualUpper)
            {
                return actualUpper;
            }

            return value;
        }
    }
}
