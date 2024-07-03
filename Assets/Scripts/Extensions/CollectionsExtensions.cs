using System.Collections.Generic;

namespace Bits.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> source)
        {
            foreach (T item in source)
            {
                collection.Add(item);
            }
        }
    }
}