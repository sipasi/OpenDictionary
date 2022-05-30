using System;
using System.Collections.ObjectModel;

namespace OpenDictionary.Collections.Extensions;

public static class CollectionExtension
{
    public static int? IndexOf<T>(this Collection<T> collection, Func<T, bool> compare)
    {
        int length = collection.Count;

        for (int i = 0; i < length; i++)
        {
            T item = collection[i];

            if (compare.Invoke(item) is true)
            {
                return i;
            }
        }

        return null;
    }
}