using System;
using System.Collections.Generic;

namespace Jimothy.Utilities.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerables, Action<T> action)
        {
            foreach (var item in enumerables)
            {
                action(item);
            }
        }
    }
}