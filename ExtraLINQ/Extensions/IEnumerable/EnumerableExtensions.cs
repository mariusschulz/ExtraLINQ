using System;

namespace ExtraLinq
{
    public static partial class EnumerableExtensions
    {
        private static readonly Random _random;

        static EnumerableExtensions()
        {
            _random = new Random();
        }
    }
}
