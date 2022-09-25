using System;
using System.Collections.Generic;

using OpenDictionary.Structures.Randoms;

namespace OpenDictionary.Collections.Extensions;

public static class ListExtension
{
    public static int? IndexOf<T>(this IReadOnlyList<T> collection, T find, Func<T, T, bool> comparison)
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
    public static bool Contains<T>(this IReadOnlyList<T> collection, T find, Func<T, T, bool> comparison)
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
    public static IList<T> Randomize<T>(this IList<T> array, int times = 1) => Randomize(array, from: 0, to: array.Count, times);
    public static IList<T> Randomize<T>(this IList<T> array, int from, int to, int times = 1)
    {
        for (int step = 0; step < times; step++)
        {
            for (int current = 0; current < to; current++)
            {
                int next = RandomNumbers.Range(from, to);

                T currentItem = array[current];
                T nextItem = array[next];

                array[current] = nextItem;
                array[next] = currentItem;
            }
        }

        return array;
    }
}