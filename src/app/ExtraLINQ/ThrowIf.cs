using System;

namespace ExtraLinq
{
    internal static class ThrowIf
    {
        public static class Argument
        {
            public static void IsNull(object argument, string argumentName)
            {
                if (argument == null)
                {
                    throw new ArgumentNullException(argumentName);
                }
            }

            public static void IsNegative(int argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName, argumentName + " must not be negative.");
                }
            }
        }
    }
}
