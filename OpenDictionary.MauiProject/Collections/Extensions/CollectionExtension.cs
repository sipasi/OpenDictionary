using System;
using System.Collections.ObjectModel;

namespace OpenDictionary.Collections.Extensions;

public static class CollectionExtension
{
    public static int? IndexOf<T>(this Collection<T> collection, T find, Func<T, T, bool> comparison)
    {
        int length = collection.Count;

        for (int i = 0; i < length; i++)
        {
            T item = collection[i];

            if (comparison.Invoke(find, item) is true)
            {
                return i;
            }
        }

        return null;
    }
    public static bool Contains<T>(this Collection<T> collection, T find, Func<T, T, bool> comparison)
    {
        int length = collection.Count;

        for (int i = 0; i < length; i++)
        {
            T item = collection[i];

            if (comparison.Invoke(find, item) is true)
            {
                return true;
            }
        }

        return false;
    }
}