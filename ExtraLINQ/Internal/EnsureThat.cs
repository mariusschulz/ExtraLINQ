using System;

namespace ExtraLinq
{
    internal static class EnsureThat
    {
        public static class Argument
        {
            public static void IsNotNull(object argument, string argumentName)
            {
                if (argument == null)
                {
                    throw new ArgumentNullException(argumentName);
                }
            }
        }
    }
}
